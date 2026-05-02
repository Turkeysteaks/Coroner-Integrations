using System.Runtime.CompilerServices;
using Coroner;

namespace CoronerIntegrations.Patch.BunkbedReviveIntegration;

public class BunkbedReviveSoftDep
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            if (_enabled == null)
            {
                _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(""); //TODO
            }

            return (bool)_enabled;
        }
    }
}