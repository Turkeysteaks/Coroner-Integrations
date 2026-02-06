using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.LethalDoorsFixedIntegration;

public class LethalDoorsFixedSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Entity378.LethalDoorsFixed");
            }

            return (bool)_enabled;
        }
    }

    //
    public static string LETHAL_DOORS_KEY = "DeathLethalDoors"; //
    public static AdvancedCauseOfDeath LETHAL_DOORS;

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if (!API.IsRegistered(LETHAL_DOORS_KEY))
        {
            LETHAL_DOORS = API.Register(LETHAL_DOORS_KEY);
        }
    }
}