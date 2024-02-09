using Sources.Presentation.Animations;
using Sources.Presentation.UI.Conteiners.AudioSources;
using Sources.Presentation.Views.Player.Inventory;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerView : View
    {
        [field: SerializeField] public PlayerMovementView Movement { get; private set; }
        [field: SerializeField] public PlayerAnimation Animation { get; private set; }
        [field: SerializeField] public PlayerInventoryView Inventory { get; private set; }
        [field: SerializeField] public PlayerWalletView Wallet { get; private set; }
        [field: SerializeField] public PlayerAudioSourcesContainer AudioSourcesContainer { get; private set; }
    }
}