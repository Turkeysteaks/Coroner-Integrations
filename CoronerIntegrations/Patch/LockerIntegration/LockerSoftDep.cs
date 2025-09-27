using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.LockerIntegration;

public class LockerSoftDep
{
    private static bool? _enabled;
    
    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.zealsprince.locker");
            }

            return (bool)_enabled;
        }
    }
    
    //
    public static string LOCKER_KEY = "DeathEnemyLocker"; //
    public static AdvancedCauseOfDeath LOCKER;
    
    public static string LOCKER_EXPLODE_KEY = "DeathEnemyLockerExplode"; //
    public static AdvancedCauseOfDeath LOCKER_EXPLODE;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if(!API.IsRegistered(LOCKER_KEY))
        {
            LOCKER = API.Register(LOCKER_KEY);
        }

        if (!API.IsRegistered(LOCKER_EXPLODE_KEY))
        {
            LOCKER_EXPLODE = API.Register(LOCKER_EXPLODE_KEY);
        }
    }

    // [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    // public static void CoronerSetCauseOfDeath(PlayerControllerB player)
    // {
    //     // Coroner.API.SetCauseOfDeath(player, HEART_ATTACK);
    // }

}