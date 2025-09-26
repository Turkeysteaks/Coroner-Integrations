using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.SirenHeadIntegration
{

    public class SirenHeadSoftDep
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("SirenHead");
                }

                return (bool)_enabled;
            }
        }

        //
        public static string SIREN_HEAD_KEY = "DeathEnemySirenHead"; //EatPlayer
        public static AdvancedCauseOfDeath SIREN_HEAD;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            if (!API.IsRegistered(SIREN_HEAD_KEY))
            {
                SIREN_HEAD = API.Register(SIREN_HEAD_KEY);
            }
        }

        // [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        // public static void CoronerSetCauseOfDeath(PlayerControllerB player)
        // {
        //     // Coroner.API.SetCauseOfDeath(player, HEART_ATTACK);
        // }

    }
}