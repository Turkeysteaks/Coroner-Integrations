using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.UsualScrapIntegration
{
    //TODO: Add support for killing other players with it
    [HarmonyPatch(typeof(UsualScrap.Behaviors.ScissorsScript))]
    [HarmonyPatch("RollForDamage")]
    public class ScissorPatch
    {
        private static PlayerControllerB player;
        private static readonly int scissorRunDamage = 15;
        public static void Postfix(UsualScrap.Behaviors.ScissorsScript __instance, ref PlayerControllerB ___playerHeldBy)
        {
            if(___playerHeldBy != null)
            {
                player = ___playerHeldBy;
            }
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UsualScrap.Behaviors.ScissorsScript), "RollForDamage", MethodType.Enumerator)]
        static void Postfix_MoveNext()
        {
            try
            {
                if(player != null && (player.isPlayerDead || player.health <= scissorRunDamage))
                {
                    HandleKill();
                }
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in ScissorPatch.MoveNext Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        static void HandleKill()
        {
            // if (player.isPlayerDead)
            // {
                Plugin.Instance.PluginLogger.LogDebug("Player was killed by running with scissors! Processing...");
                
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by running with scissors! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, UsualScrapSoftDep.SCISSORS_RUNNING);
            // }
        }
    }
}