using GameNetcodeStuff;
using HarmonyLib;
using JackensteinApparatus;
using static CoronerIntegrations.Patch.Utilities;

namespace CoronerIntegrations.Patch.JackensteinApparatusIntegration
{
    [HarmonyPatch]
    internal class JackensteinPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(JackensteinAI), "ExplosionClientRpc")]
        public static void JackensteinExplosionPatch(JackensteinAI __instance)
        {
            PlayerControllerB? player = GetLocalPlayerInExplosionArea(__instance.transform.position, 60f);
            if (player != null)
            {
                SetCauseOfDeath(player, JackensteinApparatusSoftDep.JACKENSTEIN);
            }
        }
    }
}
