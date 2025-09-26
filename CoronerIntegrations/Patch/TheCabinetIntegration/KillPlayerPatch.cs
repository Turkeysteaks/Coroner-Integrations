
using System;
using System.Collections;
using HarmonyLib;

namespace CoronerIntegrations.Patch.TheCabinetIntegration {

    [HarmonyPatch(typeof(TheCabinet.TheCabinetAI))]
    [HarmonyPatch("KillPlayer")]
    class KillPlayerPatch {
        public static void Postfix(TheCabinet.TheCabinetAI __instance, ulong playerId, ref IEnumerator __result) {
            try {
		        Action prefixAction = () => { Console.WriteLine("--> beginning"); };
		        Action postfixAction = () => { HandleCabinetKill(playerId); };
		        Action<object> preItemAction = (item) => { Console.WriteLine($"--> before {item}"); };
		        Action<object> postItemAction = (item) => { Console.WriteLine($"--> after {item}"); };
		        Func<object, object> itemAction = (item) =>
		        {
		        	var newItem = item + "+";
		        	Console.WriteLine($"--> item {item} => {newItem}");
		        	return newItem;
		        };
		        var myEnumerator = new SimpleEnumerator()
		        {
		        	enumerator = __result,
		        	prefixAction = prefixAction,
		        	postfixAction = postfixAction,
		        	preItemAction = preItemAction,
		        	postItemAction = postItemAction,
		        	itemAction = itemAction
		        };
		        __result = myEnumerator.GetEnumerator();
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in KillPlayerPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleCabinetKill(ulong playerId) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by The Cabinet! Processing...");
            var player = StartOfRound.Instance.allPlayerScripts[playerId];

            // if (player.isPlayerDead && player.causeOfDeath == CauseOfDeath.Unknown) {
            Plugin.Instance.PluginLogger.LogDebug($"Player {playerId} was killed by The Cabinet! Setting cause of death...");
            Coroner.API.SetCauseOfDeath(player, TheCabinetSoftDep.CABINET);
            // }
        }
    }
}