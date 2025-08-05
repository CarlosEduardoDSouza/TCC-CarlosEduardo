using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image fadeScreen;
    public static UIManager instance;

    private void Awake() 
    {
        instance = this;
    }
    
    public async Task Fade()
    {
        FadeIn();
        await Task.Delay(100);
        FadeOut();
    }

    public void FadeIn()
    {
        fadeScreen.DOFade(1, 0.1f);
    }

    public void FadeOut()
    {
        fadeScreen.DOFade(0, 0.1f);
    }
}
