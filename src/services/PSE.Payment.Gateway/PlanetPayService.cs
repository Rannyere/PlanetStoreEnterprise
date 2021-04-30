namespace PSE.Payment.Gateway
{
    public class PlanetPayService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public PlanetPayService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}