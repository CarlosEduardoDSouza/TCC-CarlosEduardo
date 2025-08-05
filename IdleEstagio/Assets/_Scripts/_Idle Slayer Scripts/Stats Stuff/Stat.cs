using System;


// Base class for stats, it contains the type of stat and the amount or current stat amount
[Serializable]
public class Stat
{
    public Enum_Stats type;
    public float baseValue;
    public float baseValueIncrement;
    public float baseValuePercentageMultiplier = 1;

    public float GetValue()
    {
        return (baseValue+baseValueIncrement) * baseValuePercentageMultiplier;
    }
}