using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// O banco de dados em si do save system.
// Todas as variaveis qe precisam ser salvas estão aqui
[System.Serializable]
public class GameData
{
    #region ================================================== CURRENCY MANAGER
        public float amountOfGoldCoins;
        public float amountOfBuildingMat;
        public float amountOfOre;  
        public float amountOfFood;
    #endregion




    #region ================================================== MOSTER STORE MANAGER
        public int amountOfCoreQuantity;
        public int amountOfFragmentsQuantity;
    #endregion




    #region ================================================== GOLD STORE MANAGER
        public List<int> goldStoreUpgradesLevels = new();
    #endregion




    #region ================================================== RESOURCE NODES
        public List<bool> resourceNodesThatAreBuilt = new();
        public int currentNodeStep;
    #endregion




    //Variaveis usadas quando o jogo começa ou é um New Game
    public GameData()
    {
        #region ================================================== CURRENCY MANAGER

            amountOfGoldCoins = 0;
            amountOfBuildingMat = 0;
            amountOfOre = 0;
            amountOfFood = 0;

        #endregion




        #region ================================================== MOSTER STORE MANAGER

            amountOfCoreQuantity = 0;
            amountOfFragmentsQuantity = 0;

        #endregion




        #region ================================================== GOLD STORE MANAGER

            goldStoreUpgradesLevels.Clear();
            for (int i = 0; i < 7; i++)
                goldStoreUpgradesLevels.Add(0);

        #endregion




        #region ================================================== RESOURCE NODES

            currentNodeStep = 1;
            resourceNodesThatAreBuilt.Clear();
            for (int i = 0; i < 4; i++)
            {
                if(i==0) resourceNodesThatAreBuilt.Add(true);
                else resourceNodesThatAreBuilt.Add(false);
            }

        #endregion
    }
}
