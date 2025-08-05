using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This class holds many stats, and the functions to grab and increment the stats
[CreateAssetMenu(fileName = "Stats", menuName = "Stats", order = 0)]
[Serializable]
public class Stats : ScriptableObject
{
    [SerializeField] private List<Stat> statsList = new();
    public List<Stat> GetStatList()
    {
        return statsList;
    }
    
    public float GetStat(Enum_Stats type)
    {
        Stat s = statsList.Where(x => x.type == type).FirstOrDefault();

        if(s == null) 
            return 0;
        else 
            return s.GetValue();
    }

    public void IncrementStat(Stat stat)
    {
        Stat s = statsList.Where(x => x.type == stat.type).FirstOrDefault();

        if(s == null)
        {
            s = CreateNewStatBasedOn(stat);
        }

        s.baseValuePercentageMultiplier += stat.baseValue/100;
        s.baseValueIncrement += stat.baseValueIncrement;
    }

    private Stat CreateNewStatBasedOn(Stat stat)
    {
        Stat s;
        Stat newStat = new();

        newStat.baseValue = stat.baseValue;
        newStat.type = stat.type;
        newStat.baseValueIncrement = stat.baseValueIncrement;
        newStat.baseValuePercentageMultiplier = stat.baseValuePercentageMultiplier;

        statsList.Add(newStat);

        Debug.LogWarning("Non existing Stat, created a new one");

        s = newStat;
        return s;
    }
}


