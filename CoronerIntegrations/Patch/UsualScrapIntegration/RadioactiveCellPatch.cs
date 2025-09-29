using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.UsualScrapIntegration
{

    [HarmonyPatch(typeof(UsualScrap.Behaviors.RadioactiveCellScript))]
    [HarmonyPatch("DamageHolder")] //Also DamageHolder
    public class RadioactiveCellPatch
    {
        private static PlayerControllerB player;
        public static void Postfix(UsualScrap.Behaviors.RadioactiveCellScript __instance, ref PlayerControllerB ___playerHeldBy)
        {
            if(___playerHeldBy != null)
            {
                player = ___playerHeldBy;
            }
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UsualScrap.Behaviors.RadioactiveCellScript), "DamageHolder",
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
                Plugin.Instance.PluginLogger.LogError("Error in RadioactiveCellPatch.MoveNext Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        static void HandleKill()
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug("Player was killed by holding a radioactive cell! Processing...");
                
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by holding a radioactive cell! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, UsualScrapSoftDep.RADIOACTIVE_CELL);
            }
        }
    }
}