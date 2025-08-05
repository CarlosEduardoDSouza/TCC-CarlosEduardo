using UnityEngine;

public class TownState : BaseState
{
    public override void OnStateEnter()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 15;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateStay()
    {
        CameraDrag.instance.CheckIfIsPossibleToDrag();
    }
}
