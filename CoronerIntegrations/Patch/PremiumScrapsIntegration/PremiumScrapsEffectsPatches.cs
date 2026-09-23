using GameNetcodeStuff;
using HarmonyLib;
using PremiumScraps.Utils;
using UnityEngine;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.PremiumScrapsIntegration
{
    [HarmonyPatch(typeof(Effects))]
    internal class PremiumScrapsEffectsPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("Explosion")]
        public static void ExplosionPatch(Vector3 position, float range, int damage, float physicsForce)
        {
            PremiumScrapsExplosionLogic(position, range, damage, physicsForce);
        }

        [HarmonyPostfix]
        [HarmonyPatch("ExplosionLight")]
        public static void ExplosionLightPatch(Vector3 position, float range, int damage, float physicsForce)
        {
            PremiumScrapsExplosionLogic(position, range, damage, physicsForce, isLight: true);
        }

        private static void PremiumScrapsExplosionLogic(Vector3 position, float range, int damage, float physicsForce, bool isLight = false)
        {
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(position, range * (isLight ? 1f : 2.5f));
            if (player != null)
            {
                if ((range == 2f && damage == 90 && physicsForce == 5f) || (range == 4f && damage == 100 && physicsForce == 20f))
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.BOMB);
                }
                else if (range == 3f && damage == 50 && physicsForce == 20f)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.KING);
                }
                else if (range == 3f && damage == 80 && physicsForce == 10f)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.CONTROLLER_UNLUCKY);
                }
                else if (range == 4.5f && damage == 40 && physicsForce == 2f)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.FAKE_AIRHORN_EXPLOSION);
                }
                else if (range == 1.5f && damage == 50 && physicsForce == 1f)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.HELM_DOMINATION);
                }
                else if (range == 2f && damage == 20 && physicsForce == 1f)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.GAZPACHO_EXPLOSION);
                }
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch("Damage")]
        public static void DamagePatch(PlayerControllerB player, CauseOfDeath cause, int animation)
        {
            PremiumScrapsDamageLogic(player, cause, animation);
        }

        private static void PremiumScrapsDamageLogic(PlayerControllerB player, CauseOfDeath cause, int animation)
        {
            if (player != null && player == GameNetworkManager.Instance.localPlayerController && player.isPlayerDead)
            {
                if (cause == CauseOfDeath.Burning && animation == 6)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.FAKE_AIRHORN_LIGHTNING);
                }
                else if (cause == CauseOfDeath.Inertia && animation == 3)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.JOB_APPLICATION);
                }
                else if (cause == CauseOfDeath.Suffocation && animation == 7)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.GAZPACHO_DRUNK);
                }
                else if (cause == CauseOfDeath.Inertia && animation == 0)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.SQUARE_STEEL);
                }
                else if (cause == CauseOfDeath.Strangulation && animation == 1)
                {
                    SetCauseOfDeath(player, PremiumScrapsSoftDep.FRIENDSHIP_ENDER);
                }
            }
        }
    }
}
