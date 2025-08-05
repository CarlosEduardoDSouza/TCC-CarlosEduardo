using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [SerializeField] List<Tab> tabList = new();
    
    private void Awake() 
    {
        foreach (Tab tab in tabList)
        {
            tab.tabButton.onClick.AddListener( () => { OpenTab(tab); } );
        }
    }

    private void OpenTab(Tab _tab)
    {
        //Close all tabs
        foreach (Tab tab in tabList)
            tab.SetViewport(false);

        //Open the specific tab that was clicked
        _tab.SetViewport(true);
    }

}


[Serializable]
public class Tab
{
    public string name;
    public Button tabButton;
    public GameObject linkedViewport;

    public void SetViewport(bool value) => linkedViewport.SetActive(value);
}
