using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.CodeRebirthIntegration
{

    public class CodeRebirthSoftDep
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("CodeRebirth"); //Might be slightly different
                }

                return (bool)_enabled;
            }
        }

        //
        public static string _KEY = "DeathEnemyxxx"; //
        public static AdvancedCauseOfDeath DEATH;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            if (!API.IsRegistered(_KEY))
            {
                 DEATH = API.Register(_KEY);
            }
        }
    }
}