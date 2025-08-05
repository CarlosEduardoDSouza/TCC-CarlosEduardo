using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : MonoBehaviour
{
    public NodeSpecificType nodeType;

    // ================================================
    [SerializeField] private int nodeID;
    public void SetupId(int id) => nodeID = id;
    public int GetId() {return nodeID;}
    // ================================================
    
    public static event Action<ResourceNode> OnResourceGained;  // Evento estático que será invocado quando um recurso for coletado
    [Space]
    [Header("Animations Options")]
    public Vector3 nomalScale;  // Escala normal do objeto
    public Vector3 bigScale;    // Escala maior usada para efeito de clique
    MyTimer nodeTimer;
    [HideInInspector] public bool hasClicked;
    [Space]
    [Header("Other Options")]
    public bool createTextOnlyOnClick;
    [HideInInspector] public bool IsBuilted;
    [SerializeField] private Image timeBar;
    [SerializeField] private Animator frogWorker;
    bool resetAnimation;
    

    public void Awake()
    {
        nomalScale = new Vector3(1, 1,  1);
        bigScale = new Vector3(1.5f, 1.5f, 1.5f);
        nodeTimer = new MyTimer();

        //Linka o evento do timer à função de ganhar recurso
        nodeTimer.OnTimerEnd += GainResourceFromType;

        resetAnimation = true;
    }

    private void Start() 
    {
        //Inicializa o timer com tempo e seta se ele esta em looping
        nodeTimer.InitializeTimer(Library.GET_TIME(nodeType), true);
    }

    public void Update()
    {
        if(!IsBuilted) return;

        //Roda a contagem regressiva do timer
        nodeTimer.TickTimer(Time.deltaTime);
        timeBar.fillAmount = nodeTimer.GetFillAmout();

        if(nodeTimer.GetRemainingTimeInSeconds() < 0.35f && resetAnimation == true)
        {
            frogWorker.Play("PurpleAttack");
            resetAnimation = false;
        }
    }

    private void OnMouseDown()   // Método chamado quando o objeto é clicado
    {
        if(!IsBuilted) return;

        hasClicked = true;

        // Animation
        // transform.DOScale(bigScale, 0.05f).OnComplete(() =>     // Animação para aumentar e depois retornar à escala normal
        // {
        //     transform.DOScale(nomalScale, 0.05f);   // Retorna à escala original após o aumento
        // });

        GainResourceFromType();
    }

    private void OnMouseUp() => hasClicked = false;

    private void GainResourceFromType()
    {
        OnResourceGained?.Invoke(this);
        resetAnimation = true;
    }

    public void SetisBuilded(bool value)
    {
        IsBuilted = value;
    }
}
