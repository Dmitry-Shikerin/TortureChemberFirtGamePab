namespace Scripts.Infrastructure.Payloads
{
    public class LoadServicePayload
    {
        public LoadServicePayload(bool canLoad)
        {
            CanLoad = canLoad;
        }

        public bool CanLoad { get; private set; }
    }
}