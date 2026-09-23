using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.TheRollingChairIntegration;

public class TheRollingChairSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("ccode.chair");
            }

            return (bool)_enabled;
        }
    }

    //
    public static string ROLLING_CHAIR_KEY = "DeathEnemyRollingChair"; //
    public static AdvancedCauseOfDeath ROLLING_CHAIR;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if (!API.IsRegistered(ROLLING_CHAIR_KEY))
        {
            ROLLING_CHAIR = API.Register(ROLLING_CHAIR_KEY);
        }
    }
}