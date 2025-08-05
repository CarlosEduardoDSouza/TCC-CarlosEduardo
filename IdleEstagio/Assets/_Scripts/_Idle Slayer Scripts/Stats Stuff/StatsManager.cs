using TMPro;
using UnityEngine;
using UnityEngine.UI;


//This class controls the current game stats. It applies new stats and holds the stat list
[RequireComponent(typeof(MonsterCoreStoreManager))]
public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    public Stats currentStats;

    private void Awake() 
    {
        instance = this;
    }

    public void BuyUpgrade(BaseStoreUpgrades storeUpg)
    {
        if(CurrencyManager.Instance.SpendCurrency(storeUpg.currencyType, storeUpg.GetCost()))
        {
            if(storeUpg.statToApply != null)
                ApplyUpgrade(storeUpg.statToApply);
                
            storeUpg.LevelUpgradeUp();

            Debug.LogWarning("Comprou o level up");
        }
        else
        {
            Debug.LogWarning("N tem dinheiro");
        }
    }

    private void ApplyUpgrade(Stats statsToApply)
    {
        foreach (Stat item in statsToApply.GetStatList())
        {
            currentStats.IncrementStat(item);
        }
    }
}
