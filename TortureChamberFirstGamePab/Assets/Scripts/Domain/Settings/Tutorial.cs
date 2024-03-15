using Scripts.Domain.DataAccess.SettingData;

namespace Scripts.Domain.Settings
{
    public class Tutorial
    {
        public Tutorial(TutorialData tutorialData)
            : this(tutorialData.HasCompleted)
        {
        }

        public Tutorial()
            : this(false)
        {
        }

        private Tutorial(bool hasCompleted)
        {
            HasCompleted = hasCompleted;
        }

        public bool HasCompleted { get; set; }
    }
}