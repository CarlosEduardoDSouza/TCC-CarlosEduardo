using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SkillTreeManager : BaseStore, IDataPersistence
{
    public static SkillTreeManager instance;
    private void Awake() 
    {
        instance = this;
    }
    // Pontos iniciais do jogador para gastar na árvore de habilidades
    public int playerPoints = 10;

    // Array para armazenar todas as habilidades da árvore
    // private Skill[] skills;
    // [SerializeField] private List<Skill> skillList = new();
    // [SerializeField] private List<SkillTierUI> skillTierList = new();
    [SerializeField] private List<SkillTree> trees = new();

    // Inicialização da árvore de habilidades

    private void SetupTrees()
    {
        foreach (SkillTree skillTree in trees)
        {
            foreach (Skill _skill in skillTree.skillList)
            {
                // Injeta as skills dentro dos tiers corretos
                // Faz tbm a linkagem dos botões
                InjectSkillInTier(_skill, skillTree);
            }

            foreach (SkillTierUI _skillTierList in skillTree.tierList)
            {
                //Seta o estado dos botões para não setados
                for (int i = 0; i < _skillTierList.GetSkillButtonsList().Count; i++)
                    if (_skillTierList.GetSkillButtonsList()[i].GetSetupState() == false)
                        _skillTierList.GetSkillButtonsList()[i].gameObject.SetActive(false);
            }
        }
    }

    private void InjectSkillInTier(Skill _skill, SkillTree tree)
    {
        SkillButton skillButton = null;

        // Pega a lista de botões dentro do tier
        for (int i = 0; i < tree.tierList[_skill.tierNumber].GetSkillButtonsList().Count; i++)
        {
            // Acha o botão disponivel
            if(tree.tierList[_skill.tierNumber].GetSkillButtonsList()[i].GetSetupState() == false)
            {
                skillButton = tree.tierList[_skill.tierNumber].GetSkillButtonsList()[i];
                break;
            }
        }

        //Muda o icone do botão para o icone da skill
        skillButton.GetIcon().sprite = _skill.GetIcon();
        skillButton.button.onClick.AddListener( ()=> BuySkill(_skill, tree));

        // Muda a cor do botão se a skill esta disponivel para comprar
        // Todas as skills estão disponiveis para a compra no Tier 0 (primeiro tier)
        if(_skill.tierNumber == 0)
        {
            _skill.isUnlocked = true;
            skillButton.SetUnlockedColor();
        }

        // Injeta a skill de referencia dentro do botão para checagem posterior
        skillButton.SetReferenceSkill(_skill);
        // Coloca o botão como setado
        skillButton.SetSetupState(true);

    }

    // Método para comprar uma habilidade
    public void BuySkill(Skill _skill, SkillTree tree)
    {
        // Procura a habilidade com o nome especificado
        Skill skill = tree.skillList.Where( item => item.skillName == _skill.skillName ).FirstOrDefault();

        // Tenta comprar a habilidade, verificando se o jogador tem pontos suficientes e se os pré-requisitos foram cumpridos
        if (skill != null && tree.TryUnlockSkill(ref playerPoints, _skill))
        {
            Debug.Log($"Habilidade {skill.skillName} comprada!");
            tree.InvestedSkillPoints_Increase();
            tree.TryUnlockTier();
            CheckPrerequisitesFromAllSkills();

            // Get the button where the skill is equatl to the injected skill
            SkillButton skillButton = tree.tierList[_skill.tierNumber]
                .GetSkillButtonsList().Where(btn => btn.GetReferenceSkill() == _skill).FirstOrDefault();

            skillButton.SetPurchasedColor();
            skillButton.skillLevel.text = (int.Parse(skillButton.skillLevel.text)+1).ToString();
        }
        else
        {
            Debug.Log($"Não foi possível comprar a habilidade {skill.skillName}."); // Mensagem de falha
        }
    }

    // Checa o prerequisito para as skills
    // Roda cada botão da arvore e desbloqueia para compra caso o prerequisito esteja desbloqueado
    private void CheckPrerequisitesFromAllSkills()
    {
        foreach (SkillTree tree in trees)
        {
            foreach (SkillTierUI _skillTiers in tree.tierList)
            {
                for (int i = 0; i < _skillTiers.GetSkillButtonsList().Count; i++)
                {
                    SkillButton _skillButton = _skillTiers.GetSkillButtonsList()[i];

                    if(_skillButton.GetReferenceSkill() != null && tree.CheckIfSkillCanBeUnlocked(_skillButton.GetReferenceSkill())
                            && !_skillButton.GetReferenceSkill().isPurchased)
                    {
                        _skillButton.SetUnlockedColor();
                    }
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        foreach (SkillTree tree in trees)
        {
            foreach (Skill _skill in tree.skillList)
            {
                _skill.isUnlocked = false;
                _skill.isPurchased = false;
            }
        }

        SetupTrees();
    }

    public void SaveData(ref GameData data)
    {
    }
}
