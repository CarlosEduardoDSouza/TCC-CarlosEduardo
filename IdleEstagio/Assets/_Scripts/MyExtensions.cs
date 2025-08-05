using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class MyExtensions
{
    public static TextMeshProUGUI GetTextMeshPro(this GameObject other)
    {
        if(other == null) return null;
        var component = other.GetComponent<TextMeshProUGUI>() == null ? other.GetComponentInChildren<TextMeshProUGUI>() : other.GetComponent<TextMeshProUGUI>();
        return component;
    }
}
