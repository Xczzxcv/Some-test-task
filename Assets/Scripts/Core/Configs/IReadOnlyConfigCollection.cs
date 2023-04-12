using System.Collections.Generic;

namespace Core.Configs
{
internal interface IReadOnlyConfigCollection<T> : IReadOnlyDictionary<string, T>
    where T : IGameConfig
{ }
}