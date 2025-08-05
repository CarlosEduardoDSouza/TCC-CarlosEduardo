using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeVisualManager : MonoBehaviour
{
    public static NodeVisualManager instance;   //permitindo acesso global à classe//
    private void Awake() 
    {
        instance = this;
    }

    [Header("Levels & Milestones")]     // Cabeçalho no Inspector do Unity para organizar os atributos //
    public int forestryMilestone = 10;      // Número de níveis necessários para alterar a aparência visual do "forestry" //
    public int quaryMilestone = 10;         // Número de níveis necessários para alterar a aparência visual do "quary" //
    public int forestryVisualLevel;     // Nível visual atual para "forestry" //
    public int quaryVisualLevel;     // Nível visual atual para "quary" //
    [Space][Space][Space][Space]


    [Header("Visual GameObjects Lists")]
    public List<GameObject> forestryVisuals = new List<GameObject>();   // Lista de representações visuais de "forestry" //
    [Space]
    public List<GameObject> quaryVisuals = new List<GameObject>();  // Lista de representações visuais de "quary" //
    [Space]
    public List<GameObject> huntCabinVisuals = new List<GameObject>();  // Lista de representações visuais de "hunt cabin" //
    [Space]
    public List<GameObject> oreMineVisuals = new List<GameObject>();    // Lista de representações visuais de "ore mine" //


    public void UpdateVisual(NodeSpecificType types)    // Método para atualizar a aparência visual //
    {
        // switch (types)
        // {
        //     case NodeSpecificType.wood:
        //         int level = ResourceStoreManager.instance.upgradesInStore.Where(item => item.newName == "Forestry").LastOrDefault().level;
        //         if(level % forestryMilestone == 0 && level != 0)   // Verifica se o nível atual de "forestry" atingiu o marco para mudar //
        //         {
        //             forestryVisualLevel++;
        //             for (int i = 0; i < forestryVisuals.Count; i++)
        //                 forestryVisuals[i].SetActive(false);    // Desativa todos os objetos visuais da lista //

        //             if (forestryVisualLevel < forestryVisuals.Count)        // Ativa o objeto visual correspondente ao nível atual //
        //                 forestryVisuals[forestryVisualLevel].SetActive(true);
        //             else
        //                 forestryVisuals[forestryVisuals.Count-1].SetActive(true);   // Se o nível for maior que a quantidade de visuais, ativa o último //
        //         }
        //         // if(ResourceNodeLevels.instance.currentQuaryLevel % quaryMilestone == 0) // Verifica se o nível atual de "quary" atingiu o marco para mudar //
        //         // {
        //         //     quaryVisualLevel++;
        //         //     for (int i = 0; i < quaryVisuals.Count; i++)
        //         //         quaryVisuals[i].SetActive(false);   // Desativa todos os objetos visuais da lista //

        //         //     if (quaryVisualLevel < quaryVisuals.Count)
        //         //         quaryVisuals[quaryVisualLevel].SetActive(true); // Ativa o objeto visual correspondente ao nível atual //
        //         //     else
        //         //         quaryVisuals[quaryVisuals.Count-1].SetActive(true); // Se o nível for maior que a quantidade de visuais, ativa o último //
        //         // }
        //     break;

        // }
    }
}
