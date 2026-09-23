using Coroner;
using System.Runtime.CompilerServices;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.PremiumScrapsIntegration
{
    public class PremiumScrapsSoftDep
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("zigzag.premiumscraps");
                return (bool)_enabled;
            }
        }


        public static AdvancedCauseOfDeath BOMB;
        public static AdvancedCauseOfDeath KING;
        public static AdvancedCauseOfDeath CONTROLLER;
        public static AdvancedCauseOfDeath CONTROLLER_UNLUCKY;
        public static AdvancedCauseOfDeath FAKE_AIRHORN_EXPLOSION;
        public static AdvancedCauseOfDeath FAKE_AIRHORN_LIGHTNING;
        public static AdvancedCauseOfDeath JOB_APPLICATION;
        public static AdvancedCauseOfDeath HELM_DOMINATION;
        public static AdvancedCauseOfDeath GAZPACHO_DRUNK;
        public static AdvancedCauseOfDeath GAZPACHO_EXPLOSION;
        public static AdvancedCauseOfDeath SQUARE_STEEL;
        public static AdvancedCauseOfDeath FRIENDSHIP_ENDER;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Register()
        {
            Plugin.Instance.Harmony.PatchAll(typeof(PremiumScrapsEffectsPatches));
            Plugin.Instance.Harmony.PatchAll(typeof(PremiumScrapsControllerPatches));

            BOMB = RegisterIntegrationKey("DeathItemBomb");
            KING = RegisterIntegrationKey("DeathItemTheKing");
            CONTROLLER = RegisterIntegrationKey("DeathItemController");
            CONTROLLER_UNLUCKY = RegisterIntegrationKey("DeathItemControllerUnlucky");
            FAKE_AIRHORN_EXPLOSION = RegisterIntegrationKey("DeathItemFakeAirhornExplosion");
            FAKE_AIRHORN_LIGHTNING = RegisterIntegrationKey("DeathItemFakeAirhornLightning");
            JOB_APPLICATION = RegisterIntegrationKey("DeathItemJobApplication");
            HELM_DOMINATION = RegisterIntegrationKey("DeathItemHelmOfDomination");
            GAZPACHO_DRUNK = RegisterIntegrationKey("DeathItemGazpachoDrunk");
            GAZPACHO_EXPLOSION = RegisterIntegrationKey("DeathItemGazpachoUnlucky");
            SQUARE_STEEL = RegisterIntegrationKey("DeathItemSquareSteel");
            FRIENDSHIP_ENDER = RegisterIntegrationKey("DeathItemFriendshipEnder");
        }
    }
}
