using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenuUI : MonoBehaviour
{
    private VisualElement _root;
    
    private Button playGameButton;

    private void Awake() 
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        
        playGameButton = _root.Q<Button>("Playgame");
        playGameButton.RegisterCallback<ClickEvent>(OnPlayGameButtonClicked);
    }

    void OnPlayGameButtonClicked (ClickEvent evt)
    {
        Debug.Log("Play game clicked");
    }
}
