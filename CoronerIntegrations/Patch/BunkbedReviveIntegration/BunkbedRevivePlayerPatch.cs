using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.BunkbedReviveIntegration
{
    [HarmonyPatch(typeof(BunkbedRevive.BunkbedController))]
    [HarmonyPatch("RevivePlayer")]
    public class BunkbedRevivePlayerPatch
    {
        public static void Postfix(BunkbedRevive.BunkbedController __instance, int playerId)
        {
            try
            {
                PlayerControllerB allPlayerScript = StartOfRound.Instance.allPlayerScripts[playerId];
                if (allPlayerScript.isPlayerDead)
                {
                    return;
                }
                Coroner.API.ClearCauseOfDeath(allPlayerScript);

            } catch  (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in BunkbedRevive BunkbedRevivePlayerPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }
}