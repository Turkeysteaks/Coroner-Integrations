using System;
using CoronerIntegrations.Patch.LethalDoorsFixedIntegration;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.LethalDoorsFixedIntegration
{
    [HarmonyPatch(typeof(Lethal_Doors.Patches.DoorInteractionPatch))]
    [HarmonyPatch("ApplyLethalDamageOrInjurePlayer")]
    public class DoorInteractionPatch
    {
        public static void Postfix(Lethal_Doors.Patches.DoorInteractionPatch __instance, PlayerControllerB player)
        {
            try
            {
                if (player != null && player.isPlayerDead)
                {
                    HandleKill(player);
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in LethalDoors DoorInteractionPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
        private static void HandleKill(PlayerControllerB player)
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by Lethal Doors! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, LethalDoorsFixedSoftDep.LETHAL_DOORS);
            }
        }
    }
    
}