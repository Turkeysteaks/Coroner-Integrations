using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.CountryRoadCreatureIntegration
{

    [HarmonyPatch(typeof(CountryRoadCreature.Scripts.CountryRoadCreatureHeadItem))]
    [HarmonyPatch("ParanoidAnimation")]
    public class ParanoidAnimationPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CountryRoadCreature.Scripts.CountryRoadCreatureHeadItem), "ParanoidAnimation",
            MethodType.Enumerator)]
        static void Postfix_MoveNext(bool __result)
        {
            try
            {
                if (__result)
                    return;
                HandleKill();
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in ParanoidAnimation.MoveNext Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleKill()
        {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by CreatureHead paranoia! Processing...");
            var player = GameNetworkManager.Instance.localPlayerController;

            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug(
                    $"Player {player.playerClientId} was killed by CreatureHead paranoia! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, CountryRoadCreatureSoftDep.COUNTRYROAD_PARANOIA);
            }
        }
    }
}