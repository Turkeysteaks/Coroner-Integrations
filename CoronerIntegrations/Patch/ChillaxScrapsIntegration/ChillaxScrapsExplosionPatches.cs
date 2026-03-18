using ChillaxScraps.CustomEffects;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ChillaxScrapsIntegration
{
    [HarmonyPatch]
    internal class ChillaxScrapsExplosionPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DanceNote), "KillInAreaDanceNoteClientRpc")]
        public static void DanceNoteExplosionPatch(DanceNote __instance, Vector3 position)
        {
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(position + Vector3.up * 0.25f, 6f);
            if (player != null)
            {
                SetCauseOfDeath(player, ChillaxScrapsSoftDep.DANCE_NOTE);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(Nokia), "OnHitGround")]
        public static void NokiaKnockbackPatch(Nokia __instance)
        {
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, 1.5f);
            if (player != null)
            {
                SetCauseOfDeath(player, ChillaxScrapsSoftDep.NOKIA);
            }
        }
    }
}
