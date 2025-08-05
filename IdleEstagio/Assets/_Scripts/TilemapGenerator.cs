using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private TileBase tile;
    [SerializeField] private int xOffset = 2;
    [SerializeField] private int yOffset = 2;
    [Space]
    [Header("Options")]
    [SerializeField] private int layers = 1;
    [SerializeField] private float contineltalness = 10;
    [SerializeField] private float frequency = 2;
    [SerializeField] private float persistence = 0.5f;
    [SerializeField] private float lacunarity = 1.5f;
    [Range(0f,1f)]
    [SerializeField] private float treshold = 0.5f;
    public float maxNoiseValue = 0;

    private void Start()
    {
        MakeMap();
    }

    private void MakeMap()
    {
        // map.ClearAllTiles();
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                var treshold = GetNoiseValue(i,j);
                if (treshold >= this.treshold)
                    map.SetTile(new Vector3Int(i, j), tile);
                else
                    map.SetTile(new Vector3Int(i, j), null);

                if(j == 19 )
                    map.SetTile(new Vector3Int(i, j), tile);
            }
        }

        maxNoiseValue = 0;
    }

    private float GetNoiseValue(float x, float y)
    {
        float amplitude = 1;
        float frequency = 1;

        float noiseValue = 0;

        // xOffset = (int)transform.position.x;
        // yOffset = (int)transform.position.y;

        for (int i = 0; i < layers; i++)
        {
            var perlinValue = Mathf.PerlinNoise((x + xOffset) / contineltalness * frequency, (y + yOffset) / contineltalness * frequency);
            noiseValue += perlinValue * amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;

            if(noiseValue >= maxNoiseValue) maxNoiseValue = noiseValue;
        }

        return Mathf.InverseLerp(0, maxNoiseValue, noiseValue);;
        
    }

    private void Update() 
    {
        xOffset = (int)transform.position.x;
        yOffset = (int)transform.position.y;
        
    }

    private void OnValidate() 
    {
        MakeMap();
    }
}

