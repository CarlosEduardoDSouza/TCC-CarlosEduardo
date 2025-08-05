using UnityEngine;

public class EnemyMoveX : MoveBase
{
    void Update()
    {
        if(!MoveManager.instance.isMoving) return;

        transform.position -= new Vector3(speed,0,0) * Time.deltaTime;
    }
}
