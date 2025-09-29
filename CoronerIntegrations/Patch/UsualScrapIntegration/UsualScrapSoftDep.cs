using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.UsualScrapIntegration
{

    public class UsualScrapSoftDep
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Emil.UsualScrap");
                }

                return (bool)_enabled;
            }
        }

        //
        public static string ROSE_KEY = "DeathSelectRose"; //
        public static AdvancedCauseOfDeath ROSE;
        public static string SCISSORS_RUNNING_KEY = "DeathScissorsRunning"; //
        public static AdvancedCauseOfDeath SCISSORS_RUNNING;
        public static string RADIOACTIVE_CELL_KEY = "DeathHoldRadioactiveCell"; //
        public static AdvancedCauseOfDeath RADIOACTIVE_CELL;
        public static string INJECTOR_OD_KEY = "DeathEmergencyInjectorOverdose"; //
        public static AdvancedCauseOfDeath INJECTOR_OD;

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void CoronerRegister()
        {
            if (!API.IsRegistered(ROSE_KEY))
            {
                ROSE = API.Register(ROSE_KEY);
            }

            if (!API.IsRegistered(SCISSORS_RUNNING_KEY))
            {
                SCISSORS_RUNNING = API.Register(SCISSORS_RUNNING_KEY);
            }
            
            if (!API.IsRegistered(RADIOACTIVE_CELL_KEY))
            {
                RADIOACTIVE_CELL = API.Register(RADIOACTIVE_CELL_KEY);
            }
            
            if (!API.IsRegistered(INJECTOR_OD_KEY))
            {
                INJECTOR_OD = API.Register(INJECTOR_OD_KEY);
            }
        }
    }
}