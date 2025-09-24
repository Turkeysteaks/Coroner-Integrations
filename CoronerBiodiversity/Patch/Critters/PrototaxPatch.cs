using System;
using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerBiodiversity.Patch.Critters
{
    [HarmonyPatch(typeof(Biodiversity.Behaviours.DamageTrigger))]
    [HarmonyPatch("Update")]
    public class PrototaxPatch
    {
        private static PlayerControllerB player;
        //Biodiversity.Creatures.Critters.FungiAI // Biodiversity.Behaviours.DamageTrigger
        public static void Postfix(Biodiversity.Behaviours.DamageTrigger __instance, ref bool ___hitLocalPlayer)
        {
            try
            {
                if (___hitLocalPlayer)
                {
                    HandleToxinKill();
                }
            }
            catch (Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in PrototaxPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
        private static void HandleToxinKill() {
            Plugin.Instance.PluginLogger.LogDebug("Player was poisoned by a Prototax! Processing...");
            player = GameNetworkManager.Instance.localPlayerController; //StartOfRound.Instance.allPlayerScripts[playerId];
			
            if (player.isPlayerDead) {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.playerClientId} was poisoned by a Prototax! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, Plugin.Instance.PROTOTAX);
            }
        }
    }
}