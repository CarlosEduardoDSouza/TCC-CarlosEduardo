using UnityEngine;

public class IdleSlayerState : BaseState
{
    public override void OnStateEnter()
    {
        // Bota todo mundo para andar
        MoveManager.instance.SetAllMovement(true);

        // Começar o spawn de inimigos
        // Levar a Camera para cima (Onde o personagem Knight esta)
        Camera.main.transform.position = new Vector3(4.7f, 173, -10);
        Camera.main.orthographicSize = 7;
    }

    public override void OnStateExit()
    {
        // Pausar todo movimento
        MoveManager.instance.SetAllMovement(false);
        // Travar o spawn
        
    }

    public override void OnStateStay()
    {

        // Parar os timers quando algo tiver em cima do spawn
        if(SpawnPointBuffer.instance.hasSomethingInside) return;

        EnemySpawnManager.instance.TickTimer();
        ChestGeneration.instance.Ticktimer();
    }
}
