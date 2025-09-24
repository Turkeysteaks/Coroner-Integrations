using GameNetcodeStuff;
using HarmonyLib;

namespace CoronerBiodiversity.Patch.Critters
{
    [HarmonyPatch(typeof(Coroner.Patch.LandmineSpawnExplosionPatch))]
    [HarmonyPatch("RewriteCauseOfDeath")]
    public class LandmineSpawnExplosionPatchPatch
    {
        public static void Prefix(Coroner.Patch.LandmineSpawnExplosionPatch __instance, PlayerControllerB targetPlayer, float killRange, float physicsForce)
        {
            try
            {
                if (targetPlayer.isPlayerDead && CoilCrabPatch.lastExplosionPlayerArr.Contains(targetPlayer))
                {
                    Plugin.Instance.PluginLogger.LogDebug(
                        $"Player {targetPlayer.playerClientId} was blown up by a CoilCrab! Setting cause of death...");
                    Coroner.API.SetCauseOfDeath(targetPlayer, null);
                    Coroner.API.SetCauseOfDeath(targetPlayer, Plugin.Instance.COILCRAB_EXPLODE);
                }
            }
            catch (System.Exception e)
            {
                Plugin.Instance.PluginLogger.LogError("Error in LandmineSpawnExplosionPatchPatch.Prefix: " + e);
                Plugin.Instance.PluginLogger.LogError(e.StackTrace);
            }
        }
    }
}