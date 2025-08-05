using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ResourceNode))]
public class BuildResourceNode : MonoBehaviour
{
    [SerializeField] private GameObject destroyedParent;
    [SerializeField] private GameObject nodeVisuals;
    [SerializeField] private GameObject nodeCanvas;
    [SerializeField] private Button buildButton;
    [SerializeField] private float buildingCost;
    [Space]
    [Header("Vfx Options")]
    [SerializeField] private GameObject buildVfx;
    [SerializeField] private float offsetY;

    private ResourceNode nodeScript;

    private void Awake() 
    {
        nodeVisuals.SetActive(false);
        destroyedParent.SetActive(true);
        nodeCanvas.SetActive(false);
        buildButton.onClick.AddListener(BuildNode);
        
        buildButton.gameObject.GetTextMeshPro().text = buildingCost.ToString("F2");

        nodeScript = GetComponent<ResourceNode>();

        //Se o node for o primeiro node do jogo, ele vai comerçar ativo
        if(nodeScript.GetId() == 0)
        {
            destroyedParent.SetActive(false);
            nodeVisuals.SetActive(true);
            nodeCanvas.SetActive(true);
            nodeScript.SetisBuilded(true);
            DoVisualAnimations();
            ResourceBuildList.instance.OnNodeBought();
        }
    }


    private async void BuildNode()
    {
        if(CurrencyManager.Instance.SpendCurrency(CurrencyType.buildingMaterial, buildingCost))
        {
            destroyedParent.SetActive(false);
            CreateVfx();
            nodeVisuals.SetActive(true);
            nodeCanvas.SetActive(true);
            nodeScript.SetisBuilded(true);
            DoVisualAnimations();
            ResourceBuildList.instance.OnNodeBought();
        }
    }

    public void BuildNodeAfterLoad()
    {
        nodeScript = GetComponent<ResourceNode>();
        destroyedParent.SetActive(false);
        // CreateVfx();
        nodeVisuals.SetActive(true);
        nodeCanvas.SetActive(true);
        nodeScript.SetisBuilded(true);
        // DoVisualAnimations();
        // ResourceBuildList.instance.OnNodeBought();
    }

    private void CreateVfx()
    {
        var position = new Vector3 (transform.position.x, transform.position.y + offsetY, transform.position.z);
        Instantiate(buildVfx, position, Quaternion.identity);
    }

    private async Task DoVisualAnimations()
    {
        List<float> previousScale = new();
        foreach (Transform child in nodeVisuals.transform)
        {
            previousScale.Add(child.localScale.x);
            child.localScale = Vector3.zero;
        }

        foreach (Transform child in nodeVisuals.transform)
        {

            child.DOScale(previousScale[0] ,1).SetEase(Ease.OutSine);
            previousScale.RemoveAt(0);
            await Task.Delay(Random.Range(10,100));
        }
    }
}
