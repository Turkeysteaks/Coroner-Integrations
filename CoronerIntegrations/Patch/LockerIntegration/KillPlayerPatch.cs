using System;
using System.Collections;
using HarmonyLib;

namespace CoronerIntegrations.Patch.LockerIntegration {

    [HarmonyPatch(typeof(Locker.MonoBehaviours.LockerAI))]
    [HarmonyPatch("KillPlayer")]
    class KillPlayerPatch {
        public static void Postfix(Locker.MonoBehaviours.LockerAI __instance, ulong id, ref IEnumerator __result) {
            try {
		        Action prefixAction = () => { Console.WriteLine("--> beginning"); };
		        Action postfixAction = () => { HandleLockerKill(id); };
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

        private static void HandleLockerKill(ulong playerId) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by a Locker! Processing...");
            var player = StartOfRound.Instance.allPlayerScripts[playerId];

            // if (player.isPlayerDead && player.causeOfDeath == CauseOfDeath.Unknown) {
            Plugin.Instance.PluginLogger.LogDebug($"Player {playerId} was killed by a Locker! Setting cause of death...");
            Coroner.API.SetCauseOfDeath(player, LockerSoftDep.LOCKER);
            // }
        }
    }
}