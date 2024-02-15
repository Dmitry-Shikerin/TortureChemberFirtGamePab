using System;
using JetBrains.Annotations;
using Sources.InfrastructureInterfaces.Services.UpdateServices;
using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Infrastructure.StateMachines.StateMachineBase
{
    public class StateMachine : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        private IState _currentState;

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

        public void Update(float deltaTime) => 
            _currentState?.Update(deltaTime);

        public void UpdateLate(float deltaTime) => 
            _currentState?.UpdateLate(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) => 
            _currentState?.UpdateFixed(fixedDeltaTime);
    }
}