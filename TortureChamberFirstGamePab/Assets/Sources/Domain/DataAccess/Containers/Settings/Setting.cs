using System;
using Sources.Domain.Settings;

namespace Sources.Domain.DataAccess.Containers.Settings
{
    public class Setting
    {
        public Setting(Volume volume)
        {
            Volume = volume ?? throw new ArgumentNullException(nameof(volume));
        }

        public Volume Volume { get; }
    }
}