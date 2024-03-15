using System;
using Scripts.InfrastructureInterfaces.Services.UpdateServices;
using Scripts.InfrastructureInterfaces.StateMachines;

namespace Scripts.Infrastructure.StateMachines.StateMachineBase
{
    public class StateMachine : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        private IState _currentState;

        public void UpdateFixed(float fixedDeltaTime) =>
            _currentState?.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            _currentState?.UpdateLate(deltaTime);

        public void Update(float deltaTime) =>
            _currentState?.Update(deltaTime);

        public void ChangeState(IState state, object payload = null)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            Exit();
            _currentState = state;
            _currentState?.Enter(payload);
        }

        public void Exit() =>
            _currentState?.Exit();
    }
}