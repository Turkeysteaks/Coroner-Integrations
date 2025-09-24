using System.Collections.Generic;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerBiodiversity.Patch.Critters
{

    [HarmonyPatch(typeof(Biodiversity.Creatures.CoilCrab.CoilCrabAI))]
    [HarmonyPatch("SpawnExpolosionClientRpc")]
    public class CoilCrabPatch
    {
        private static Collider[] lastExplosionColliderArr = {};
        public static List<PlayerControllerB> lastExplosionPlayerArr= new(4);
        public static void Prefix(Biodiversity.Creatures.CoilCrab.CoilCrabAI __instance, Vector3 explosionPosition, float killRange, float damageRange) {
            try
            {
                lastExplosionColliderArr = Physics.OverlapSphere(explosionPosition, damageRange, 2621448, QueryTriggerInteraction.Collide); //Layer 3 should be players
                lastExplosionPlayerArr.Clear();
                foreach (var collider in lastExplosionColliderArr)
                {
                    
                    if(collider == null || collider.gameObject == null) continue;
                    var player = collider.gameObject.GetComponent<PlayerControllerB>();
                    
                    if (player == null) continue;
                    lastExplosionPlayerArr.Add(player);
                }
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in CoilCrabPatch.Prefix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }

    
}