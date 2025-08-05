using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;

    //Prefab que vai ser usado
    public GameObject floatingPrefab_Icon;
    public GameObject floatingPrefab_NoIcon;
    public Queue<GameObject> floatingQueue = new Queue<GameObject>();    // Fila que armazena os objetos de texto flutuante //

    [Space]
    [Header("Global Sprites")]
    [SerializeField] public Sprite goldSprite;
    [SerializeField] public Sprite oreSprite;
    [SerializeField] public Sprite foodSprite;
    [SerializeField] public Sprite buildingMatSprite;
    [SerializeField] public Sprite fragmentsSprite;
    [SerializeField] public Sprite monsterCoreSprite;

    private void Awake() 
    {
        Instance = this;
    }

    public void SpawnFloatingText(ResourceNode node, float amount, CurrencyType type)      // Método responsável por exibir o texto flutuante quando um recurso for coletado //
    {
        // Remove o primeiro objeto de texto flutuante da fila para reutilizá-lo //
        GameObject textObject = GetTextFromQueue();
        if (node.createTextOnlyOnClick && !node.hasClicked) return;
        textObject.GetTextMeshPro().text = "+" + amount.ToString();
        ChangeSprite(type, textObject);
        textObject.SetActive(true);     // Ativa o objeto para ser exibido. //
        AnimateText(node.transform, textObject);

    }

    public void SpawnFloatingText(Transform EnemyPosition, CurrencyType type)      // Método responsável por exibir o texto flutuante quando um recurso for coletado //
    {
        // Remove o primeiro objeto de texto flutuante da fila para reutilizá-lo //
        GameObject textObject = GetTextFromQueue();
        textObject.GetTextMeshPro().text = "Crit";
        textObject.SetActive(true);     // Ativa o objeto para ser exibido. //
        ChangeSprite(type, textObject);
        AnimateText(EnemyPosition, textObject);
        
    }

    public void SpawnFloatingText(Transform transform, Transform parent, float amount, CurrencyType type, String formatType = "F2")      // Método responsável por exibir o texto flutuante quando um recurso for coletado //
    {
        // Remove o primeiro objeto de texto flutuante da fila para reutilizá-lo //
        GameObject textObject = GetTextFromQueue();
        textObject.GetTextMeshPro().text = "+" + amount.ToString(formatType);
        textObject.SetActive(true);     // Ativa o objeto para ser exibido. //

        if(parent != null)
            textObject.transform.SetParent(parent);

        ChangeSprite(type, textObject);
        AnimateText(transform, textObject);
    }

    public void SpawnFloatingText(Transform EnemyPosition, string text)      // Método responsável por exibir o texto flutuante quando um recurso for coletado //
    {
        // Remove o primeiro objeto de texto flutuante da fila para reutilizá-lo //
        GameObject textObject = GetTextFromQueue();
        textObject.GetTextMeshPro().text = text;
        textObject.SetActive(true);     // Ativa o objeto para ser exibido. //
        AnimateText(EnemyPosition, textObject);

    }

    private void AnimateText(Transform spawnPosition, GameObject textObject)
    {
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomY = UnityEngine.Random.Range(1f, 2f);
        textObject.transform.position = new Vector3(spawnPosition.transform.position.x + randomX, spawnPosition.transform.position.y + randomY, 0);
        textObject.transform.DOMoveY(textObject.transform.position.y + randomY, 1.45f).OnComplete(() =>
        {
            textObject.SetActive(false);
            textObject.transform.SetParent(null);
            ReturnTextToQueue(textObject);
        });
        textObject.transform.DOPunchScale(new Vector3(1.15f, 1.15f, 1.15f), 0.35f, 5, 0.25f);
    }

    private void ReturnTextToQueue(GameObject obj)
    {
        floatingQueue.Enqueue(obj);
    }

    private GameObject GetTextFromQueue()
    {
        GameObject textObject;

        if (floatingQueue.Count <= 0)
            textObject = Instantiate(floatingPrefab_Icon);
        else
            textObject = floatingQueue.Dequeue();

        if(textObject == null)
        {
            Debug.LogError("TEXT OBJECT EMPTY - CREATING A NEW ONE");
            textObject = Instantiate(floatingPrefab_Icon);
        }

        return textObject;
    }

    private void ChangeSprite(CurrencyType type, GameObject textObject)
    {
        switch (type)
        {
            case CurrencyType.gold:
                textObject.GetComponentInChildren<Image>().sprite = goldSprite;
                break;

            case CurrencyType.food:
                textObject.GetComponentInChildren<Image>().sprite = foodSprite;
                break;

            case CurrencyType.buildingMaterial:
                textObject.GetComponentInChildren<Image>().sprite = buildingMatSprite;
                break;

            case CurrencyType.ore:
                textObject.GetComponentInChildren<Image>().sprite = oreSprite;
                break;
            
            case CurrencyType.fragments:
                textObject.GetComponentInChildren<Image>().sprite = fragmentsSprite;
                break;
            
            case CurrencyType.monsterCore:
                textObject.GetComponentInChildren<Image>().sprite = monsterCoreSprite;
                break;

            default:
                textObject.GetComponentInChildren<Image>().sprite = null;
                break;
        }
    }
}
