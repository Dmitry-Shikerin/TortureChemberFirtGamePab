using UnityEngine;

namespace PresentationInterfaces.Views.Players
{
    public interface IPlayerWalletView
    {
        Vector3 Position { get; }

        void Add(int quantity);
    }
}