using System.Linq;
using UnityEngine;

namespace Core.Configs
{
internal partial class ConfigsProvider
{
    [ContextMenu("Validate game configs")]
    private void Validate()
    {
        var configsWithoutViewConfig = itemConfigs.Where(config => 
            itemViewConfigs.All(viewConfig => config.Id != viewConfig.Id));
        foreach (var config in configsWithoutViewConfig)
        {
            Debug.LogError($"Item config '{config.Id}' has no corresponding view config");
        }

        var viewConfigsWithoutConfig = itemViewConfigs.Where(viewConfig =>
            itemConfigs.All(config => viewConfig.Id != config.Id));
        foreach (var viewConfig in viewConfigsWithoutConfig)
        {
            Debug.LogError($"Item view Config '{viewConfig.Id}' has no corresponding config");
        }

        var hasErrors = configsWithoutViewConfig.Any() || viewConfigsWithoutConfig.Any();
        if (!hasErrors)
        {
            Debug.Log("Game configs are correct");
        }
    }
}
}