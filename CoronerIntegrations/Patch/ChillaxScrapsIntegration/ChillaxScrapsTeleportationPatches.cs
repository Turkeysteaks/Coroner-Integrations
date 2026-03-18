using ChillaxScraps.CustomEffects;
using GameNetcodeStuff;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ChillaxScrapsIntegration
{
    [HarmonyPatch]
    internal class ChillaxScrapsTeleportationPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EmergencyMeeting), "TeleportationClientRpc")]
        public static void MeetingTeleportationPatch(EmergencyMeeting __instance, ulong playerID)
        {
            PlayerControllerB player = StartOfRound.Instance.allPlayerScripts[playerID];
            if (player != null && player == GameNetworkManager.Instance.localPlayerController)
            {
                __instance.StartCoroutine(__instance.CheckTeleportationDeath(player));
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnoReverse), "TeleportationClientRpc")]
        public static void UnoTeleportationPatch(UnoReverse __instance, ulong playerID)
        {
            PlayerControllerB player = StartOfRound.Instance.allPlayerScripts[playerID];
            if (player != null && player == GameNetworkManager.Instance.localPlayerController)
            {
                __instance.StartCoroutine(__instance.CheckTeleportationDeath(player));
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnoReverseDX), "TeleportationClientRpc")]
        public static void UnoDxTeleportationPatch(UnoReverseDX __instance, ulong playerID)
        {
            PlayerControllerB player = StartOfRound.Instance.allPlayerScripts[playerID];
            if (player != null && player == GameNetworkManager.Instance.localPlayerController)
            {
                __instance.StartCoroutine(__instance.CheckTeleportationDeath(player));
            }
        }
    }


    internal static class ChillaxScrapsExtensions
    {
        public static IEnumerator CheckTeleportationDeath(this EmergencyMeeting emergencyMeeting, PlayerControllerB player)
        {
            yield return CheckTeleportationDeath(player, ChillaxScrapsSoftDep.EMERGENCY_MEETING);
        }

        public static IEnumerator CheckTeleportationDeath(this UnoReverse unoReverse, PlayerControllerB player)
        {
            yield return CheckTeleportationDeath(player, ChillaxScrapsSoftDep.UNO_REVERSE);
        }

        public static IEnumerator CheckTeleportationDeath(this UnoReverseDX unoReverseDX, PlayerControllerB player)
        {
            yield return CheckTeleportationDeath(player, ChillaxScrapsSoftDep.UNO_REVERSE);
        }

        private static IEnumerator CheckTeleportationDeath(PlayerControllerB player, Coroner.AdvancedCauseOfDeath cause)
        {
            float timer = 0f;
            while (timer < 3f)
            {
                if (player != null && player.isPlayerDead)
                {
                    yield return new WaitForEndOfFrame();
                    SetCauseOfDeath(player, cause);
                    yield break;
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
