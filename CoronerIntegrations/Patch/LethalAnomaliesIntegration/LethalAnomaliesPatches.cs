using GameNetcodeStuff;
using HarmonyLib;
using LethalAnomalies;
using LethalAnomalies.External;
using System.Collections;
using UnityEngine;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.LethalAnomaliesIntegration
{
    [HarmonyPatch]
    internal class LethalAnomaliesPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ExternalScripts), "SpawnLightningBolt", [typeof(Vector3), typeof(bool), typeof(Vector3)])]
        public static void LightningBoltPatch(ExternalScripts __instance, Vector3 strikePosition, bool isLethal)
        {
            if (!isLethal)
            {
                return;
            }
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(strikePosition + Vector3.up * 0.25f, 5f);
            if (player != null)
            {
                SetCauseOfDeath(player, LethalAnomaliesSoftDep.SPARK_TOWER);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TouristAI), "AttackCoroutine")]
        public static IEnumerator TouristExplosionPatch(IEnumerator result, TouristAI __instance)
        {
            int i = 0;
            while (result.MoveNext())
            {
                if (__instance.IsServer && i == 1)
                {
                    PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, 10f);
                    if (player != null)
                    {
                        SetCauseOfDeath(player, LethalAnomaliesSoftDep.TOURIST);
                    }
                }
                i++;
                yield return result.Current;
            }
            if (!__instance.IsServer)
            {
                PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, 10f);
                if (player != null)
                {
                    SetCauseOfDeath(player, LethalAnomaliesSoftDep.TOURIST);
                }
            }
            yield return null;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TourBusAI), "ExplosionCoroutine")]
        public static IEnumerator TourBusExplosionPatch(IEnumerator result, TourBusAI __instance)
        {
            while (result.MoveNext())
            {
                yield return result.Current;
            }
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position + new Vector3(0f, 3f, 0f), 35f);
            if (player != null)
            {
                SetCauseOfDeath(player, LethalAnomaliesSoftDep.TOUR_BUS);
            }
            yield return null;
        }
    }
}
