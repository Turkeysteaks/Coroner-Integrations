using System;
using Coroner;
using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerIntegrations.Patch.UsualScrapIntegration
{
    [HarmonyPatch(typeof(UsualScrap.Behaviors.RoseScript))]
    [HarmonyPatch("EquipItem")]
    public class RosePatchEquip
    {

        private static PlayerControllerB player;
        
        public static void Prefix(UsualScrap.Behaviors.RoseScript __instance, ref PlayerControllerB ___player)
        {
            if (___player == null) return;
            player = ___player;            
        }
        
        public static void Postfix(UsualScrap.Behaviors.RoseScript __instance)
        {
            try
            {
                if (player.isPlayerDead)
                {
                    API.SetCauseOfDeath(player, UsualScrapSoftDep.ROSE);
                }
                
            }
            catch(Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in RosePatchEquip: "+e.Message);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }

    [HarmonyPatch(typeof(UsualScrap.Behaviors.RoseScript))]
    [HarmonyPatch("PocketItem")]
    public class RosePatchPocket
    {
        private static PlayerControllerB player;
        
        public static void Prefix(UsualScrap.Behaviors.RoseScript __instance, ref PlayerControllerB ___player)
        {
            if (___player == null) return;
            player = ___player;            
        }
        
        public static void Postfix(UsualScrap.Behaviors.RoseScript __instance)
        {
            try
            {
                if (player.isPlayerDead)
                {
                    API.SetCauseOfDeath(player, UsualScrapSoftDep.ROSE);
                }
                
            }
            catch(Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in RosePatchPocket: "+e.Message);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }
}
