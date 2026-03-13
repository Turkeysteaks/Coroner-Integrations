using Coroner;
using GameNetcodeStuff;

namespace CoronerIntegrations.Patch
{
    internal static class Utils
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


        public static void SetCauseOfDeath(PlayerControllerB player, AdvancedCauseOfDeath causeOfDeath, bool forceOverride = false)
        {
            AdvancedDeathTracker.SetCauseOfDeath(player, causeOfDeath, forceOverride);
        }

        public static void SetCauseOfDeath(int playerId, AdvancedCauseOfDeath causeOfDeath, bool forceOverride = false)
        {
            AdvancedDeathTracker.SetCauseOfDeath(playerId, causeOfDeath, forceOverride);
        }
    }
}
