using BepInEx;
using BepInEx.Logging;
using CoronerIntegrations.Patch.BiodiversityIntegration;
using CoronerIntegrations.Patch.BiodiversityIntegration.Aloe;
using CoronerIntegrations.Patch.BiodiversityIntegration.Critters;
using CoronerIntegrations.Patch.BiodiversityIntegration.Ogopogo;
using CoronerIntegrations.Patch.BunkbedReviveIntegration;
using CoronerIntegrations.Patch.ChillaxScrapsIntegration;
using CoronerIntegrations.Patch.CountryRoadCreatureIntegration;
using CoronerIntegrations.Patch.HerobrineIntegration;
using CoronerIntegrations.Patch.JackensteinApparatusIntegration;
using CoronerIntegrations.Patch.LegendWeathersIntegration;
using CoronerIntegrations.Patch.LethalAnomaliesIntegration;
using CoronerIntegrations.Patch.LethalDoorsFixedIntegration;
using CoronerIntegrations.Patch.LockerIntegration;
using CoronerIntegrations.Patch.PremiumScrapsIntegration;
using CoronerIntegrations.Patch.ReviveCompanyIntegration;
using CoronerIntegrations.Patch.RollingGiantIntegration;
using CoronerIntegrations.Patch.ScopophobiaIntegration;
using CoronerIntegrations.Patch.ShockwaveDroneIntegration;
using CoronerIntegrations.Patch.SirenHeadIntegration;
using CoronerIntegrations.Patch.TheCabinetIntegration;
using CoronerIntegrations.Patch.TheRollingChairIntegration;
using CoronerIntegrations.Patch.UsualScrapIntegration;
using CoronerIntegrations.Patch.ZeldaScrapsIntegration;
using HarmonyLib;

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
    [BepInDependency("com.elitemastereric.coroner", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.github.biodiversitylc.Biodiversity", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("viviko.BunkbedRevive", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Erksmit.LethalRevives", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("OpJosMod.ReviveCompany", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("wexop.country_road_creature", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.zealsprince.locker", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Scopophobia", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("ccode.chair", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Ccode.SirenHead", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("VectorV.TheCabinet", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Emil.UsualScrap", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("nomnomab.rollinggiant", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("droneenemy", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Kittenji.HerobrineMod", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Entity378.LethalDoorsFixed", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zigzag.legendweathers", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zigzag.premiumscraps", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("zigzag.chillaxscraps", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Zeldahu.JackensteinApparatus", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Zeldahu.LethalAnomalies", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Zeldahu.ZeldaScraps", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;

        internal Harmony Harmony { get; } = new Harmony(PluginInfo.PLUGIN_GUID);

        public void Awake()
        {
            Instance = this;
            PluginLogger = Logger;

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loading...");

            PluginLogger.LogInfo($"Biodiversity Found: {BiodiversitySoftDep.enabled}");
            if (BiodiversitySoftDep.enabled)
            {
                Harmony.PatchAll(typeof(AloeBludgeonPatch));
                Harmony.PatchAll(typeof(AloeCrushPatch));
                Harmony.PatchAll(typeof(AloeSlapPatch));
                Harmony.PatchAll(typeof(CoilCrabPatch));
                Harmony.PatchAll(typeof(LandmineSpawnExplosionPatchPatch));
                Harmony.PatchAll(typeof(PrototaxPatch));
                Harmony.PatchAll(typeof(OgopogoPatch));
                Harmony.PatchAll(typeof(VerminPatch));

                BiodiversitySoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"BunkbedRevive_LegitsFork Found: {BunkbedReviveSoftDep.enabled}");
            if (BunkbedReviveSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(BunkbedRevivePlayerPatch));
                //No register because there is no cause of death
            }
            
            PluginLogger.LogInfo($"ReviveCompany or Patched Found: {ReviveCompanySoftDep.enabled}"); //Should work for both Erk's and OpJos'
            if (ReviveCompanySoftDep.enabled)
            {
                Harmony.PatchAll(typeof(ReviveCompanyPatch));
                //No register because there is no cause of death
            }


            PluginLogger.LogInfo($"Scopophobia Found: {ScopophobiaSoftDep.enabled}");
            if (ScopophobiaSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(KillPlayerAnimationPatch));
                ScopophobiaSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"LethalSirenHead Found: {SirenHeadSoftDep.enabled}");
            if (SirenHeadSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(SirenHeadEatPlayerPatch));
                SirenHeadSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"The Rolling Chair Found: {TheRollingChairSoftDep.enabled}");
            if (TheRollingChairSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(RollingChairCollidePatch));
                TheRollingChairSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"The Cabinet Found: {TheCabinetSoftDep.enabled}");
            if (TheCabinetSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(Patch.TheCabinetIntegration.KillPlayerPatch));
                TheCabinetSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"Locker Found: {LockerSoftDep.enabled}");
            if (LockerSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(Patch.LockerIntegration.KillPlayerPatch));
                Harmony.PatchAll(typeof(ExplodePatch));
                LockerSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"CountryRoadCreature Found: {CountryRoadCreatureSoftDep.enabled}");
            if (CountryRoadCreatureSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(GrabAnimationPatch));
                Harmony.PatchAll(typeof(ParanoidAnimationPatch));
                CountryRoadCreatureSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"LethalDoorsFixed Found: {LethalDoorsFixedSoftDep.enabled}");
            if (LethalDoorsFixedSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(DoorInteractionPatch));
                LethalDoorsFixedSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"RollingGiant Found: {RollingGiantSoftDep.enabled}");
            if (RollingGiantSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(OnCollideWithPlayerPatch));
                RollingGiantSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"ShockwaveDrone Found: {ShockwaveDroneSoftDep.enabled}");
            if (ShockwaveDroneSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(ShockScanPatch));
                Harmony.PatchAll(typeof(ShockCreateExplosionPatch));
                ShockwaveDroneSoftDep.CoronerRegister();
            }
            
            PluginLogger.LogInfo($"HerobrineMod Found: {HerobrineSoftDep.enabled}");
            if (HerobrineSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(HerobrineKillPatch));
                Harmony.PatchAll(typeof(MinecraftPlayerExplodePatch));
                //
                HerobrineSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"UsualScrap Found: {UsualScrapSoftDep.enabled}");
            if (UsualScrapSoftDep.enabled)
            {
                Harmony.PatchAll(typeof(RosePatchEquip));
                Harmony.PatchAll(typeof(RosePatchPocket));
                Harmony.PatchAll(typeof(ScissorPatch));
                Harmony.PatchAll(typeof(RadioactiveCellPatch));
                UsualScrapSoftDep.CoronerRegister();
            }

            PluginLogger.LogInfo($"LegendWeathers Found: {LegendWeathersSoftDep.Enabled}");
            if (LegendWeathersSoftDep.Enabled)
            {
                LegendWeathersSoftDep.Register();
            }

            PluginLogger.LogInfo($"PremiumScraps Found: {PremiumScrapsSoftDep.Enabled}");
            if (PremiumScrapsSoftDep.Enabled)
            {
                PremiumScrapsSoftDep.Register();
            }

            PluginLogger.LogInfo($"ChillaxScraps Found: {ChillaxScrapsSoftDep.Enabled}");
            if (ChillaxScrapsSoftDep.Enabled)
            {
                ChillaxScrapsSoftDep.Register();
            }

            PluginLogger.LogInfo($"JackensteinApparatus Found: {JackensteinApparatusSoftDep.Enabled}");
            if (JackensteinApparatusSoftDep.Enabled)
            {
                JackensteinApparatusSoftDep.Register();
            }

            PluginLogger.LogInfo($"LethalAnomalies Found: {LethalAnomaliesSoftDep.Enabled}");
            if (LethalAnomaliesSoftDep.Enabled)
            {
                LethalAnomaliesSoftDep.Register();
            }

            PluginLogger.LogInfo($"ZeldaScraps Found: {ZeldaScrapsSoftDep.Enabled}");
            if (ZeldaScrapsSoftDep.Enabled)
            {
                ZeldaScrapsSoftDep.Register();
            }

            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");
        }
    }
}
