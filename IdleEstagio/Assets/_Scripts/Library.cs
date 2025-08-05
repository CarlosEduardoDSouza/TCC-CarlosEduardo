using UnityEngine.Rendering;

public static class Library
{
    public const float FOREST_BASE_INCOME = 1;
    public const float FOREST_BASE_TIME = 1;

    public const float HUNTCABIN_BASE_INCOME = 10;
    public const float HUNTCABIN_BASE_TIME = 3;

    public const float QUARRY_BASE_INCOME = 90;
    public const float QUARRY_BASE_TIME = 6;

    public const float COPPERMINE_BASE_INCOME = 720;
    public const float COPPERMINE_BASE_TIME = 12;

    public const float FARM_BASE_INCOME = 6480;
    public const float FARM_BASE_TIME = 24;

    public const float MARKET_BASE_INCOME = 51840;
    public const float MARKET_BASE_TIME = 48;

    public const float IRONMINE_BASE_INCOME = 414720;
    public const float IRONMINE_BASE_TIME = 96;

    public const float FISHERY_BASE_INCOME = 3317760;
    public const float FISHERY_BASE_TIME = 192;

    public const float CLAYMINE_BASE_INCOME = 23224320;
    public const float CLAYMINE_BASE_TIME = 384;
    
    public const float GEMMINE_BASE_INCOME = 185794560;
    public const float GEMMINE_BASE_TIME = 768;

    public readonly static int[] tiersUnlockPoints = {0,1,3,6,10,15,21,28,36,45};

    public static float GET_TIME(NodeSpecificType nodeType)
    {
        return nodeType switch
        {
            NodeSpecificType.wood => FOREST_BASE_TIME,
            NodeSpecificType.stone => QUARRY_BASE_TIME,
            NodeSpecificType.meat => HUNTCABIN_BASE_TIME,
            NodeSpecificType.farm => FARM_BASE_TIME,
            NodeSpecificType.iron => IRONMINE_BASE_TIME,
            NodeSpecificType.marketGold => MARKET_BASE_TIME,
            NodeSpecificType.copper => COPPERMINE_BASE_TIME,
            NodeSpecificType.fishery => FISHERY_BASE_TIME,
            NodeSpecificType.clay => CLAYMINE_BASE_TIME,
            NodeSpecificType.gem => GEMMINE_BASE_TIME,
            _ => 10,
        };
    }
}

public enum Enum_Stats {damage, life, moveSpeed, foodPerSecond, coinsPerSecond, forestryLevel}
public enum ButtonType {goldStore, monsterCoreStore, skillTrees, workerStore}
