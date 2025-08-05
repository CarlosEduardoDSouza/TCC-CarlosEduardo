using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WorkerGeneration : MonoBehaviour
{
    [SerializeField]
    private List<string> workerTypes;  // Tipos possíveis de trabalhadores
    [SerializeField]
    private UIWorker uiWorker;  // Referência ao UIWorker

    private List<Worker> workers = new List<Worker>(); // Lista de trabalhadores ativos
    private const int MaxWorkers = 3; // Máximo de trabalhadores
    private int rollCount = 0;

    private void Start()
    {
        if (workerTypes == null || workerTypes.Count == 0)
            workerTypes = new List<string> { "Miner", "Farmer", "Blacksmith" }; // Tipos padrão

        GenerateInitialWorkers();
    }

    [ContextMenu("Generate Workers")]
    public void GenerateInitialWorkers()
    {
        workers.Clear();  // Limpa a lista de trabalhadores antigos

        // Gera trabalhadores e adiciona à lista
        for (int i = 0; i < MaxWorkers; i++)
        {
            Worker worker = GenerateWorker();
            workers.Add(worker);
        }

        // Chama o método para atualizar a UI com os trabalhadores gerados
        if (uiWorker != null)
            uiWorker.UpdateWorkerList(workers);
        else
            Debug.LogWarning("UIWorker reference is not set in the Inspector.");

        rollCount++;
    }

    private Worker GenerateWorker()
    {
        var tierPercentage = UnityEngine.Random.Range(0f, 100f);

        int workerTier = 1;
        float minProdBonus = 1, maxProdBonus = 5;
        float minSpeedBonus = 1, maxSpeedBonus = 1.5f;
        Vector2 costRange = new(1, 1.5f);
        Vector2 baseCost = new(100, 150f);

        // Determina o tier do trabalhador
        if (tierPercentage <= 25)
        {
            workerTier = 2;
            minProdBonus = 5;
            maxProdBonus = 8;
            costRange = new(1, 2f);
            minSpeedBonus = 1.5f;
            maxSpeedBonus = 2.5f;
            baseCost = new(100, 250f);
        }

        if (tierPercentage <= 5)
        {
            workerTier = 3;
            minProdBonus = 8;
            maxProdBonus = 12;
            costRange = new(1, 2.5f);
            minSpeedBonus = 2.5f;
            maxSpeedBonus = 3.5f;
            baseCost = new(100, 550f);
        }

        var multiplier = UnityEngine.Random.Range(costRange.x, costRange.y);
        var rollCountMultiplier = Mathf.Pow(1.10f, rollCount);


        string type = workerTypes[UnityEngine.Random.Range(0, workerTypes.Count)];
        float productionBonus = UnityEngine.Random.Range(minProdBonus, maxProdBonus);
        float productionSpeed = UnityEngine.Random.Range(minSpeedBonus, maxSpeedBonus);
        float cost = UnityEngine.Random.Range(baseCost.x, baseCost.y) * multiplier * rollCountMultiplier;

        return new Worker(type, productionBonus, cost, productionSpeed, workerTier);
    }
}


[Serializable]
public class Worker
{
    public string Type;  // Tipo do trabalhador
    public float ProductionBonus;  // Bônus de produção (%)
    public float Cost;  // Custo em ouro
    public float ProductionSpeed;  // Velocidade de produção (%)
    public int tier;  // Tier do trabalhador

    public Worker(string type, float productionBonus, float cost, float productionSpeed, int tier)
    {
        Type = type;
        ProductionBonus = productionBonus;
        Cost = cost;
        ProductionSpeed = productionSpeed;
        this.tier = tier;
    }
    public override string ToString()
    {
        return $"Type: {Type}, Production Bonus: {ProductionBonus}%, Cost: {Cost}, Production Speed: {ProductionSpeed}%, Tier: {tier}";
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WorkerGeneration))]
public class WorkerGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorkerGeneration workerGen = (WorkerGeneration)target;

        // Adiciona um botão no Editor para gerar trabalhadores
        if (GUILayout.Button("Generate Workers"))
        {
            workerGen.GenerateInitialWorkers();  // Gera trabalhadores e atualiza a UI
        }
    }
}
#endif