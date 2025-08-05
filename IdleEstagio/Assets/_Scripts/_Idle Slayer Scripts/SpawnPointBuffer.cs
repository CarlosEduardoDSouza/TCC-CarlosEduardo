using UnityEngine;

public class SpawnPointBuffer : MonoBehaviour
{
    public bool hasSomethingInside;
    public static SpawnPointBuffer instance;
    private void Awake() 
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        hasSomethingInside = true;
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {
        hasSomethingInside = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        hasSomethingInside = false;
    }
}
