using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.HerobrineIntegration;

public class HerobrineSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Kittenji.HerobrineMod");
            }

            return (bool)_enabled;
        }
    }

    //
    public static string HEROBRINE_KEY = "DeathEnemyHerobrine"; //
    public static AdvancedCauseOfDeath HEROBRINE;
    
    public static string HEROBRINE_EXPLODE_KEY = "DeathEnemyHerobrineExplode"; //
    public static AdvancedCauseOfDeath HEROBRINE_EXPLODE;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if (!API.IsRegistered(HEROBRINE_KEY))
        {
            HEROBRINE = API.Register(HEROBRINE_KEY);
        }

        if (!API.IsRegistered(HEROBRINE_EXPLODE_KEY))
        {
            HEROBRINE_EXPLODE = API.Register(HEROBRINE_EXPLODE_KEY);
        }
    }
}