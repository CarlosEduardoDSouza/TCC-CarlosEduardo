using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image unlockedImage;
    [SerializeField] private Image icon;
    public TextMeshProUGUI skillLevel;
    // Referência para a árvore de habilidades
    // public SkillTreeManager skillTree;

    // Referência para o botão na interface
    public Button button;

    private Skill referenceSkill;

    [SerializeField] private bool isSeted;

    public Skill GetReferenceSkill()
    {
        return referenceSkill;
    }

    public void SetReferenceSkill(Skill skill)
    {
        referenceSkill = skill;
    }

    public bool GetSetupState()
    {
        return isSeted;
    }

    public void SetSetupState(bool value)
    {
        isSeted = value;
    }

    public Image GetIcon()
    {
        return icon;
    }

    public void SetLockedColor()
    {
        GetComponent<Image>().color = new Color(0.54f, 0.43f, 0.28f, 1);
        unlockedImage.color = new Color(0, 0, 0, 0.9f);
    }

    public void SetUnlockedColor()
    {
        unlockedImage.color = new Color(0, 0, 0, 0);
    }

    public void SetPurchasedColor()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        unlockedImage.color = new Color(0, 0, 0, 0);
    }
}