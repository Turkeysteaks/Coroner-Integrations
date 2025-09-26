using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.TheCabinetIntegration
{

    public class TheCabinetSoftDep
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("VectorV.TheCabinet");
                }

                return (bool)_enabled;
            }
        }

        //
        public static string CABINET_KEY = "DeathEnemyCabinet"; //EatPlayer
        public static AdvancedCauseOfDeath CABINET;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            if (!API.IsRegistered(CABINET_KEY))
            {
                CABINET = API.Register(CABINET_KEY);
            }
        }

        // [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        // public static void CoronerSetCauseOfDeath(PlayerControllerB player)
        // {
        //     // Coroner.API.SetCauseOfDeath(player, HEART_ATTACK);
        // }

    }
}