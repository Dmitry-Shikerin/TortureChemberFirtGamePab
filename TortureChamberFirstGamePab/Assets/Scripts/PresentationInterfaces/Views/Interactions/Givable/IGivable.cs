using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.DomainInterfaces.Items;

namespace Scripts.PresentationInterfaces.Views.Interactions.Givable
{
    public interface IGivable
    {
        UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken);
    }
}