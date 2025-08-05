using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Controla o Novo jogo, o Save Game, e o Load Game
public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    public static DataPersistenceManager instance{get; private set;}

    private GameData gameData;
    private FileDataHandler fileDataHandler;

    List<IDataPersistence> dataPersistenceObjects = new();

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return dataPersistenceObjects.ToList();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = this.fileDataHandler.Load();

        if(this.gameData == null)
        {
            Debug.LogWarning("No save data found - Initializing default values.");
            NewGame();
        }

        foreach (var item in this.dataPersistenceObjects)
        {
            item.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var item in this.dataPersistenceObjects)
        {
            item.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);

        Debug.LogWarning("Game Saved");
    }

    private void OnApplicationQuit() 
    {
        SaveGame();
    }

}
