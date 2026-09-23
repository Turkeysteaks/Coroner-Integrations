using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.JackensteinApparatusIntegration
{
    internal class JackensteinApparatusSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Zeldahu.JackensteinApparatus");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath JACKENSTEIN;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Register()
        {
            Plugin.Instance.Harmony.PatchAll(typeof(JackensteinPatch));

            JACKENSTEIN = RegisterIntegrationKey("DeathEnemyJackenstein");
        }
    }
}
