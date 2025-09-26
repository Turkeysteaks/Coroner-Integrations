using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.BiodiversityIntegration.Ogopogo
{
    [HarmonyPatch(typeof(Biodiversity.Creatures.Ogopogo.VerminAI))]
    [HarmonyPatch("OnCollideWithPlayer")]
    public class VerminPatch
    {
        public static void Postfix(Biodiversity.Creatures.Ogopogo.VerminAI __instance, Collider other) {
            try
            {
	            HandleVerminKill(other);
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in VerminPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        static void HandleVerminKill(Collider other)
        {
            Plugin.Instance.PluginLogger.LogDebug("Player was eaten by Vermin! Processing...");
            var player = other.gameObject.GetComponent<PlayerControllerB>();

            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was eaten by Vermin! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, BiodiversitySoftDep.VERMIN);
            }
        }
    }
}