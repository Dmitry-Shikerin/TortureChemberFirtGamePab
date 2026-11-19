using System.Threading;
using Cysharp.Threading.Tasks;
using DomainInterfaces.Items;

namespace PresentationInterfaces.Views.Interactions.Givable
{
    public interface IGivable
    {
        UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken);
    }
}