using System;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.ReviveCompanyIntegration
{
    [HarmonyPatch(typeof(OPJosMod.ReviveCompany.GeneralUtil))]
    [HarmonyPatch("RevivePlayer")]
    public class ReviveCompanyPatch
    {
        public static void Postfix(int playerId)
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
                Plugin.Instance.PluginLogger.LogError("Error in ReviveCompany ReviveCompanyPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }
}