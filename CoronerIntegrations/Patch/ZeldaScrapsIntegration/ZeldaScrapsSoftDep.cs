using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.ZeldaScrapsIntegration
{
    internal class ZeldaScrapsSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Zeldahu.ZeldaScraps");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath CHRISTMAS_BAUBLE;
        public static AdvancedCauseOfDeath FIREWORK;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Register()
        {
            Plugin.Instance.Harmony.PatchAll(typeof(ZeldaScrapsPatches));

            CHRISTMAS_BAUBLE = RegisterIntegrationKey("DeathItemChristmasBauble");
            FIREWORK = RegisterIntegrationKey("DeathItemFirework");
        }
    }
}
