
using UnityEngine;

[System.Serializable]
public class StoreUpgrade
{
    public string newName;
    public float baseValue;
    public CurrencyType type;
    public NodeSpecificType nodeType;
    public float multiplier = 1.07f;
    public int level;
    public StoreBannerInfo uiInfo;

    public void UpdateUI()
    {
        uiInfo.ReturnDescription().text = $"Lvl: {level} -> {level+1} ";
        uiInfo.ReturnButtonText().text = GetLevelCost().ToString("F2");
    }

    public float GetLevelCost()
    {
        return baseValue * Mathf.Pow(multiplier, level);
    }

    public void LevelUP()
    {
        if(CurrencyManager.Instance.SpendCurrency(type, GetLevelCost()))
        {
            Debug.Log("TA RODANDO DI BOA");
            level++;
            UpdateUI();
            UIResources.instance.UpdateTextInfo();
            NodeVisualManager.instance.UpdateVisual(nodeType);
        }
    }

    public void InitilizeButton()
    {
        uiInfo.ReturnBuyButton().onClick.AddListener(LevelUP);
    }
}
