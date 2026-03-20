using ChillaxScraps.Utils;
using GameNetcodeStuff;
using HarmonyLib;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ChillaxScrapsIntegration
{
    [HarmonyPatch(typeof(Effects))]
    internal class ChillaxScrapsEffectsPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("Damage")]
        public static void DamagePatch(PlayerControllerB player, CauseOfDeath cause, int animation)
        {
            ChillaxScrapsDamageLogic(player, cause, animation);
        }

        private static void ChillaxScrapsDamageLogic(PlayerControllerB player, CauseOfDeath cause, int animation)
        {
            if (player != null && player == GameNetworkManager.Instance.localPlayerController && player.isPlayerDead)
            {
                if (cause == CauseOfDeath.Unknown && animation == 3)
                {
                    SetCauseOfDeath(player, ChillaxScrapsSoftDep.DEATH_NOTE);
                }
                else if (cause == CauseOfDeath.Mauling && animation == 8)
                {
                    SetCauseOfDeath(player, ChillaxScrapsSoftDep.FREDDY);
                }
            }
        }
    }
}
