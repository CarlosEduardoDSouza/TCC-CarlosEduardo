using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrit : MonoBehaviour
{
    float BaseCrit = 1;
    float MultCrit = 1;
    float BaseCritDamage = 2;
    float MultCritDamage = 1; 

    public static PlayerCrit Instance;

    private void Awake() 
    {
        Instance = this;
    }

    public bool RoolCritHit()
    {
        if (UnityEngine.Random.Range(0, 100) <= BaseCrit)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
