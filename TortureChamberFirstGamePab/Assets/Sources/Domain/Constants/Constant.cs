using Sources.Infrastructure.Services.LoadServices.DataAccess.TavernData;

namespace Sources.Domain.Constants
{
    public static class Constant
    {
        public const float Epsilon = 0.01f;

        public static class Inventory
        {
            public const int FirstItemIndex = 0;
            public const int SecondItemIndex = 1;
            public const int ThirdItemIndex = 2;
        }
        
        public static class Overlap
        {
            public const int MaxCollidersValue = 32;
        }
        
        public static class FillingAmount
        {
            public const float Maximum = 1;
            public const float Minimum = 0;
        }
        
        public static class TavernMoodValues
        {
            public const float StartValue = 0.5f;
            public const float RemovedAmount = 0.05f;
        }
        
        public static class GamePlay
        {
            public const int SpawnDelay = 2;
        }

        public static class Input
        {
            public const string Horizontal = "Horizontal";
            public const string Vertical = "Vertical";
            public const string Run = "Run";
        }
        
        public static class Visitors
        {
            public const float SpawnDelay = 5f;
            public const int MaximumCapacity = 2;
            public const float EatFillingRate = 0.2f;
            public const float WaitingEatFillingRate = 0.02f;
        }

        public static class PrefabPaths
        {
            public const string HUD = "Prefabs/HUD";
            public const string PlayerMovementCharacteristic = "Configs/PlayerMovementCharacteristics";
            public const string Curtain = "Views/Bootstrap/CurtainView";
            public const string CoinView = "Views/Coin";
            public const string GarbageView = "Prefabs/Garbage";
            public const string ItemView = "Prefabs/ItemViews/";
            public const string VisitorView = "Prefabs/Visitor";
        }
        
        public static class UpgradeDataKey
        {
            public const string CharismaKey = "CharismaUpgrader";
            public const string InventoryKey = "InventoryUpgrader";
            public const string MovementKey = "MovementUpgrader";
        }

        public static class DataKey
        {
            public const string MovementKey = "PlayerMovement";
            public const string InventoryKey = "PlayerInventory";
            public const string WalletKey = "PlayerWallet";
        }
        
        public static class TavernDataKey
        {
            public const string TavernMoodKey = "TavernMood";
            public const string GameplayKey = "GamePlay";
        }

        public static class UpgradeConfigPath
        {
            public const string Charisma = "Configs/Upgrades/CharismaUpgradeConfig";
            public const string Inventory = "Configs/Upgrades/InventoryUpgradeConfig";
            public const string Movement = "Configs/Upgrades/MovementUpgradeConfig";
        }
        
        public static class ItemConfigPath
        {
            public const string Beer = "Configs/Items/BeerItemConfig";
            public const string Bread = "Configs/Items/BreadItemConfig";
            public const string Meat = "Configs/Items/MeatItemConfig";
            public const string Soup = "Configs/Items/SoupItemConfig";
            public const string Wine = "Configs/Items/WineItemConfig";
        }
        
        public static class SceneNames
        {
            public const string MainMenu = "MainMenu";
            public const string GamePlay = "GamePlay";
        }
    }
}