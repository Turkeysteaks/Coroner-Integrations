using System;
using CoronerIntegrations.Patch.TheRollingChairIntegration;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.TheRollingChairIntegration
{
    [HarmonyPatch(typeof(TheRollingChair.Chair))]
    [HarmonyPatch("OnCollideWithPlayer")]
    public class RollingChairCollidePatch
    {
        public static void Postfix(TheRollingChair.Chair __instance, Collider other)
        {
            try
            {
                PlayerControllerB player = other.gameObject.GetComponent<PlayerControllerB>();
                if (player != null && player.isPlayerDead)
                {
                    HandleKill(player);
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in RollingChair RollingChairCollidePatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
        private static void HandleKill(PlayerControllerB player)
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by The Rolling Chair! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, TheRollingChairSoftDep.ROLLING_CHAIR);
            }
        }
    }
    
}