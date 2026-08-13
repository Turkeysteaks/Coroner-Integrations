using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.MissileTurretIntegration;

public class MissileTurretSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Finnerex.MissileTurret");
            }

            return (bool)_enabled;
        }
    }

    //
    public static string MISSILE_TURRET_IMPACT_KEY = "DeathEnemyMissileTurretImpact"; //
    public static AdvancedCauseOfDeath MISSILE_TURRET_IMPACT;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if (!API.IsRegistered(MISSILE_TURRET_IMPACT_KEY))
        {
            MISSILE_TURRET_IMPACT = API.Register(MISSILE_TURRET_IMPACT_KEY);
        }
    }
}