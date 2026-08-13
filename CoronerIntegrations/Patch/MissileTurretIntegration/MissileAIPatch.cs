using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.MissileTurretIntegration
{
    [HarmonyPatch(typeof(MissileTurret.MissileAI))]
    [HarmonyPatch("ExplodeClientRpc")]
    public class MissileAIPatch
    {
        public static void Postfix(MissileTurret.MissileAI __instance, Vector3 position, float killRange, float damageRange)
        {
            try
            {
                PlayerControllerB? player = Utilities.GetLocalPlayerInExplosionArea(position, killRange);
                Plugin.Instance.PluginLogger.LogInfo($"Player is alive: {player?.isPlayerDead} ");
                if (player != null && player.isPlayerDead)
                {
                    HandleKill(player);
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in MissileTurret ExplodeClientRpc.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleKill(PlayerControllerB player)
        {
            if (!player.isPlayerDead) return;
            Plugin.Instance.PluginLogger.LogDebug(
                $"Player {player.playerClientId} was killed by a Missile Turret! Setting cause of death...");
            Coroner.API.ClearCauseOfDeath(player); //To avoid it being pre-written as an explosion
            Coroner.API.SetCauseOfDeath(player, MissileTurretSoftDep.MISSILE_TURRET_IMPACT);
        }
    }
}