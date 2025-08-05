using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTierUI : MonoBehaviour
{
    [SerializeField] private List<SkillButton> skillButtonList;
    [SerializeField] private TextMeshProUGUI tierNumberText;
    [SerializeField] private Image tierImage;

    public List<SkillButton> GetSkillButtonsList()
    {
        return skillButtonList;
    }

    public void SetupTierNumberText(int value)
    {
        tierNumberText.text = value.ToString();
    }

    public void SetTierImage()
    {
        tierImage.color = new Color(1,1,1,0.5f);
    }
    
}
