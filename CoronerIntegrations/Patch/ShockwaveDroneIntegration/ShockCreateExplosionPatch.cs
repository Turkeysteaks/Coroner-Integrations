using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.ShockwaveDroneIntegration
{
    [HarmonyPatch(typeof(DroneEnemy.DroneEnemyAI))]
    [HarmonyPatch("CreateExplosion")]
    public class ShockCreateExplosionPatch
    {
        private static Collider[] colliderArray = new Collider[16];
        public static void Postfix(DroneEnemy.DroneEnemyAI __instance, Vector3 explosionPosition, bool spawnExplosionEffect,
            int damage, float minDamageRange, float maxDamageRange)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(explosionPosition, maxDamageRange, colliderArray, 2621448, queryTriggerInteraction: QueryTriggerInteraction.Collide);
            for (int i = 0; i < numColliders; i++)
            {
                if (colliderArray[i].gameObject.layer == 3)
                {
                    PlayerControllerB component = colliderArray[i].gameObject.GetComponent<PlayerControllerB>();
                    if(component == null) continue;
                    if (component.isPlayerDead)
                    {
                        HandleDroneExplode(component);
                    }
                    
                }
            }

        }
        
        private static void HandleDroneExplode(PlayerControllerB player) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by dying drone! Processing...");
            // var player = StartOfRound.Instance.allPlayerScripts[playerId];
			
            if (player.isPlayerDead) {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.actualClientId} was killed by dying drone! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, ShockwaveDroneSoftDep.SHOCKWAVE_DRONE_EXPLODE);
            }
        }
    }
}