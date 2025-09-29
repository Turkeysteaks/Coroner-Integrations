using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.UsualScrapIntegration
{

    [HarmonyPatch(typeof(UsualScrap.Behaviors.Effects.EmergencyInjectorOverdoseEffect))]
    [HarmonyPatch("EmergencyInjectorOverdose")] //Coroutine
    public class EmergencyInjectorPatch
    {
        private static PlayerControllerB player;
        public static void Postfix(UsualScrap.Behaviors.Effects.EmergencyInjectorOverdoseEffect __instance, ref PlayerControllerB ___playerUsedBy)
        {
            if(___playerUsedBy != null)
            {
                player = ___playerUsedBy;
            }

            if (player.isPlayerDead)
            {
                HandleKill();
            }
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UsualScrap.Behaviors.Effects.EmergencyInjectorOverdoseEffect), "EmergencyInjectorOverdose",
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
                Plugin.Instance.PluginLogger.LogError("Error in EmergencyInjectorPatch.MoveNext Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }

        static void HandleKill()
        {
            if (player.isPlayerDead)
            {
                Plugin.Instance.PluginLogger.LogDebug("Player was killed by emergency injector OD! Processing...");
                
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was killed by emergency injector OD! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, UsualScrapSoftDep.INJECTOR_OD);
            }
        }
    }
}