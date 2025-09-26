using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.ScopophobiaIntegration;

public class ScopophobiaSoftDep
{
    private static bool? _enabled;
    
    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Scopophobia");
            }

            return (bool)_enabled;
        }
    }
    
    //
    public static string SHY_GUY_KEY = "DeathEnemyShyGuy"; //killPlayerAnimation (note the lowercase k)
    public static AdvancedCauseOfDeath SHY_GUY;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if(!API.IsRegistered(SHY_GUY_KEY))
        {
            SHY_GUY = API.Register(SHY_GUY_KEY);
        }
    }

    // [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    // public static void CoronerSetCauseOfDeath(PlayerControllerB player)
    // {
    //     // Coroner.API.SetCauseOfDeath(player, HEART_ATTACK);
    // }

}