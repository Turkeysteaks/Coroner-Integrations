using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using Coroner;
using CoronerIntegrations.Patch;
using CoronerIntegrations.Patch.BiodiversityIntegration;
using CoronerIntegrations.Patch.BiodiversityIntegration.Aloe;
using CoronerIntegrations.Patch.BiodiversityIntegration.Critters;
using CoronerIntegrations.Patch.BiodiversityIntegration.Ogopogo;
using CoronerIntegrations.Patch.CountryRoadCreatureIntegration;
using CoronerIntegrations.Patch.LethalDoorsFixedIntegration;
using CoronerIntegrations.Patch.LockerIntegration;
using CoronerIntegrations.Patch.RollingGiantIntegration;
using CoronerIntegrations.Patch.ScopophobiaIntegration;
using CoronerIntegrations.Patch.ShockwaveDroneIntegration;
using CoronerIntegrations.Patch.SirenHeadIntegration;
using CoronerIntegrations.Patch.TheCabinetIntegration;
using CoronerIntegrations.Patch.UsualScrapIntegration;

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
    [BepInDependency("com.github.biodiversitylc.Biodiversity", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("wexop.country_road_creature", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.zealsprince.locker", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Scopophobia", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Ccode.SirenHead", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("VectorV.TheCabinet", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Emil.UsualScrap", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("nomnomab.rollinggiant", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("droneenemy", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Entity378.LethalDoorsFixed", BepInDependency.DependencyFlags.SoftDependency)]
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
                harmony.PatchAll(typeof(Patch.TheCabinetIntegration.KillPlayerPatch));
                
                TheCabinetSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"Locker Found: {LockerSoftDep.enabled}");
            if (LockerSoftDep.enabled)
            {
                harmony.PatchAll(typeof(Patch.LockerIntegration.KillPlayerPatch));
                harmony.PatchAll(typeof(ExplodePatch));
                LockerSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"CountryRoadCreature Found: {CountryRoadCreatureSoftDep.enabled}");
            if (CountryRoadCreatureSoftDep.enabled)
            {
                harmony.PatchAll(typeof(GrabAnimationPatch));
                harmony.PatchAll(typeof(ParanoidAnimationPatch));
                CountryRoadCreatureSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"LethalDoorsFixed Found: {LethalDoorsFixedSoftDep.enabled}");
            if (LethalDoorsFixedSoftDep.enabled)
            {
                harmony.PatchAll(typeof(DoorInteractionPatch));
                LethalDoorsFixedSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"RollingGiant Found: {RollingGiantSoftDep.enabled}");
            if (RollingGiantSoftDep.enabled)
            {
                harmony.PatchAll(typeof(OnCollideWithPlayerPatch));
                RollingGiantSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"ShockwaveDrone Found: {ShockwaveDroneSoftDep.enabled}");
            if (ShockwaveDroneSoftDep.enabled)
            {
                harmony.PatchAll(typeof(ShockScanPatch));
                harmony.PatchAll(typeof(ShockCreateExplosionPatch));
                ShockwaveDroneSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"UsualScrap Found: {UsualScrapSoftDep.enabled}");
            if (UsualScrapSoftDep.enabled)
            {
                harmony.PatchAll(typeof(RosePatchEquip));
                harmony.PatchAll(typeof(RosePatchPocket));
                
                harmony.PatchAll(typeof(ScissorPatch));
                
                harmony.PatchAll(typeof(RadioactiveCellPatch));
                
                
                UsualScrapSoftDep.CoronerRegister();
            }
        }
    }
}
