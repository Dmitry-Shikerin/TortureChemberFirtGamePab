namespace Scripts.Domain.Points
{
    public class EatPoint
    {
        public bool IsClear { get; private set; } = true;

        public void Clean()
        {
            IsClear = true;
        }

        public void SetDirty()
        {
            IsClear = false;
        }
    }
}