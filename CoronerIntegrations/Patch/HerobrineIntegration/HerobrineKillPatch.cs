using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.HerobrineIntegration
{
    [HarmonyPatch(typeof(Kittenji.HerobrineMod.HerobrineAI))]
    [HarmonyPatch("weight_KillPlayer")]
    public class HerobrineKillPatch
    {
        public static void Postfix(Kittenji.HerobrineMod.HerobrineAI __instance,ref PlayerControllerB ___hauntingPlayer)
        {
            try
            {
                if (___hauntingPlayer != null && ___hauntingPlayer.isPlayerDead)
                {
                    HandleKill(___hauntingPlayer);
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in Herobrine HerobrineKillPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
        private static void HandleKill(PlayerControllerB player)
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by Herobrine! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, HerobrineSoftDep.HEROBRINE);
            }
        }
    }
    
}