using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    [HideInInspector] public float gold, food, buildingMaterial, ore;
    public static CurrencyManager Instance;
    public void Awake()
    {
        Instance = this;                                // Inicializa a instância estática da classe//
        ResourceNode.OnResourceGained += GainResources;
    }

    public bool DoesIHaveCurrencyToBuy(CurrencyType types, float quantity)
    {
        switch (types)
        {
            case CurrencyType.gold:
                if(gold >= quantity) return true;
                else return false;

            case CurrencyType.food:
                if(food >= quantity) return true;
                else return false;

            case CurrencyType.buildingMaterial:
                if(buildingMaterial >= quantity) return true;
                else return false;

            case CurrencyType.ore:
                if(ore >= quantity) return true;
                else return false;

            case CurrencyType.monsterCore:
                if(CoreAndFragmentsManager.instance.GetCores() >= quantity) return true;
                else return false;

            case CurrencyType.fragments:
                if(CoreAndFragmentsManager.instance.GetFragments() >= quantity) return true;
                else return false;
        }

        return false;
    }

    public bool SpendCurrency(CurrencyType types, float quantity)
    {
        if(DoesIHaveCurrencyToBuy(types, quantity))
        {
            switch (types)
            {
                case CurrencyType.gold:
                    gold -= quantity;
                    UIResources.instance.UpdateTextInfo();
                    return true;

                case CurrencyType.food:
                    food -= quantity;
                    UIResources.instance.UpdateTextInfo();
                    return true;

                case CurrencyType.buildingMaterial:
                    buildingMaterial -= quantity;
                    UIResources.instance.UpdateTextInfo();
                    return true;

                case CurrencyType.ore:
                    ore -= quantity;
                    UIResources.instance.UpdateTextInfo();
                    return true;

                case CurrencyType.monsterCore:
                    CoreAndFragmentsManager.instance.SpendCore((int)quantity);
                    CoreAndFragmentsManager.instance.UpdateUi();
                    return true;

                case CurrencyType.fragments:
                    CoreAndFragmentsManager.instance.SpendFragments((int)quantity);
                    CoreAndFragmentsManager.instance.UpdateUi();
                    return true;
            }
        }
        return false;
    }

    // modificador de acesso - Tipo - Nome
    //                        Tipo - Nome         Tipo - Nome
    public void GainCurrency (CurrencyType type, float amount)
    {
        switch(type)
        {
            case CurrencyType.gold:
                gold += amount;
            break;

            case CurrencyType.buildingMaterial:
                buildingMaterial += amount;
            break;

            case CurrencyType.ore:
                ore += amount;
            break;
            
            case CurrencyType.food:
                food += amount;
            break;
        }
        
        UIResources.instance.UpdateTextInfo();
    }

    private void GainResources(ResourceNode node)
    {
        float netIncome = 1;
        switch (node.nodeType)
        {
            case NodeSpecificType.marketGold:
                GainCurrency(CurrencyType.gold, 1);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.gold);
            break;

            case NodeSpecificType.meat:
                netIncome = Library.HUNTCABIN_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Hunt Cabin Level Up"));
                GainCurrency(CurrencyType.food, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.food);
            break;

            case NodeSpecificType.stone:
                netIncome = Library.QUARRY_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Quarry Level Up"));
                GainCurrency(CurrencyType.buildingMaterial, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.buildingMaterial);
            break;

            case NodeSpecificType.wood:
                netIncome = Library.FOREST_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Forestry Level Up"));
                GainCurrency(CurrencyType.buildingMaterial, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.buildingMaterial);
            break;

            case NodeSpecificType.iron:
                netIncome = Library.IRONMINE_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Iron Mine Level Up"));
                GainCurrency(CurrencyType.ore, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.ore);
            break;
            case NodeSpecificType.fishery:
                netIncome = Library.FISHERY_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Fish Cabin Level Up"));
                GainCurrency(CurrencyType.food, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.food);
            break;
            case NodeSpecificType.copper:
                netIncome = Library.COPPERMINE_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Copper Mine Level Up"));
                GainCurrency(CurrencyType.ore, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.ore);
            break;
            case NodeSpecificType.farm:
                netIncome = Library.FARM_BASE_INCOME * (1+GoldStoreManager.instance.GetUpgradeLevel("Farm Level Up"));
                GainCurrency(CurrencyType.food, netIncome);
                FloatingTextManager.Instance.SpawnFloatingText(node, netIncome, CurrencyType.food);
            break;
        }

        UIResources.instance.UpdateTextInfo();
    }

    public void LoadData(GameData data)
    {
        gold = data.amountOfGoldCoins;
        buildingMaterial = data.amountOfBuildingMat;
        ore = data.amountOfOre;
        food = data.amountOfFood;
        
        UIResources.instance.UpdateTextInfo();
    }

    public void SaveData(ref GameData data)
    {
        data.amountOfGoldCoins = gold;
        data.amountOfBuildingMat = buildingMaterial;
        data.amountOfOre = ore;
        data.amountOfFood = food;
    }
}
