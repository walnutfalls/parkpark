namespace Secret
{
    public interface IConnectionProvider<TConnectionData>
    {
        TConnectionData Connection { get; }
    }
}