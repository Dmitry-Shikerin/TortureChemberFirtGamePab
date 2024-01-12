using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.DomainInterfaces.Items;

namespace Sources.PresentationInterfaces.Views.Interactions.Givable
{
    public interface IGivable
    {
        UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken);
    }
}