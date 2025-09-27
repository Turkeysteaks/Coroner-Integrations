using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace CoronerIntegrations.Patch.LockerIntegration
{
    [HarmonyPatch(typeof(Locker.Utilities))]
    [HarmonyPatch("Explode")]
    public class ExplodePatch
    {
        private static Collider[] colliderArray = new Collider[16];
        public static void Postfix(Locker.Utilities __instance, Vector3 position, float minRange, float maxRange)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(position, maxRange, colliderArray, 2621448, queryTriggerInteraction: QueryTriggerInteraction.Collide);
            for (int i = 0; i < numColliders; i++)
            {
                if (colliderArray[i].gameObject.layer == 3)
                {
                    PlayerControllerB component = colliderArray[i].gameObject.GetComponent<PlayerControllerB>();
                    if(component == null) continue;
                    if (component.isPlayerDead)
                    {
                        HandleLockerExplode(component);
                    }
                    
                }
            }

        }
        
        private static void HandleLockerExplode(PlayerControllerB player) {
            Plugin.Instance.PluginLogger.LogDebug("Player was killed by colliding Lockers! Processing...");
            // var player = StartOfRound.Instance.allPlayerScripts[playerId];
			
            if (player.isPlayerDead) {
                Plugin.Instance.PluginLogger.LogDebug($"Player {player.actualClientId} was killed by colliding Lockers! Setting cause of death...");
                Coroner.API.SetCauseOfDeath(player, LockerSoftDep.LOCKER_EXPLODE);
            }
        }
    }
}