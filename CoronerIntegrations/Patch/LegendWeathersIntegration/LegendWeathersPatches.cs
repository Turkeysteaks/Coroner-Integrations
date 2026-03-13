using GameNetcodeStuff;
using HarmonyLib;
using LegendWeathers.BehaviourScripts;
using LegendWeathers.Weathers;
using UnityEngine;
using static CoronerIntegrations.Patch.Utils;

namespace CoronerIntegrations.Patch.LegendWeathersIntegration
{
    [HarmonyPatch]
    internal class LegendWeathersPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(MoonTearItem), "EnableFallingEffects")]
        public static void MoonTearFallingPatch(MoonTearItem __instance, bool enable)
        {
            if (!enable && !__instance.isHeld)
            {
                Collider[] colliderArray = Physics.OverlapSphere(__instance.targetFloorPosition, radius: 7, 2621448, queryTriggerInteraction: QueryTriggerInteraction.Collide);
                for (int i = 0; i < colliderArray.Length; i++)
                {
                    if (colliderArray[i].gameObject.layer == 3)
                    {
                        PlayerControllerB player = colliderArray[i].gameObject.GetComponent<PlayerControllerB>();
                        if (player == null || !player.isPlayerDead || player != GameNetworkManager.Instance.localPlayerController)
                        {
                            continue;
                        }
                        SetCauseOfDeath(player, LegendWeathersSoftDep.MOON_TEAR, forceOverride: true);
                    }
                }
            }
        }


        public static bool localPlayerKilledByMoonThisTime = false;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MajoraMoon), "UpdateImpact")]
        public static void MajoraMoonExplosionPatch(MajoraMoon __instance)
        {
            if (__instance.impactStarted && __instance.impact != null)
            {
                PlayerControllerB player = GameNetworkManager.Instance.localPlayerController;
                if (player.isPlayerDead && !localPlayerKilledByMoonThisTime && !player.isInHangarShipRoom && !player.isInElevator && Vector3.Distance(__instance.outsideNodeEndPosition, player.positionOfDeath) <= __instance.impact.transform.localScale.x * __instance.moonRadiusApprox * 0.9f)
                {
                    SetCauseOfDeath(player, LegendWeathersSoftDep.MAJORA_MOON, forceOverride: true);
                    localPlayerKilledByMoonThisTime = true;
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MajoraMoon), "StartMusic")]
        public static void MajoraMoonRestartStatePatch()
        {
            localPlayerKilledByMoonThisTime = false;
        }
    }
}
