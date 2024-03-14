using Sources.Domain.Settings;

namespace Sources.Domain.DataAccess.Containers.Settings
{
    public class Setting
    {
        public Setting()
        {
            Volume = new Volume();
            Tutorial = new Tutorial();
        }

        public Volume Volume { get; }
        public Tutorial Tutorial { get; }
    }
}