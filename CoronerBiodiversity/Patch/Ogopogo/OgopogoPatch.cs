using System.Collections.Generic;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerBiodiversity.Patch.Ogopogo
{
    [HarmonyPatch(typeof(Biodiversity.Creatures.Ogopogo.OgopogoAI))]
    [HarmonyPatch("SetPlayerGrabbedClientRpc")] //SetPlayerGrabbedClientRpc(int playerID, bool setNull = false, bool resetSpecialPlayer = true)
    public class OgopogoPatch
    {
        //Gravity
        //Drowning
        //Vermin?
        public static List<PlayerControllerB> playersGrabbed = new();
        public static void Postfix(Biodiversity.Creatures.Ogopogo.OgopogoAI __instance, int playerID)
        {
            try
            {
                var player = StartOfRound.Instance.allPlayerScripts[playerID].gameObject.GetComponent<PlayerControllerB>();
                Plugin.Instance.PluginLogger.LogDebug($"Player {player} grabbed");
                playersGrabbed.Add(player);
                //mark player as grabbed by ogopogo
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in OgopogoPatch.Postfix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
        
    }

    [HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB))]
    [HarmonyPatch("PlayerHitGroundEffects")]
    public class PlayerHitGroundEffectsPatch
    {
        public static void Postfix(GameNetcodeStuff.PlayerControllerB __instance)
        {
            if (__instance.isPlayerDead && OgopogoPatch.playersGrabbed.Contains(__instance))
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {__instance.playerClientId} was thrown to their death by the Ogopogo! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(__instance, Plugin.Instance.OGOPOGO_GRAVITY);
            }
        }
    }
    //Drowning - PlayerControllerB.SetFaceUnderwaterFilters
    [HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB))]
    [HarmonyPatch("SetFaceUnderwaterFilters")]
    public class SetFaceUnderwaterFiltersPatch
    {
        public static void Postfix(GameNetcodeStuff.PlayerControllerB __instance)
        {
            if (__instance.isPlayerDead && OgopogoPatch.playersGrabbed.Contains(__instance))
            {
                Plugin.Instance.PluginLogger.LogDebug($"Player {__instance.playerClientId} was drowned by the Ogopogo! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(__instance, Plugin.Instance.OGOPOGO_DROWNING);
            }
        }
    }
}