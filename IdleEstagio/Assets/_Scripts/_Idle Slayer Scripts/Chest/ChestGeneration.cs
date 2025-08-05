using Unity.Mathematics;
using UnityEngine;

public class ChestGeneration : MonoBehaviour
{
    //Modificador - Tipo - Nome
    [SerializeField] private GameObject _goldChestPrefab;
    
    [SerializeField] private GameObject _ironChestPrefab;
    
    [SerializeField] private GameObject _woodChestPrefab;

    //Atributo - Modificador - Tipo - Nome
    [SerializeField] private Transform spawnPoint;
    private MyTimer chestSpawnTimer = new MyTimer();

    //Modificar de acesso - Tipo - Nome
    public static ChestGeneration instance;
    [SerializeField] private float spawnPercentage = 3;

    private void Awake() 
    {
        instance = this;
    }
    private void Start() 
    {
        chestSpawnTimer.InitializeTimer(1f, true);
        chestSpawnTimer.OnTimerEnd += SpawnChest;
    }

    public void Ticktimer() => chestSpawnTimer.TickTimer(Time.deltaTime);

    // Modificador - Tipo - Nome
    private void SpawnChest()
    {
        if(ChestSpawnRoll())
        {
            var roll = UnityEngine.Random.Range(0f, 100f);
            GameObject spawnedChest;
            

            if(roll <= 5)
            {
                spawnedChest = Instantiate(_goldChestPrefab, spawnPoint.position, quaternion.identity);
            }
            else if(roll <= 10)
            {
                spawnedChest = Instantiate(_ironChestPrefab, spawnPoint.position, quaternion.identity);
            }
            else
            {
                spawnedChest = Instantiate(_woodChestPrefab, spawnPoint.position, quaternion.identity);
            }

            // GameObject currentChest;
            // currentChest = roll <= 5 ? _goldChestPrefab : roll <= 10 ? _ironChestPrefab : _woodChestPrefab;
            // spawnedChest = Instantiate(currentChest, spawnPoint.position, quaternion.identity);
            MoveManager.instance.AddMovableEntityToList(spawnedChest.GetComponentInChildren<MoveBase>());
        }
    }

    private bool ChestSpawnRoll()
    {
        if(UnityEngine.Random.Range(0f, 100f) <= spawnPercentage) return true;
        else return false;
    }

}
