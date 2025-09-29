using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.CountryRoadCreatureIntegration;

public class CountryRoadCreatureSoftDep
{
    private static bool? _enabled;
    
    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("wexop.country_road_creature");
            }

            return (bool)_enabled;
        }
    }
    
    //
    public static string COUNTRYROAD_KEY = "DeathEnemyCountryRoadCreature"; //
    public static AdvancedCauseOfDeath COUNTRYROAD;
    public static string COUNTRYROAD_PARANOIA_KEY = "DeathEnemyCountryRoadCreatureHead"; //
    public static AdvancedCauseOfDeath COUNTRYROAD_PARANOIA;
    

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        if(!API.IsRegistered(COUNTRYROAD_KEY))
        {
            COUNTRYROAD = API.Register(COUNTRYROAD_KEY);
        }
        
        if(!API.IsRegistered(COUNTRYROAD_PARANOIA_KEY))
        {
            COUNTRYROAD_PARANOIA = API.Register(COUNTRYROAD_PARANOIA_KEY);
        }
    }
}