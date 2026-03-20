using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ChillaxScrapsIntegration
{
    public class ChillaxScrapsSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("zigzag.chillaxscraps");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath DANCE_NOTE;
        public static AdvancedCauseOfDeath DEATH_NOTE;
        public static AdvancedCauseOfDeath EMERGENCY_MEETING;
        public static AdvancedCauseOfDeath FREDDY;
        public static AdvancedCauseOfDeath NOKIA;
        public static AdvancedCauseOfDeath EPONA;
        public static AdvancedCauseOfDeath UNO_REVERSE;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Register()
        {
            Plugin.Instance.Harmony.PatchAll(typeof(ChillaxScrapsEffectsPatches));
            Plugin.Instance.Harmony.PatchAll(typeof(ChillaxScrapsExplosionPatches));
            Plugin.Instance.Harmony.PatchAll(typeof(ChillaxScrapsTeleportationPatches));
            //Plugin.Instance.Harmony.PatchAll(typeof(MouthDogEponaPatch)); // broken for now

            DANCE_NOTE = RegisterIntegrationKey("DeathItemDanceNote");
            DEATH_NOTE = RegisterIntegrationKey("DeathItemDeathNote");
            EMERGENCY_MEETING = RegisterIntegrationKey("DeathItemEmergencyMeeting");
            FREDDY = RegisterIntegrationKey("DeathItemFreddyFazbear");
            NOKIA = RegisterIntegrationKey("DeathItemNokia");
            EPONA = RegisterIntegrationKey("DeathItemOcarinaEpona");
            UNO_REVERSE = RegisterIntegrationKey("DeathItemUnoReverse");
        }
    }
}
