using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;

namespace Sources.Presentation.Views.Taverns
{
    public interface ITakeble
    {
        // IItem TakeItem();
        UniTask<IItem> TakeItemAsync(CancellationToken cancellationToken);
        // IItemView TakeItemView();
    }
}