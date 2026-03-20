using HarmonyLib;
using PremiumScraps.CustomEffects;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.PremiumScrapsIntegration
{
    [HarmonyPatch]
    internal class PremiumScrapsControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Controller), "Update")]
        public static void ControllerCheckForDeadTarget(Controller __instance)
        {
            if (__instance.playerHeldBy == null || GameNetworkManager.Instance.localPlayerController.playerClientId != __instance.playerHeldBy.playerClientId || !__instance.screenIsReady || __instance.targetPlayer == null)
                return;
            if (__instance.targetPlayer.disconnectedMidGame || !__instance.targetPlayer.IsSpawned || !__instance.targetPlayer.isPlayerControlled || __instance.targetPlayer.isPlayerDead)
            {
                SetCauseOfDeath(__instance.targetPlayer, PremiumScrapsSoftDep.CONTROLLER);
            }
        }
    }
}
