using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Classe base dos upgrades das lojas
[System.Serializable]   // Permite que essa classe apareca no editor.
public class BaseStoreUpgrades
{
    public string upgName;
    public Button buyButton;
    public Stats statToApply;
    public int baseCost;
    public float multiplier;
    public int level;
    public CurrencyType currencyType;
    public bool roundCostValue;
    [Space]
    [Header("UI")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI detailsText;
    [Space]
    [Header("Has a node linked?")]
    public bool yesItDoes;
    public NodeSpecificType nodeType;

    public TextMeshProUGUI GetPriceText()
    {
        return buyButton.GetComponentInChildren<TextMeshProUGUI>(); // Retorna o componente de texto que está dentro do botão de compra.
    }

    public void LevelUpgradeUp()    // aumenta o nível do upgrade.
    {
        level++;
        UiUpdate(); // Atualiza a UI.
    }

    public float GetCost()    //calcula o custo do upgrade com base no nível e no multiplicador.
    {
        if(roundCostValue)
            return Mathf.Round(baseCost * Mathf.Pow(multiplier, level));
        else
            return baseCost * Mathf.Pow(multiplier, level);
    }

    public void UiUpdate()  // Método que atualiza a UI
    {
        GetPriceText().text = GetCost().ToString("F2");
        if(levelText != null) levelText.text = level.ToString();
        if(detailsText != null && yesItDoes) detailsText.text = $"Current : {GetProduction().ToString("F2")}";
    }

    private float GetProduction()
    {
        return nodeType switch
        {
            NodeSpecificType.wood => Library.FOREST_BASE_INCOME * (level+1),
            NodeSpecificType.stone => Library.QUARRY_BASE_INCOME * (level+1),
            NodeSpecificType.meat => Library.HUNTCABIN_BASE_INCOME * (level+1),
            NodeSpecificType.farm => Library.FARM_BASE_INCOME * (level+1),
            NodeSpecificType.iron => Library.IRONMINE_BASE_INCOME * (level+1),
            NodeSpecificType.marketGold => Library.MARKET_BASE_INCOME * (level+1),
            NodeSpecificType.copper => Library.COPPERMINE_BASE_INCOME * (level+1),
            NodeSpecificType.fishery => Library.FISHERY_BASE_INCOME * (level+1),
            NodeSpecificType.clay => Library.CLAYMINE_BASE_INCOME * (level+1),
            NodeSpecificType.gem => Library.GEMMINE_BASE_INCOME * (level+1),
            _ => 0,
        };
    }
}