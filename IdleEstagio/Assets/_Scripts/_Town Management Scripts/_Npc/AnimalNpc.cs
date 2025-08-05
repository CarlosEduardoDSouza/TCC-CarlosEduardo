using System.Collections;
using UnityEngine;

public class AnimalNpc : MonoBehaviour
{
    public Vector3 movementDirection;
    Vector3 newPosition;
    public Vector2 bounds;
    Vector3 leftBounds;
    Vector3 rightBounds;
    bool isMovementLocked;
    MyTimer stopTimer = new MyTimer();

    private void Start() 
    {
        stopTimer.InitializeTimer(Random.Range(1f, 5f), true);
        stopTimer.OnTimerEnd += LockMovement;
    }

    void LockMovement()
    {
        isMovementLocked = true;
        StartCoroutine(UnlockMovement());
    }

    IEnumerator UnlockMovement()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        isMovementLocked = false;

        if(Random.Range(0f, 100f) >= 50)
            movementDirection = Vector2.left;
        else
            movementDirection = Vector2.right;
    }

    private void Update()
    {
        if(isMovementLocked == true) return;

        stopTimer.TickTimer(Time.deltaTime);

        newPosition = transform.position + movementDirection * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.x >= rightBounds.x)
            movementDirection = Vector2.left;

        else if (transform.position.x <= leftBounds.x)
            movementDirection = Vector2.right;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;

        leftBounds = new Vector3(bounds.x, transform.position.y, transform.position.z);
        rightBounds = new Vector3(bounds.y, transform.position.y, transform.position.z);

        Gizmos.DrawWireCube(leftBounds, new Vector3(0.5f,2,2));
        Gizmos.DrawWireCube(rightBounds, new Vector3(0.5f,2,2));
    }
}