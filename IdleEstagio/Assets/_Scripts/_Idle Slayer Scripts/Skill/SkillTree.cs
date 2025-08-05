using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public List<Skill> skillList = new();
    public List<SkillTierUI> tierList = new();
    [SerializeField] private int skillPointsInvested;
    private readonly int[] tiersUnlockPoints = Library.tiersUnlockPoints;

    private void Start() 
    {
        for (int i = 0; i < tierList.Count; i++)
        {
            tierList[i].SetupTierNumberText(tiersUnlockPoints[i]);
        }
    }
    public void InvestedSkillPoints_Increase()
    {
        skillPointsInvested++;
    }

    public bool CheckIfSkillCanBeUnlocked(Skill _skill)
    {
        // Se a skill já esta comprada ela pode ser comprada multiplas vezes
        if (_skill.isPurchased)
        {
            Debug.Log($"A Habilidade foi evoluida com sucesso!");
            return true;
        }

        // Verifica se todas as habilidades pré-requisito foram compradas
        foreach (Skill prerequisiteSkill in _skill.prerequisitesList)
        {
            // Se qualquer pré-requisito não está comprado, retorna falso
            if (!prerequisiteSkill.isPurchased)
            {
                Debug.Log($"A Habilidade não possui todos os prerequisitos!");
                return false; 
            }
        }

        // Veja se o tier da skill esta desbloqueado, caso não esteja, não permite a compra
        if(tiersUnlockPoints[_skill.tierNumber] > skillPointsInvested)
        {
            Debug.Log($"O Tier dessa habilidade não esta liberado!");
            return false;
        }
        
        return true;
    }

    public bool TryUnlockSkill(ref int playerPoints, Skill _skill)
    {
        // Verifica se a habilidade pode ser desbloqueada e se o jogador tem pontos suficientes
        if (CheckIfSkillCanBeUnlocked(_skill) && playerPoints >= _skill.cost)
        {
            playerPoints -= _skill.cost;  // Deduz o custo dos pontos do jogador
            _skill.isPurchased = true;    // Marca a habilidade como comprada
            _skill.isUnlocked = true;     // Marca a habilidade como desbloqueada
            return true;           // Retorna verdadeiro indicando que a compra foi bem-sucedida
        }

        Debug.Log($"Você não tem pontos para comprar essa habilidade!");
        return false;              // Se a compra falhou, retorna falso
    }

    public void TryUnlockTier()
    {
        for (int i = 0; i < tierList.Count; i++)
        {
            if(skillPointsInvested >= tiersUnlockPoints[i])
                tierList[i].SetTierImage();
        }
    }
}
