namespace Secret
{
    public interface IEncryptionKeyProvider
    {
        string EncryptionKey { get; }
        byte[] EncryptionKeyBytes { get; }
    }
}