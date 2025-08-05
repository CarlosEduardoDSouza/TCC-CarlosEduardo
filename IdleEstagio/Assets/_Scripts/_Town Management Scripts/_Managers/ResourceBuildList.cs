using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceBuildList : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<ResourceNode> nodesInSequence = new List<ResourceNode>();
    [SerializeField] private int currentNodeStep;
    public static ResourceBuildList instance;
    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        ShowAvailableNodes();
    }

    public void OnNodeBought()
    {
        currentNodeStep++;
        ShowAvailableNodes();
    }

    private void ShowAvailableNodes()
    {
        //Roda a lista de nodes para ver quais estão desbloqueados e desbloqueia eles
        for (int i = 0; i < nodesInSequence.Count; i++)
        {
            if(nodesInSequence[i].GetId() <= currentNodeStep)
            {
                nodesInSequence[i].gameObject.SetActive(true);
            }
            else
            {
                nodesInSequence[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetupIds()
    {
        for (int i = 0; i < nodesInSequence.Count; i++)
        {
            nodesInSequence[i].SetupId(i);
            Debug.Log("ID's injected");
        }
    }

    public void LoadData(GameData data)
    {
        for (int i = 0; i < nodesInSequence.Count; i++)
        {
            nodesInSequence[i].IsBuilted = data.resourceNodesThatAreBuilt[i];
            
            if(nodesInSequence[i].IsBuilted == true) 
                nodesInSequence[i].GetComponent<BuildResourceNode>().BuildNodeAfterLoad();
        }

        currentNodeStep = data.currentNodeStep;
        ShowAvailableNodes();
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < nodesInSequence.Count; i++)
        {
            data.resourceNodesThatAreBuilt[i] = nodesInSequence[i].IsBuilted;
        }

        data.currentNodeStep = currentNodeStep;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ResourceBuildList))]
public class ResourceBuildListEditor : Editor {
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        var buildList = target as ResourceBuildList;

        if(GUILayout.Button("Setup Id's"))
        {
            buildList.SetupIds();
        }
        
    }
}
#endif