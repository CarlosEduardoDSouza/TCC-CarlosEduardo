using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldStoreManager : BaseStore, IDataPersistence
{
    [Space]
    [SerializeField] private List<BaseStoreUpgrades> upgrades = new List<BaseStoreUpgrades>();  // Lista de upgrades disponíveis na loja, cada um do tipo BaseStoreUpgrades.
    public static GoldStoreManager instance { get; private set;}

    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        LinkButtonsToFunctions();   //vincular botões às suas funções específicas.
    }
    private void LinkButtonsToFunctions()   // Função que vincula os botões de compra às funções de compra de upgrade.
    {
        foreach (BaseStoreUpgrades item in upgrades)
        {
            if(item == null) return;

            item.buyButton.onClick.AddListener( () => {
                StatsManager.instance.BuyUpgrade(item); // Quando o botão é clicado a função de compra do upgrade no StatsManager é chamada.
            } );

            item.UiUpdate();     // Atualiza a UI do item.
        }
    }

    public int GetUpgradeLevel(string upgradeName)
    {
        return upgrades.Where( item => item.upgName == upgradeName).FirstOrDefault().level;
    }

    public void LoadData(GameData data)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].level = data.goldStoreUpgradesLevels[i];
            
            if(upgrades[i].level > 0)
                upgrades[i].UiUpdate();
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            data.goldStoreUpgradesLevels[i] = upgrades[i].level;
        }
    }
}
