using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using Coroner;
using CoronerIntegrations.Patch;
using CoronerIntegrations.Patch.BiodiversityIntegration;
using CoronerIntegrations.Patch.BiodiversityIntegration.Aloe;
using CoronerIntegrations.Patch.BiodiversityIntegration.Critters;
using CoronerIntegrations.Patch.BiodiversityIntegration.Ogopogo;
using CoronerIntegrations.Patch.ScopophobiaIntegration;
using CoronerIntegrations.Patch.SirenHeadIntegration;
using CoronerIntegrations.Patch.TheCabinetIntegration;

namespace CoronerIntegrations
{
    public static class PluginInfo
    {
        public const string PLUGIN_ID = "coroner.integrations";
        public const string PLUGIN_NAME = "Coroner - Integrations";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "Turkeysteaks.coroner.integrations";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;
        
        private void Awake()
        {
            Instance = this;

            PluginLogger = Logger;

            // Apply Harmony patches (if any exist)
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            // harmony.PatchAll();

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");

            PluginLogger.LogInfo($"Biodiversity Found: {BiodiversitySoftDep.enabled}");
            if (BiodiversitySoftDep.enabled)
            {
                harmony.PatchAll(typeof(AloeBludgeonPatch));
                harmony.PatchAll(typeof(AloeCrushPatch));
                harmony.PatchAll(typeof(AloeSlapPatch));
                harmony.PatchAll(typeof(CoilCrabPatch));
                harmony.PatchAll(typeof(LandmineSpawnExplosionPatchPatch));
                harmony.PatchAll(typeof(PrototaxPatch));
                harmony.PatchAll(typeof(OgopogoPatch));
                harmony.PatchAll(typeof(VerminPatch));

                BiodiversitySoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"Scopophobia Found: {ScopophobiaSoftDep.enabled}");
            if (ScopophobiaSoftDep.enabled)
            {
                harmony.PatchAll(typeof(KillPlayerAnimationPatch));
                
                ScopophobiaSoftDep.CoronerRegister();
            }
    
            PluginLogger.LogInfo($"LethalSirenHead Found: {SirenHeadSoftDep.enabled}");
            if (SirenHeadSoftDep.enabled)
            {
                harmony.PatchAll(typeof(SirenHeadEatPlayerPatch));
                
                SirenHeadSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"The Cabinet Found: {TheCabinetSoftDep.enabled}");
            if (TheCabinetSoftDep.enabled)
            {
                harmony.PatchAll(typeof(KillPlayerPatch));
                
                TheCabinetSoftDep.CoronerRegister();
            }
        }
    }
}
