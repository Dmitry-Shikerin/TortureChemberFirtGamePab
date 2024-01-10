using System.Collections.Generic;

namespace Sources.DomainInterfaces.Upgrades
{
    public interface IUpgradeble
    {
        int AddedAmountUpgrade { get; }
        //TODO здесь ли должно это храниться или запрашивать в конструктор сервиса?
        IReadOnlyCollection<int>  LevelThresholds { get; }
        
        void Upgrade();
    }
}