using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.HerobrineIntegration;

[HarmonyPatch(typeof(Kittenji.HerobrineMod.Networking.MinecraftPlayerNetworking))]
[HarmonyPatch("ExplodePlayer")]
public class MinecraftPlayerExplodePatch
{
    public static void Postfix(Kittenji.HerobrineMod.Networking.MinecraftPlayerNetworking __instance, ref PlayerControllerB ___playerScript)
    {
        try
        {
            if (___playerScript != null && ___playerScript.isPlayerDead)
            {
                HandleKill(___playerScript);
            }
        }
        catch (Exception e)
        {
            Plugin.Instance.PluginLogger.LogError("Error in Herobrine MinecraftPlayerExplodePatch.Postfix: " + e);
            Plugin.Instance.PluginLogger.LogError(e.StackTrace);
        }
    }

    private static void HandleKill(PlayerControllerB player)
    {
        if (player.isPlayerDead)
        {
            Plugin.Instance.PluginLogger.LogDebug(
                $"Player {player.playerClientId} was exploded by Herobrine! Setting cause of death...");
            Coroner.API.SetCauseOfDeath(player, HerobrineSoftDep.HEROBRINE_EXPLODE);
        }
    }
}