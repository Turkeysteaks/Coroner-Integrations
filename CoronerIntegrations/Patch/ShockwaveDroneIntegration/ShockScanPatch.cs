using System.Collections.Generic;
using Coroner;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.ShockwaveDroneIntegration
{
    [HarmonyPatch(typeof(DroneEnemy.DroneEnemyAI))]
    [HarmonyPatch("Scan")]
    public class ShockScanPatch
    {
        // private static int numA;
        private static int numB;
        private static int playerLayer = 8;
        private static Transform scanArea = new();
        // private static PlayerControllerB player;
        // private static List<PlayerControllerB> playersToSlow;
        private static Collider[] hitColliders = new Collider[256];
        public static void Postfix(DroneEnemy.DroneEnemyAI __instance, ref Transform ___scanArea)
        {
            numB = 0;
            // Plugin.Instance.PluginLogger.LogDebug($"ShockScanPatch.Post: numA: {numA}, numB: {numB}");
            scanArea =  ___scanArea;
            // playersToSlow = ___playersToSlow;
            // if(___playerUsedBy != null)
            // {
            //     player = ___playerUsedBy;
            // }
            //
            // if (player.isPlayerDead)
            // {
            //     HandleKill();
            // }
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DroneEnemy.DroneEnemyAI), "Scan",
            MethodType.Enumerator)]
        static void Postfix_MoveNext(bool __result)
        {
            try
            {
                if (__result)
                {
                    // Plugin.Instance.PluginLogger.LogDebug($"ShockScanPatch.Move: numA: {numA}, numB: {numB}");
                    // numA++;
                    numB++;
                    if (numB > 2)
                    {
                        // Plugin.Instance.PluginLogger.LogDebug($"ShockScanPatch.Move: I think player is hurting numA: {numA}, numB: {numB}");
                        hitColliders = Physics.OverlapBox(scanArea.position, scanArea.localScale, Quaternion.identity, playerLayer);
                        HandleKill();
                    }
                    return;
                }
                // HandleKill();
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in ShockScanPatch.MoveNext Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        static void HandleKill()
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i] == null) return;
                PlayerControllerB playerCol = hitColliders[i].gameObject.GetComponent<PlayerControllerB>();
                if (playerCol.isPlayerDead) 
                {
                    Plugin.Instance.PluginLogger.LogDebug($"Player {playerCol.actualClientId} was killed by Shockwave! Setting cause of death...");
                    API.SetCauseOfDeath(playerCol, ShockwaveDroneSoftDep.SHOCKWAVE_DRONE_SCAN);
                }
            }
        }
    }
}