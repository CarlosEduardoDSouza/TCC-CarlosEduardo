using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController instance;
    Animator animator;
    public bool canJump = true;
    public bool isPlayerAttacking;
    public bool IsEnemyNearby;

    private void Awake() 
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void Update() 
    {
        if(!canJump)
        {
            animator.SetBool("CanJump", false);
            animator.SetBool("IsEnemyNearby", false);
            animator.SetBool("isPlayerAttacking", false);
        }
        else if(isPlayerAttacking)
        {
            return;
        }
        else if(!IsEnemyNearby || !isPlayerAttacking)
        {
            animator.SetBool("IsEnemyNearby", false);
            animator.SetBool("isPlayerAttacking", false);
            animator.SetBool("CanJump", true);
        }
    }

    public void DoAttackAnimation()
    {
        string animName = Random.Range(0f,100f) >= 50 ? "Atk1" : 
                          Random.Range(0f,100f) >= 50 ? "Atk2" : "Atk3";

        animator.Play(animName);

        animator.SetBool("isPlayerAttacking", true);
        animator.SetBool("IsEnemyNearby", true);
        animator.SetBool("CanJump", true);
    }

}
