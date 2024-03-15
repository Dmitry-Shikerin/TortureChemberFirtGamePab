using UnityEngine;

namespace Scripts.Domain.Items.ItemConfigs
{
    [CreateAssetMenu(
        fileName = "ItemConfig",
        menuName = "Characteristics/ItemConfig",
        order = 51)]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}