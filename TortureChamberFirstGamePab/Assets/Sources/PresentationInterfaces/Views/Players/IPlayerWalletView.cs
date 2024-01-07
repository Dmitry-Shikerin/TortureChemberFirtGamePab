using UnityEngine;

namespace MyProject.Sources.Presentation.Views
{
    public interface IPlayerWalletView
    {
        Vector3 Position { get; }
        
        void Add(int quantity);
        void Remove(int quantity);
    }
}