namespace Sources.Infrastructure.Services.LoadServices.Payloads
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