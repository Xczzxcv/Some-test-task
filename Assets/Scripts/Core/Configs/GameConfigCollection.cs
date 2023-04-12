using System.Collections.Generic;

namespace Core.Configs
{
internal class GameConfigCollection<T> : Dictionary<string, T>, IReadOnlyConfigCollection<T>
    where T : IGameConfig
{ }
}