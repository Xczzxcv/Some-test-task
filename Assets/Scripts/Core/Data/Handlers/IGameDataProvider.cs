namespace Core.Data.Handlers
{
internal interface IGameDataProvider<TData> where TData : IGameDataPiece
{
    TData GetData();
}
}