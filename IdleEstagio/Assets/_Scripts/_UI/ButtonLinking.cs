using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonLinking : MonoBehaviour
{
    public ButtonType buttonType;
    Button btn;

    // Link the button to onClick event
    private void Start() 
    {
        btn = GetComponent<Button>();

        switch (buttonType)
        {
            case ButtonType.goldStore:
                btn.onClick.AddListener( () => GoldStoreManager.instance.OpenCloseStore());
            break;

            case ButtonType.monsterCoreStore:
                btn.onClick.AddListener( () => MonsterCoreStoreManager.instance.OpenCloseStore());
            break;

            case ButtonType.skillTrees:
                btn.onClick.AddListener( () => SkillTreeManager.instance.OpenCloseStore());
            break;

            case ButtonType.workerStore:
                btn.onClick.AddListener( () => UIWorker.instance.OpenCloseStore());
            break;

        }
    }
}
