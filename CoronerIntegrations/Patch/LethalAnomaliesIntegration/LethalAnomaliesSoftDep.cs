using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.LethalAnomaliesIntegration
{
    internal class LethalAnomaliesSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Zeldahu.LethalAnomalies");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath SPARK_TOWER;
        public static AdvancedCauseOfDeath TOURIST;
        public static AdvancedCauseOfDeath TOUR_BUS;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Register()
        {
            Plugin.Instance.Harmony.PatchAll(typeof(LethalAnomaliesPatches));

            SPARK_TOWER = RegisterIntegrationKey("DeathEnemySparkTower");
            TOURIST = RegisterIntegrationKey("DeathEnemyTourist");
            TOUR_BUS = RegisterIntegrationKey("DeathEnemyTourBus");
        }
    }
}
