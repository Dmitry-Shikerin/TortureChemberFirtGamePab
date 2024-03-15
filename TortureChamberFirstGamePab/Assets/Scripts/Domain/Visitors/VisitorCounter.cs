using System;

namespace Scripts.Domain.Visitors
{
    public class VisitorCounter
    {
        public int ActiveVisitorsCount { get; private set; }

        public void AddActiveVisitorsCount()
        {
            ActiveVisitorsCount++;
        }

        public void RemoveActiveVisitor()
        {
            if (ActiveVisitorsCount <= 0)
                throw new InvalidOperationException(nameof(ActiveVisitorsCount));

            ActiveVisitorsCount--;
        }
    }
}