
using TMPro;
using UnityEngine;

public class CoreAndFragmentsManager : MonoBehaviour , IDataPersistence
{
    [SerializeField] private int coreQuantity;              // Armazena a quantidade de núcleos.
    [SerializeField] private int fragmentsQuantity;         // Armazena a quantidade de fragmentos.
    [SerializeField] private int fragmentsTreshold;         // Armazena o número de fragmentos necessários para gerar um núcleo.
    public TextMeshProUGUI fragText;
    public TextMeshProUGUI coreText;
    public static CoreAndFragmentsManager instance;
    private void Awake()
    {
        instance = this;
        coreQuantity = 100000;      // Define a quantidade inicial de núcleos.
        UpdateUi();                 // Atualiza a UI para mostrar os valores iniciais de fragmentos e núcleos.
    }
    private void FragmentsNeed()    // calcula quantos fragmentos são necessários para criar um núcleo.
    {
        fragmentsTreshold = (int)(10 + (Mathf.Pow(coreQuantity , 1.15f)));
        fragmentsTreshold = (int)(Mathf.Ceil(fragmentsTreshold));
    }
    public void GainFragments()     // Método quando jogador ganha fragmentos.
    {
        fragmentsQuantity++;
        FragmentsNeed();
        CreateCoreFromFragments();
        UpdateUi();

    }

    public void UpdateUi()  //atualiza a UI.
    {
        fragText.text = fragmentsQuantity.ToString();   // Atualiza o texto da quantidade de fragmentos.
        coreText.text = coreQuantity.ToString();        // Atualiza o texto da quantidade de núcleos.
    }

    private void CreateCoreFromFragments()  //verifica se o jogador tem fragmentos suficientes para criar um núcleo.
    {
        if(fragmentsQuantity >= fragmentsTreshold)
        {
            coreQuantity++;
            fragmentsQuantity = 0;  // Reseta a quantidade de fragmentos.
        }
    }

    public void SpendFragments(int amount)
    {
        fragmentsQuantity -= amount;    // Subtrai a quantidade de fragmentos gasta.
    }

    public void SpendCore(int amount)
    {
        coreQuantity -= amount;     // Subtrai a quantidade de núcleos gasta.
    }

    public float GetFragments()
    {
        return fragmentsQuantity;   // Retorna a quantidade de fragmentos.
    }

    public float GetCores()
    {
        return coreQuantity;    // Retorna a quantidade de núcleos.
    }

    public void LoadData(GameData data)
    {
        coreQuantity = data.amountOfCoreQuantity;
        fragmentsQuantity = data.amountOfFragmentsQuantity;
        UIResources.instance.UpdateTextInfo();
    }

    public void SaveData(ref GameData data)
    {
        data.amountOfCoreQuantity = coreQuantity;
        data.amountOfFragmentsQuantity = fragmentsQuantity;
    }
}
