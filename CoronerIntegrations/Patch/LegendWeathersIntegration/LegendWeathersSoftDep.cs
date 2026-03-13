using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utils;

namespace CoronerIntegrations.Patch.LegendWeathersIntegration
{
    public class LegendWeathersSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("zigzag.legendweathers");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath BLOOD_MOON_LIGHTNING;
        public static AdvancedCauseOfDeath MAJORA_MOON;
        public static AdvancedCauseOfDeath MOON_TEAR;
        public static AdvancedCauseOfDeath MAJORA_MASK;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            BLOOD_MOON_LIGHTNING = RegisterIntegrationKey("DeathWeatherLightningBloodMoon");
            MAJORA_MOON = RegisterIntegrationKey("DeathWeatherMajoraMoon");
            MOON_TEAR = RegisterIntegrationKey("DeathItemMoonTear");
            MAJORA_MASK = RegisterIntegrationKey("DeathItemMajoraMask");
        }
    }
}
