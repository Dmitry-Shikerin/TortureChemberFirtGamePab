namespace Sources.Domain.Constants
{
    public static class Constant
    {
        public const float Epsilon = 0.01f;

        
        public static class ScrollRect
        {
            public const float MaxValue = 0.99f;
            public const float MinValue = 0.01f;
        }
        
        public static class Volume
        {
            public const float Min = 0;
            public const float Max = 1;
        }

        public static class Coin
        {
            public const float TargetDistance = 1f;
            public const float OffsetY = 0.3f;
        }
        
        public static class Inventory
        {
            public const int StartCapacity = 1;
            public const int FirstItemIndex = 0;
            public const int SecondItemIndex = 1;
            public const int ThirdItemIndex = 2;
        }
        
        public static class Overlap
        {
            public const int MaxCollidersValue = 32;
        }

        public static class ImageRotate
        {
            public const float Speed = -1f;
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
            public const int SpawnDelay = 20;
        }

        public static class Input
        {
            public const string Horizontal = "Horizontal";
            public const string Vertical = "Vertical";
            public const string Run = "Run";
            public const string Rotation = "Rotation";
        }
        
        public static class Visitors
        {
            public const float SpawnDelay = 5f;
            public const int MaximumQuantity = 2;
            public const float EatFillingRate = 0.2f;
            public const float WaitingEatFillingRate = 0.01f;
        }

        public static class PrefabPaths
        {
            public const string HUD = "Prefabs/UI/HUD";
            public const string PlayerMovementCharacteristic = "Configs/PlayerMovementCharacteristics";
            public const string Curtain = "Views/Bootstrap/CurtainView";
            public const string CoinView = "Views/Coin";
            public const string GarbageView = "Prefabs/Garbage";
            public const string ItemView = "Prefabs/ItemViews/";
            public const string VisitorView = "Prefabs/Visitor";
            public const string PlayerView = "Prefabs/Player";
            public const string ItemConfigContainer = "Configs/Items/Containers/ItemConfigContainer";
            public const string UpgradeConfigContainer = "Configs/Upgrades/Containers/UpgradeConfigContainer";
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
            public const string VisitorQuantityKey = "GamePlay";
        }

        public static class SettingDataKey
        {
            public const string VolumeKey = "Volume";
            public const string TutorialKey = "Tutorial";
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
            public const string Gameplay = "GamePlay";
        }

        public static class Localization
        {
            public const string EnglishCode = "English";
            public const string RussianCode = "Russian";
            public const string TurkishCode = "Turkish";
            public const string Turkish = "tr";
            public const string Russian = "ru";
            public const string English = "en";
        }

        public static class TimeScaleValue
        {
            public const float Min = 0;
            public const float Max = 1;
        }
        
        public static class LeaderboardNames
        {
            public const string LeaderboardName = "Leaderboard";
            public const string AnonymousName = "Anonymous";
        }
        
        public static class Forms
        {
            public const string PrefabPath = "Views/Forms";
        }

        public static class App
        {
            public const double CurtainDelay = 1;
            public const double CurtainWaiting = 2;
        }
        
        public static class AdvertisingReward
        {
            public const int CoinsAmount = 10;
        }

        public static class AdvertisingTimer
        {
            public const string ContentText = "Реклама появвится через";
            public const float Delay = 1.2f;
        }
        
        public static class VolumeValue
        {
            public const float BaseValue = 0.2f;
            public const float MaxValue = 1f;
            public const float MinValue = 0;
            public const float VolumeValuePerStep = 0.2f;
            public const int BaseStep = 1;
            public const int MinStep = 0;
            public const int MaxStep = 5;
        }

        public static class GarbageRandomizer
        {
            public const int PositiveRange = 3;
            public const int MaximumRange = 10;
        }
        
        public static class SaveService
        {
            public const int SaveDelay = 2;
        }

        public static class InterstitialService
        {
            public const int ShowDelay = 7;
        }

        public static class TutorialContent
        {
            public const string FodPoints = "Для того чтобы добавить еду в инвентарь подойдите к токи отмеченной" +
                                            " желтым кругом с необходимым вам продуктом и немного подождите пока " +
                                            "таймер полностью не опустеет.";

            public const string Upgrades = "В ходе игры вы будете зарабатывать монеты которые можно потратить " +
                                           "на улучшения. Для того чтобы улучшить свои характеристики подойдите к " +
                                           "точке улучшения и выберете необходимое улучшение. Если монет " +
                                           "достаточно ты вы сможете улучшить характеристику. Вам доступны 3 вида" +
                                           " улучшений. Передвижение увеличивает скоротсть передвижения с " +
                                           "продуктами в инвеентаре. Улучшение инвентаря добавляет дополнительныю " +
                                           "ячейку инвентаря. Улучшение харизмы увеличивает количество " +
                                           "добавляемого счасть после того как вы обслужили посетителя.";

            public const string Coins = "После того как посетитель получит вовремя свой заказ он какоето время" +
                                        " будет его есть, после чеего обязательно оставит для вас монеты. " +
                                        "Подойдите к монете достаточно близко и она сама добавится к вам в " +
                                        "кошелек. Монеты можно поторатить для улучшения характеристик персонажа.";

            public const string Garbage = "Некоторые посетители очень неаккуратные и могут оставить после себя " +
                                          "мусор. Старайтесь своевременно его убирать иначе если новый посетитель" +
                                          " займет грязное место то он будет недоволен и настроение в заведении " +
                                          "ухудшится. Для того чтобы убрать мусор подойдите кему достаточно" +
                                          " близко и подождите по таймер полностью не исчезнет";

            public const string MoodIndicator = "С правой стороны экрана вы увидите индикатор который отвечает " +
                                                "за настроение посетитлей в в таверне старайтесь держать индикатор" +
                                                " настроения на высоком уровне иначе если индикатор настроения " +
                                                "упадет до нуля то вы проиграете. Настроение растет если вы " +
                                                "вовремя успеваете обслужить посетителя. Настроение падает если" +
                                                " вы не успеете вовремя принести ппосетителю заказ а так же сли" +
                                                " посетитель займет место на котором находится мусор.";
        }
    }
}