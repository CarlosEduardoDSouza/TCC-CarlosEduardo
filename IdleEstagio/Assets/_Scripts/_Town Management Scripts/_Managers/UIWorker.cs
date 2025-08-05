using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWorker : BaseStore
{
    public static UIWorker instance;

    [SerializeField] private List<WorkerUIClass> workersUI = new();
    [SerializeField] private WorkerGeneration workerGeneration; 
    [SerializeField] private Button rollButton; 

    public void Awake()
    {
        instance = this;

        // Configura o botão, se estiver atribuído
        if (rollButton != null)
        {
            rollButton.onClick.AddListener(RollWorkers); // Liga o método ao clique do botão
        }
        else
        {
            Debug.LogWarning("Botão de roll não está configurado no Inspector.");
        }
    }

    public void UpdateWorkerList(List<Worker> _workers)
    {
        for (int i = 0; i < workersUI.Count; i++)
        {
            workersUI[i].workerCostText.text = _workers[i].Cost.ToString("f2");
            workersUI[i].workerBonusText.text = _workers[i].ProductionBonus.ToString("f2") + "%";
            workersUI[i].workerTypeText.text = _workers[i].Type.ToString();
            workersUI[i].SetUpTier(_workers[i].tier - 1);
            workersUI[i].workerSpeedText.text = _workers[i].ProductionSpeed.ToString("f2") + "%";
        }
    }

    public void RollWorkers()
    {
        if (workerGeneration != null)
        {
            workerGeneration.GenerateInitialWorkers(); // Gera novos trabalhadores
            Debug.Log("Trabalhadores atualizados!");
        }
        else
        {
            Debug.LogWarning("Referência ao WorkerGeneration não está atribuída no Inspector.");
        }
    }
}
