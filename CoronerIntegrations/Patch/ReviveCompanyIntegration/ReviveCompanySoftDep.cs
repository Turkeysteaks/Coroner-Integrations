using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.ReviveCompanyIntegration;

public class ReviveCompanySoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("OpJosMod.ReviveCompany"); //Erk.LethalRevives
            }

            return (bool)_enabled;
        }
    }
}