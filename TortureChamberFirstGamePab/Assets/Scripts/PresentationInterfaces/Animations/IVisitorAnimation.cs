namespace Scripts.PresentationInterfaces.Animations
{
    public interface IVisitorAnimation
    {
        void PlayIdle();
        void PlayWalk();
        void PlaySeatIdle();
        void PlayStandUp();
    }
}