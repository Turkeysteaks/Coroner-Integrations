using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.RollingGiantIntegration
{
    [HarmonyPatch(typeof(RollingGiant.RollingGiantAI))]
    [HarmonyPatch("OnCollideWithPlayer")]
    public class OnCollideWithPlayerPatch
    {
        private static PlayerControllerB player;
        public static void Postfix(RollingGiant.RollingGiantAI __instance, Collider other)
        {
            try
            {
                player = other.gameObject.GetComponent<PlayerControllerB>();
                if (player != null && player.isPlayerDead)
                {
                    HandleKill(player);
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in RollingGiant OnCollideWithPlayerPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
        private static void HandleKill(PlayerControllerB player)
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by the Rolling Giant! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, RollingGiantSoftDep.ROLLING_GIANT);
            }
        }
    }
    
}