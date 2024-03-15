namespace Scripts.Infrastructure.Payloads
{
    public class InitializeServicePayload
    {
        public InitializeServicePayload(bool isInitialized)
        {
            IsInitialized = isInitialized;
        }

        public bool IsInitialized { get; }
    }
}