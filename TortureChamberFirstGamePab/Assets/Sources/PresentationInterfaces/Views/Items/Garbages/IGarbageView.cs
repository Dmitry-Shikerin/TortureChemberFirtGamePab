namespace Sources.PresentationInterfaces.Views.Garbages
{
    public interface IGarbageView
    {
        float FillingRate { get; }
        
        public void Destroy();
    }
}