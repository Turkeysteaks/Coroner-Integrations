using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.ShockwaveDroneIntegration
{

    public class ShockwaveDroneSoftDep
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("droneenemy");
                }

                return (bool)_enabled;
            }
        }

        //
        public static string SHOCKWAVE_DRONE_SCAN_KEY = "DeathEnemyShockwaveDroneScan"; //Scan & Explosion
        public static AdvancedCauseOfDeath SHOCKWAVE_DRONE_SCAN;
        public static string SHOCKWAVE_DRONE_EXPLODE_KEY = "DeathEnemyShockwaveDroneExplode"; //Scan & Explosion
        public static AdvancedCauseOfDeath SHOCKWAVE_DRONE_EXPLODE;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            if (!API.IsRegistered(SHOCKWAVE_DRONE_SCAN_KEY))
            {
                SHOCKWAVE_DRONE_SCAN = API.Register(SHOCKWAVE_DRONE_SCAN_KEY);
            }
            
            if (!API.IsRegistered(SHOCKWAVE_DRONE_EXPLODE_KEY))
            {
                SHOCKWAVE_DRONE_EXPLODE = API.Register(SHOCKWAVE_DRONE_EXPLODE_KEY);
            }
        }
    }
}