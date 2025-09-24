using System;
using System.Collections;
using HarmonyLib;
using Biodiversity;
using GameNetcodeStuff;

namespace CoronerBiodiversity.Patch.Aloe {
    [HarmonyPatch(typeof(Biodiversity.Creatures.Aloe.AloeClient))]
    [HarmonyPatch("CrushPlayerAnimation")]
    class AloeCrushPatch {
        public static void Postfix(Biodiversity.Creatures.Aloe.AloeClient __instance, PlayerControllerB player, ref IEnumerator __result) {
            try {
		        Action prefixAction = () => { Console.WriteLine("--> beginning"); };
		        Action postfixAction = () => { HandleAloeKill(player); };
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
                Plugin.Instance.PluginLogger.LogError("Error in AloeSlapPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        private static void HandleAloeKill(PlayerControllerB player) {
            Plugin.Instance.PluginLogger.LogDebug("Player was crushed by The Aloe! Processing...");
            // var player = StartOfRound.Instance.allPlayerScripts[playerID];
			
            // if (player.isPlayerDead) {
				Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was crushed by The Aloe! Setting cause of death...");
				Coroner.API.SetCauseOfDeath(player, Plugin.Instance.ALOE_CRUSH);
            // }
        }
    }

 //    class SimpleEnumerator : IEnumerable
	// {
	// 	public IEnumerator enumerator;
	// 	public Action prefixAction, postfixAction;
	// 	public Action<object> preItemAction, postItemAction;
	// 	public Func<object, object> itemAction;
	// 	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
	// 	public IEnumerator GetEnumerator()
	// 	{
	// 		prefixAction();
	// 		while (enumerator.MoveNext())
	// 		{
	// 			var item = enumerator.Current;
	// 			preItemAction(item);
	// 			yield return itemAction(item);
	// 			postItemAction(item);
	// 		}
	// 		postfixAction();
	// 	}
	// }
}