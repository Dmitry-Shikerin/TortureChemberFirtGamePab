using UnityEngine;

namespace Scripts.Domain.Items.ItemConfigs
{
    [CreateAssetMenu(fileName = "ItemConfigContainer", menuName = "Characteristics/ItemConfigContainer", order = 51)]
    public class ItemConfigContainer : ScriptableObject
    {
        [field: SerializeField] public ItemConfig Beer { get; private set; }
        [field: SerializeField] public ItemConfig Bread { get; private set; }
        [field: SerializeField] public ItemConfig Meat { get; private set; }
        [field: SerializeField] public ItemConfig Soup { get; private set; }
        [field: SerializeField] public ItemConfig Wine { get; private set; }
    }
}