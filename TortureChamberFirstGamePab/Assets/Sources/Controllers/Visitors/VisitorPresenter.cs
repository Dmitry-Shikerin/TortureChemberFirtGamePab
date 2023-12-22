using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Views;

namespace Sources.Controllers
{
    public class VisitorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;

        public VisitorPresenter(FiniteState firstState, IVisitorView visitorView, Visitor visitor)
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
        } 
    
        public void Start()
        {
            Start(_firstState);
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    
        //TODO здесь методы связи модели и вьюшки
        public bool TryGet(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
