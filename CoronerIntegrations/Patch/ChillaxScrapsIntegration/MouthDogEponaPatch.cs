using GameNetcodeStuff;
using HarmonyLib;
using System.Collections;

namespace CoronerIntegrations.Patch.ChillaxScrapsIntegration
{
    [HarmonyPatch]
    internal class MouthDogEponaPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(MouthDogAI), "KillPlayer")]
        public static IEnumerator MouthDogKillPlayerEponaCheckPatch(IEnumerator result, MouthDogAI __instance, int playerId)
        {
            while (result.MoveNext())
            {
                yield return result.Current;
            }
            // does not work, i dont know why for now
            PlayerControllerB player = StartOfRound.Instance.allPlayerScripts[playerId];
            if (player != null && player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug(__instance.screamSFX.name + "   " + __instance.breathingSFX.name);
                // get epona sfx names then set cause of death to EPONA
            }
        }
    }
}
