using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Skill", order = 0)]
public class Skill : ScriptableObject
{
    //Nome da habilidade
    public string skillName;
    public int tierNumber;
    //Custo em pontos para desbloquear a habilidade
    public int cost = 1;
    //Se a habilidade está desbloqueada e disponível para compra
    public bool isUnlocked;

    //Se a habilidade já foi comprada
    public bool isPurchased;
    //Lista de habilidades que são pré-requisitos para desbloquear esta habilidade
    public Skill[] prerequisitesList;
    //Inicializar a habilidade com nome, custo e pré-requisitos
    [Space]
    [Header("UI")]
    [SerializeField] private Sprite icon;
    // [SerializeField] private Sprite unlockedImage;

    private void OnValidate() 
    {
        skillName = name;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    //verificar se todos os pré-requisitos estão comprados e se a habilidade pode ser desbloqueada
    // public bool CanUnlock()
    // {
    //     if (isPurchased) return false; // Se já está comprada, não pode ser comprada novamente
    //     // Verifica se todas as habilidades pré-requisito foram compradas
    //     foreach (Skill skill in prerequisitesList)
    //     {
    //         if (!skill.isPurchased)
    //             return false; // Se qualquer pré-requisito não está comprado, retorna falso
    //     }

    //     isUnlocked = true;
    //     return true; //Se todos os pré-requisitos foram comprados, retorna verdadeiro
    // }

    //Método para tentar desbloquear e comprar a habilidade, descontando pontos do jogador
    // public bool UnlockSkill(ref int playerPoints)
    // {
    //     // Verifica se a habilidade pode ser desbloqueada e se o jogador tem pontos suficientes
    //     if (CanUnlock() && playerPoints >= cost)
    //     {
    //         playerPoints -= cost;  // Deduz o custo dos pontos do jogador
    //         isPurchased = true;    // Marca a habilidade como comprada
    //         isUnlocked = true;     // Marca a habilidade como desbloqueada
    //         return true;           // Retorna verdadeiro indicando que a compra foi bem-sucedida
    //     }
    //     return false;              // Se a compra falhou, retorna falso
    // }

    
}