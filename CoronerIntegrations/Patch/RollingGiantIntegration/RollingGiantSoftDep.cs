using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.RollingGiantIntegration;

public class RollingGiantSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("nomnomab.rollinggiant");
            }

            return (bool)_enabled;
        }
    }

    //
    public static string ROLLING_GIANT_KEY = "DeathEnemyRollingGiant"; //
    public static AdvancedCauseOfDeath ROLLING_GIANT;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if (!API.IsRegistered(ROLLING_GIANT_KEY))
        {
            ROLLING_GIANT = API.Register(ROLLING_GIANT_KEY);
        }
    }
}