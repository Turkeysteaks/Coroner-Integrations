using System.Runtime.CompilerServices;
using Coroner;
using GameNetcodeStuff;

namespace CoronerIntegrations.Patch.BiodiversityIntegration;

public class BiodiversitySoftDep
{
    private static bool? _enabled;
    
    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.github.biodiversitylc.Biodiversity");
            }

            return (bool)_enabled;
        }
    }
    
    //Aloe
    public static string ALOE_SLAP_KEY = "DeathEnemyAloeSlap"; //SlapCollisionDetection.SlapPlayerServerRpc
    public static AdvancedCauseOfDeath ALOE_SLAP;
    public static string ALOE_CRUSH_KEY = "DeathEnemyAloeCrush"; //AloeClient.CrushPlayerAnimation
    public static AdvancedCauseOfDeath ALOE_CRUSH;
    public static string ALOE_BLUDGEON_KEY = "DeathEnemyAloeBludgeon"; //AloeClient.HandlePlayerDamage
    public static AdvancedCauseOfDeath ALOE_BLUDGEON;
        
    //CoilCrabs //Biodiversity.Creatures.CoilCrab.CoilCrabAI - Explosions? Lightning I assume
    public static string COILCRAB_EXPLODE_KEY = "DeathEnemyCoilCrabExplosion";
    public static AdvancedCauseOfDeath COILCRAB_EXPLODE;
    //Ogopogo - How to count it when deaths come from gravity or drowning?
    public static string OGOPOGO_GRAVITY_KEY = "DeathEnemyOgopogoGravity";
    public static AdvancedCauseOfDeath OGOPOGO_GRAVITY;
        
    public static string OGOPOGO_DROWNING_KEY = "DeathEnemyOgopogoDrowned";
    public static AdvancedCauseOfDeath OGOPOGO_DROWNING;
    //Vermin
    public static string VERMIN_KEY = "DeathEnemyVermin"; //OnCollideWithPlayer
    public static AdvancedCauseOfDeath VERMIN;
        
    //Boom birds
        
    //Prototax //Biodiversity.Creatures.Critters.FungiAI // Biodiversity.Behaviours.DamageTrigger
    public static string PROTOTAX_KEY = "DeathEnemyPrototax";
    public static AdvancedCauseOfDeath PROTOTAX;
        
    //HoneyFeeder - Unused as of time of writing
    //Wax soldier - Unused as of time of writing

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerRegister()
    {
        //Yes this is a dumb AF way to do it, I'll enumerate it instead eventually
        if(!API.IsRegistered(ALOE_SLAP_KEY))
        {
            ALOE_SLAP = API.Register(ALOE_SLAP_KEY);
        }

        if (!API.IsRegistered(ALOE_CRUSH_KEY))
        {
            ALOE_CRUSH = API.Register(ALOE_CRUSH_KEY);
        }

        if (!API.IsRegistered(ALOE_BLUDGEON_KEY))
        {
            ALOE_BLUDGEON = API.Register(ALOE_BLUDGEON_KEY);
        }

        if (!API.IsRegistered(COILCRAB_EXPLODE_KEY))
        {
            COILCRAB_EXPLODE = API.Register(COILCRAB_EXPLODE_KEY);
        }

        if (!API.IsRegistered(VERMIN_KEY))
        {
            VERMIN = API.Register(VERMIN_KEY);
        }

        if (!API.IsRegistered(PROTOTAX_KEY))
        {
            PROTOTAX = API.Register(PROTOTAX_KEY);
        }

        if (!API.IsRegistered(OGOPOGO_GRAVITY_KEY))
        {
            OGOPOGO_GRAVITY = API.Register(OGOPOGO_GRAVITY_KEY);
        }
            
        if (!API.IsRegistered(OGOPOGO_DROWNING_KEY))
        {
            OGOPOGO_DROWNING = API.Register(OGOPOGO_DROWNING_KEY);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void CoronerSetCauseOfDeath(PlayerControllerB player)
    {
        // Coroner.API.SetCauseOfDeath(player, HEART_ATTACK);
    }
}