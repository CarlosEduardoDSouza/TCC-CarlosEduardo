using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("MovingRight", true);
            anim.SetBool("MovingLeft", false);
            anim.SetBool("IsMoving", true);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("MovingLeft", true);
            anim.SetBool("MovingRight", false);
            anim.SetBool("IsMoving", true);
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("IsMoving", false);
            anim.SetBool("MovingRight", false);
            anim.SetBool("MovingLeft", false);
        }
    }
}
