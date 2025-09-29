using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.CountryRoadCreatureIntegration
{

    [HarmonyPatch(typeof(CountryRoadCreature.Scripts.CountryRoadCreatureEnemyAI))]
    [HarmonyPatch("GrabAnimation")]
    public class GrabAnimationPatch
    {
        public static void Prefix(CountryRoadCreature.Scripts.CountryRoadCreatureEnemyAI __instance, ref PlayerControllerB ___playerToKIll)
        {
            try
            {
                // CountryRoadCreature.Scripts.CountryRoadCreatureHeadItem
                HandleKill(___playerToKIll);
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in GrabAnimationPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleKill(PlayerControllerB player)
        {
            Plugin.Instance.PluginLogger.LogDebug("Player was crushed to death by CountryRoadCreature! Processing...");
            // var player = StartOfRound.Instance.allPlayerScripts[playerId];

            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug(
                    $"Player {player.playerClientId} was crushed to death by CountryRoadCreature! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, CountryRoadCreatureSoftDep.COUNTRYROAD);
            }
        }
    }
}