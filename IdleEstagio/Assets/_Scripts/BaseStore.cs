using UnityEngine;
using UnityEngine.UI;

public class BaseStore : MonoBehaviour
{
    // [SerializeField] protected Button openCloseButton;
    [SerializeField] protected bool isOpen;
    [SerializeField] protected GameObject storeObject;

    public void OpenCloseStore()
    {
        if(isOpen)
        {
            storeObject.SetActive(false);
            isOpen = false;
        }
        else
        {
            storeObject.SetActive(true);
            isOpen = true;
        }
    }
}
