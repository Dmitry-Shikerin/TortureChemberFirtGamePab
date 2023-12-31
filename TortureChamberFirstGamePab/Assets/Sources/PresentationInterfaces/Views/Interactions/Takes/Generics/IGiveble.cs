﻿using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;

namespace Sources.Presentation.Views.Taverns
{
    public interface IGiveble
    {
        UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken);
    }
}