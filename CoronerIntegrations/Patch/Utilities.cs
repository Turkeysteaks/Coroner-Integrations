using Coroner;
using GameNetcodeStuff;
using UnityEngine;

namespace CoronerIntegrations.Patch
{
    internal static class Utilities
    {
        /// <summary>
        /// Register the CoronerIntegrations key into the Coroner API and returns the resulting AdvancedCauseOfDeath
        /// </summary>
        /// <param name="key">String key given inside xml files</param>
        /// <returns>AdvancedCauseOfDeath of the key</returns>
        public static AdvancedCauseOfDeath RegisterIntegrationKey(string key)
        {
            if (!API.IsRegistered(key))
            {
                return API.Register(key);
            }
            return AdvancedCauseOfDeath.Registry[key];
        }


        public static void SetCauseOfDeath(PlayerControllerB player, AdvancedCauseOfDeath causeOfDeath, bool forceOverride = true)
        {
            AdvancedDeathTracker.SetCauseOfDeath(player, causeOfDeath, forceOverride);
        }

        public static void SetCauseOfDeath(int playerId, AdvancedCauseOfDeath causeOfDeath, bool forceOverride = true)
        {
            AdvancedDeathTracker.SetCauseOfDeath(playerId, causeOfDeath, forceOverride);
        }


        /// <summary>
        /// Get the local player in the specified area
        /// </summary>
        /// <param name="explosionPosition">The specified position</param>
        /// <param name="explosionRadius">The specified radius</param>
        /// <param name="shouldPlayerBeDead">True if the searched local player should be dead (default) or false otherwise</param>
        /// <returns>Returns the searched local player or null if no player corresponds to the search</returns>
        public static PlayerControllerB? GetLocalPlayerInExplosionArea(Vector3 explosionPosition, float explosionRadius, bool shouldPlayerBeDead = true)
        {
            Collider[] colliderArray = Physics.OverlapSphere(explosionPosition, explosionRadius, 2621448, queryTriggerInteraction: QueryTriggerInteraction.Collide);
            for (int i = 0; i < colliderArray.Length; i++)
            {
                if (colliderArray[i].gameObject.layer == 3)
                {
                    PlayerControllerB player = colliderArray[i].gameObject.GetComponent<PlayerControllerB>();
                    if (player != null && player == GameNetworkManager.Instance.localPlayerController && ((player.isPlayerDead && shouldPlayerBeDead) || (!player.isPlayerDead && !shouldPlayerBeDead)))
                    {
                        return player;
                    }
                }
            }
            return null;
        }
    }
}
