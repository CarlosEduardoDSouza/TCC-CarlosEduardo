using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public GameObject dialogBox; 
    public TMP_Text questText; 
    public Button deliverButton; 

    public int requiredGold = 10;
    public int requiredFood = 10;
    public int requiredMaterials = 10;

    private int currentGold = 0;
    private int currentFood = 0;
    private int currentMaterials = 0;

    private bool isPlayerNearby = false;

    private void Start()
    {
        dialogBox.SetActive(false);
        deliverButton.onClick.AddListener(DeliverQuest);
        UpdateQuestText();
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            dialogBox.SetActive(true);
            UpdateQuestText();
        }
        else
        {
            dialogBox.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        isPlayerNearby = true;
    }

    private void OnMouseExit()
    {
        isPlayerNearby = false;
    }

    private void UpdateQuestText()
    {
        questText.text =
            $"<color={(currentGold < requiredGold ? "red" : "black")}>Ouro {currentGold}/{requiredGold}</color>\n" +
            $"<color={(currentFood < requiredFood ? "red" : "black")}>Comida {currentFood}/{requiredFood}</color>\n" +
            $"<color={(currentMaterials < requiredMaterials ? "red" : "black")}>Materiais {currentMaterials}/{requiredMaterials}</color>";
    }

    private void DeliverQuest()
    {
        if (currentGold >= requiredGold && currentFood >= requiredFood && currentMaterials >= requiredMaterials)
        {
            currentGold -= requiredGold;
            currentFood -= requiredFood;
            currentMaterials -= requiredMaterials;
            UpdateQuestText();
            Debug.Log("Missão concluída! Parabéns!");

            // adicionar efeitos de partículas ou outras recompensas
        }
        else
        {
            Debug.Log("Você não tem os recursos necessários para concluir a missão.");
        }
    }

    public void AddResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Gold":
                currentGold += amount;
                break;
            case "Food":
                currentFood += amount;
                break;
            case "Materials":
                currentMaterials += amount;
                break;
        }

        UpdateQuestText();
    }
}
