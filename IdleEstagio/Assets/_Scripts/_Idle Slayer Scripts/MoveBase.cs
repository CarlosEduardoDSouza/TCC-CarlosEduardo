using UnityEngine;

public class MoveBase : MonoBehaviour
{
    public float speed = 0;
    protected bool isMoving = false;

    private void Start() 
    {
        SetIsMoving(true);
    }
    
    public void SetIsMoving(bool value)
    {
        isMoving = value;
        speed = StatsManager.instance.currentStats.GetStat(Enum_Stats.moveSpeed);
    }
}
