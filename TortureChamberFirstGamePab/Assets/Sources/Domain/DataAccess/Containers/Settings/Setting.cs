using System;
using Sources.Domain.Settings;

namespace Sources.Domain.DataAccess.Containers.Settings
{
    public class Setting
    {
        public Setting()
        {
            Volume = new Volume();
        }

        public Volume Volume { get; }
    }
}