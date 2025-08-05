using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 100;
    public LayerMask groundLayer;
    Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {  
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0,0.85f,0), Vector2.down, 0.1f, groundLayer);

        if(hit)
        { 
            Debug.DrawRay(transform.position - new Vector3(0,0.85f,0), Vector2.down*0.1f, Color.green);
            PlayerAnimationController.instance.canJump = true;
        }
        else
        {
            Debug.DrawRay(transform.position - new Vector3(0,0.85f,0), Vector2.down*0.1f, Color.red);
            PlayerAnimationController.instance.canJump = false;
        }

        if(Input.GetMouseButtonDown(0) && PlayerAnimationController.instance.canJump && !PlayerAnimationController.instance.isPlayerAttacking
            && !PlayerAnimationController.instance.IsEnemyNearby && GameManager.instance.CompareState("IdleSlayerState"))
        {
            rb.AddForce(new Vector2(0,jumpPower));
        }
    }
}
