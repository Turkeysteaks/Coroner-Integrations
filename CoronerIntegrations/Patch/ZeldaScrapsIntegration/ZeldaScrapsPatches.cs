using GameNetcodeStuff;
using HarmonyLib;
using System.Collections;
using ZeldaScraps;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ZeldaScrapsIntegration
{
    [HarmonyPatch]
    internal class ZeldaScrapsPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ChristmasBauble), "BreakBaubleCoroutine")]
        public static IEnumerator RareBaubleExplosionPatch(IEnumerator result, ChristmasBauble __instance)
        {
            if (!__instance.isRare)
            {
                yield break;
            }
            while (result.MoveNext())
            {
                PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, 1f);
                if (player != null)
                {
                    SetCauseOfDeath(player, ZeldaScrapsSoftDep.CHRISTMAS_BAUBLE);
                }
                yield return result.Current;
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(FireworkRocketVisual), "FireworkExplosionTimer")]
        public static IEnumerator FireworkExplosionPatch(IEnumerator result, FireworkRocketVisual __instance)
        {
            int i = 0;
            while (result.MoveNext())
            {
                if (i == 1)
                {
                    PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, __instance.explosionRange + __instance.explosionRange * 0.5f);
                    if (player != null)
                    {
                        SetCauseOfDeath(player, ZeldaScrapsSoftDep.FIREWORK);
                    }
                }
                i++;
                yield return result.Current;
            }
        }
    }
}
