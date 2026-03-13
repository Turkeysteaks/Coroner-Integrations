using GameNetcodeStuff;
using HarmonyLib;
using LegendWeathers.BehaviourScripts;
using LegendWeathers.Utils;
using LegendWeathers.Weathers;
using UnityEngine;
using static CoronerIntegrations.Patch.Utilities;

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
                PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.targetFloorPosition, 7);
                if (player != null)
                {
                    SetCauseOfDeath(player, LegendWeathersSoftDep.MOON_TEAR);
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
                if (player.isPlayerDead && !localPlayerKilledByMoonThisTime && !player.isInHangarShipRoom && !player.isInElevator && Vector3.Distance(__instance.outsideNodeEndPosition, player.transform.position) <= __instance.impact.transform.localScale.x * __instance.moonRadiusApprox * 0.9f)
                {
                    SetCauseOfDeath(player, LegendWeathersSoftDep.MAJORA_MOON);
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


        [HarmonyPostfix]
        [HarmonyPatch(typeof(MajoraMaskItem), "FinishAttachingMajoraMask")]
        public static void MajoraMaskKillPlayerPatch(MajoraMaskItem __instance)
        {
            if (__instance.IsOwner && !__instance.finishedAttaching && __instance.previousPlayerHeldBy != null && __instance.previousPlayerHeldBy.AllowPlayerDeath() && __instance.previousPlayerHeldBy.isPlayerDead)
            {
                SetCauseOfDeath(__instance.previousPlayerHeldBy, LegendWeathersSoftDep.MAJORA_MASK);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Effects), "SpawnLightningBolt")]
        public static void SpawnLightningBoltPatch(Vector3 destination, bool damage)
        {
            if (!damage)
            {
                return;
            }
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(destination, 5);
            if (player != null)
            {
                SetCauseOfDeath(player, LegendWeathersSoftDep.BLOOD_MOON_LIGHTNING);
            }
        }
    }
}
