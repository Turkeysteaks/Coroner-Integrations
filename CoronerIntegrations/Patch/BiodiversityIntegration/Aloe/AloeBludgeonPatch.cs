using CoronerIntegrations.Patch.BiodiversityIntegration;
using HarmonyLib;

namespace CoronerIntegrations.Patch.BiodiversityIntegration.Aloe {
    [HarmonyPatch(typeof(Biodiversity.Creatures.Aloe.AloeClient))]
    [HarmonyPatch("HandleDamagePlayer")]
    class AloeBludgeonPatch {
        public static void Postfix(Biodiversity.Creatures.Aloe.AloeClient __instance, ulong playerId, int damage) {
            try {
		        HandleAloeKill(playerId);
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in AloeBludgeonPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleAloeKill(ulong playerId) {
            Plugin.Instance.PluginLogger.LogDebug("Player was bludgeoned to death by The Aloe! Processing...");
            var player = StartOfRound.Instance.allPlayerScripts[playerId];
			
            if (player.isPlayerDead) {
				Plugin.Instance.PluginLogger.LogDebug($"Player {playerId} was bludgeoned to death by The Aloe! Setting cause of death...");
				Coroner.API.SetCauseOfDeath(player, BiodiversitySoftDep.ALOE_BLUDGEON);
            }
        }
    }
}