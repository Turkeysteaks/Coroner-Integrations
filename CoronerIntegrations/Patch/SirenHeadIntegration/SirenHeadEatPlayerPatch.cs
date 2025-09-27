using System;
using System.Collections;
using HarmonyLib;
using LethalSirenHead.Enemy;

namespace CoronerIntegrations.Patch.SirenHeadIntegration {

    [HarmonyPatch(typeof(SirenHeadAI))]
    [HarmonyPatch("EatPlayer")]
    class SirenHeadEatPlayerPatch
    {
	    private static ulong playerID;
        public static void Postfix(SirenHeadAI __instance, ulong player, ref IEnumerator __result) {
	        playerID = player;
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(SirenHeadAI), "EatPlayer", MethodType.Enumerator)]
        static void Postfix_MoveNext(bool __result)
        {
	        try
	        {
		        if (__result)
			        return;
		        HandleSirenHeadKill(playerID);
	        }
	        catch (System.Exception e)
	        {
		        Plugin.Instance.PluginLogger.LogError("Error in SirenHeadEatPlayerPatch: " + e);
		        Plugin.Instance.PluginLogger.LogError(e.StackTrace);
	        }
        }

        private static void HandleSirenHeadKill(ulong playerId) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by Siren Head! Processing...");
            var player = StartOfRound.Instance.allPlayerScripts[playerId];

            // if (player.isPlayerDead && player.causeOfDeath == CauseOfDeath.Unknown) {
            Plugin.Instance.PluginLogger.LogDebug($"Player {playerId} was killed by Siren Head! Setting cause of death...");
            Coroner.API.SetCauseOfDeath(player, SirenHeadSoftDep.SIREN_HEAD);
            playerID = 100000;
            // }
        }
    }
}