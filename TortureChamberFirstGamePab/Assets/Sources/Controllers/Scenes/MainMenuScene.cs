using Sources.ControllersInterfaces;
using Sources.ControllersInterfaces.Scenes;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        public string Name { get; } = nameof(MainMenuScene);

        public void Update(float deltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        public void Enter(object payload)
        {
        }

        public void Exit()
        {
        }
    }
}