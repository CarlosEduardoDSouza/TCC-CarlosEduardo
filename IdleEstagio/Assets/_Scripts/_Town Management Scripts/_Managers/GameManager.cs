using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    IdleSlayerState slayerState = new IdleSlayerState();
    TownState townState = new TownState();
    BaseState currentState;
    public string currentStateName = "";
    public Button changeStateButton;
    public bool isChanging;

    public GameObject SwordImg;
    public GameObject TownImg;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        changeStateButton.onClick.AddListener(() => {StartCoroutine(ToggleState());});
    }

    private IEnumerator ToggleState()
    {

        if(isChanging == false)
        {
            isChanging = true;
            UIManager.instance.FadeIn();

            yield return new WaitForSeconds(0.1f);

            if(CompareState("IdleSlayerState"))
            {
                ChangeState(townState);
                changeStateButton.GetComponentInChildren<TextMeshProUGUI>().text = "Overworld";
            }
            else
            {
                ChangeState(slayerState);
                changeStateButton.GetComponentInChildren<TextMeshProUGUI>().text = "Village";
            }

            UIManager.instance.FadeOut();

            yield return new WaitForSeconds(1f);
            isChanging = false;
        }
        
    }

    private void Start() 
    {
        ChangeState(townState);
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
        currentStateName = newState.ToString();
        ChangeImgButtom();
    }

    private void Update() 
    {
        currentState?.OnStateStay();
    }

    public bool CompareState(String stateName)
    {
        switch(stateName)
        {
            case "IdleSlayerState":
            return currentState == slayerState;

            case "TownState":
            return currentState == townState;
        }

        return false;
    }
    public void ChangeImgButtom()
    {
        if (currentState == slayerState)
        {
            TownImg.SetActive(true);
            SwordImg.SetActive(false);
        }
        else
        {
            TownImg.SetActive(false);
            SwordImg.SetActive(true);
        }

    }
}
