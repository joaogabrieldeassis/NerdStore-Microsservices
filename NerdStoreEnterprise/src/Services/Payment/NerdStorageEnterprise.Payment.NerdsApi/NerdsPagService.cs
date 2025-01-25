namespace NerdStorageEnterprise.Payment.NerdsApi;

public class NerdsPagService(string apiKey, string encryptionKey)
{
    public readonly string ApiKey = apiKey;
    public readonly string EncryptionKey = encryptionKey;
}