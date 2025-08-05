using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityLevelManager : MonoBehaviour
{
    public int CityLevel = 0;
    public int WorkerUnlock= 50;
    [SerializeField] Button btn;

    private void OnValidate()
    {  
        ButtonState();
    }

    void Start()
    {
        ButtonState();
    }

    private void ButtonState()
    {
        if (btn != null)
        {
            bool isActive = CityLevel >= WorkerUnlock;
            btn.gameObject.SetActive(isActive);
        }
    }


}
