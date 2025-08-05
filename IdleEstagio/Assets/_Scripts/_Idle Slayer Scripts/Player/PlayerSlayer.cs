using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSlayer : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayerMask;
    public static event Action<bool> CanMove;
    EnemyTakeDamage currentEnemy = null;

    CancellationTokenSource _tokenSource;

    // Função que faz o personagem atacar o inimigo
    // Funcina tanto a animação como o dano em si
    private async Task AttackEnemy()
    {
        _tokenSource = new CancellationTokenSource();
        var token = _tokenSource.Token;

        // Enquanto o inimigo estiver proximo do personagem
        while(PlayerAnimationController.instance.IsEnemyNearby)
        {
            if(token.IsCancellationRequested) 
            {
                Debug.Log("Cancelled");
                return;
            }

            PlayerAnimationController.instance.isPlayerAttacking = true;
            PlayerAnimationController.instance.DoAttackAnimation();
            //Delay de 300 ms = 0.3 segundos
            await Task.Delay(3 * 100);

            // Se houver inimigos dentro dessa variavel, ele vai tomar dano
            if(currentEnemy != null)
                currentEnemy.TakeDamage();

            // Delay de 2 segundos 2000 ms
            await Task.Delay(2 * 1000);
        }
    }

    private void Update()
    {
        // Linha imaginaira que acerta o inimigo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1, enemyLayerMask);

        if (hit)
        {
            Debug.DrawRay(transform.position, Vector2.right * 1, Color.green);

            // Guarda o inimigo dentro da variavel currentEnemy
            currentEnemy = hit.collider.gameObject.GetComponent<EnemyTakeDamage>();
            // Seta o estado para InimigoProximo
            PlayerAnimationController.instance.IsEnemyNearby = true;

            // Se eu n estiver atacando atualmente, começa uma nova rotina de ataque
            if (!PlayerAnimationController.instance.isPlayerAttacking)
            {
                // Manda um evento para todo mundo saber que a hora de se mexer acabou, agora devo atacar
                CanMove?.Invoke(false);
                AttackEnemy();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * 1, Color.red);

            if (PlayerAnimationController.instance.isPlayerAttacking)
            {
                // O ataque acabou, pode mandar o evento para todos voltarem a mexer
                CanMove?.Invoke(true);
                // Cancela a task
                _tokenSource.Cancel();
                // Seta o estado de Atacando para false
                PlayerAnimationController.instance.isPlayerAttacking = false;

            }

            // Seta false porque n tem nenhum inimigo proximo
            PlayerAnimationController.instance.IsEnemyNearby = false;
        }

        ReturnToOptimalPoint();

    }

    private void ReturnToOptimalPoint()
    {
        var multiplier = Mathf.Abs(transform.position.x - 1.4f) * 3;

        if (transform.position.x < 1.3f)
        {
            transform.position += new Vector3(1.4f, 0, 0) * Time.deltaTime * multiplier;
        }
        else if (transform.position.x > 1.5f)
        {
            transform.position += new Vector3(-1.4f, 0, 0) * Time.deltaTime * multiplier;
        }
    }
}
