using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using Coroner;
using CoronerBiodiversity.Patch.Aloe;

namespace CoronerBiodiversity
{
    public static class PluginInfo
    {
        public const string PLUGIN_ID = "coroner.biodiversity";
        public const string PLUGIN_NAME = "Coroner - Biodiversity";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "Turkeysteaks.coroner.biodiversity";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;
        //Aloe
        public string ALOE_SLAP_KEY = "DeathEnemyAloeSlap"; //SlapCollisionDetection.SlapPlayerServerRpc
        public AdvancedCauseOfDeath ALOE_SLAP;
        public string ALOE_CRUSH_KEY = "DeathEnemyAloeCrush"; //AloeClient.CrushPlayerAnimation
        public AdvancedCauseOfDeath ALOE_CRUSH;
        public string ALOE_BLUDGEON_KEY = "DeathEnemyAloeBludgeon"; //AloeClient.HandlePlayerDamage
        public AdvancedCauseOfDeath ALOE_BLUDGEON;
        
        //CoilCrabs //Biodiversity.Creatures.CoilCrab.CoilCrabAI - Explosions? Lightning I assume
        public string COILCRAB_EXPLODE_KEY = "DeathEnemyCoilCrabExplosion";
        public AdvancedCauseOfDeath COILCRAB_EXPLODE;
        //Ogopogo - How to count it when deaths come from gravity or drowning?
        public string OGOPOGO_GRAVITY_KEY = "DeathEnemyOgopogoGravity";
        public AdvancedCauseOfDeath OGOPOGO_GRAVITY;
        
        public string OGOPOGO_DROWNING_KEY = "DeathEnemyOgopogoDrowned";
        public AdvancedCauseOfDeath OGOPOGO_DROWNING;
        //Vermin
        public string VERMIN_KEY = "DeathEnemyVermin"; //OnCollideWithPlayer
        public AdvancedCauseOfDeath VERMIN;
        
        //Boom birds
        
        //Prototax //Biodiversity.Creatures.Critters.FungiAI // Biodiversity.Behaviours.DamageTrigger
        public string PROTOTAX_KEY = "DeathEnemyPrototax";
        public AdvancedCauseOfDeath PROTOTAX;
        
        //HoneyFeeder - Unused as of time of writing
        //Wax soldier - Unused as of time of writing
        private void Awake()
        {
            Instance = this;

            PluginLogger = Logger;

            // Apply Harmony patches (if any exist)
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            harmony.PatchAll(typeof(AloeSlapPatch)); //Remove?
            harmony.PatchAll(typeof(AloeBludgeonPatch)); //Remove?

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");
            
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
    }
}
