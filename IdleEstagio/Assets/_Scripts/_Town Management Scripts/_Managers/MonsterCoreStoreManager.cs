using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCoreStoreManager : BaseStore
{
    public static MonsterCoreStoreManager instance;
    [SerializeField] private List<BaseStoreUpgrades> upgrades = new List<BaseStoreUpgrades>();  // Lista de upgrades disponíveis na loja, cada um do tipo BaseStoreUpgrades.

    void Awake() 
    {
        LinkButtonsToFunctions();   //vincular botões às suas funções específicas.
        instance = this;
    }
    private void LinkButtonsToFunctions()   // Função que vincula os botões de compra às funções de compra de upgrade.
    {
        foreach (BaseStoreUpgrades item in upgrades)
        {
            item.buyButton.onClick.AddListener( () => {
                StatsManager.instance.BuyUpgrade(item); // Quando o botão é clicado a função de compra do upgrade no StatsManager é chamada.
            } );

            item.UiUpdate();     // Atualiza a UI do item.
        }
    }
}


