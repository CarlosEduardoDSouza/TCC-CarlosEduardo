using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    private int nuclearAbsorptionLevel;
    public TextMeshProUGUI LevelText;

     private void Awake()
    {
           nuclearAbsorptionLevel = 0;
    }
     public void UpdateNuclearAbsorptionLevel()
    {
       
    }
     public void UpdateUi()  //atualiza a UI.
    {
        LevelText.text = nuclearAbsorptionLevel.ToString();   
    }

}
