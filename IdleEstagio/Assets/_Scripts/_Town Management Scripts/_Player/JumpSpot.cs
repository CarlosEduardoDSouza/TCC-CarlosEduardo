using DG.Tweening;
using UnityEngine;

public class JumpSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Vector2 distance = other.GetComponent<FollowCamera>().distance;

        if(distance.y > 0.5f)
        {
            DoJumpAnimation(Vector3.up, 1, other.transform, true);
        }

        if(distance.y < 0.5f)
        {
            DoJumpAnimation(-Vector3.up, 1, other.transform, false);
        }
        
        Debug.Log(other.name);
    }

    public void DoJumpAnimation(Vector3 newPosition, float time, Transform other, bool isJump)
    {
        Vector3 newPos = other.position + newPosition;
        
        if(isJump)
            other.DOMoveY(newPos.y, time/2).SetEase(Ease.OutBack);
        else
            other.DOMoveY(newPos.y, time/2).SetEase(Ease.InSine);
    }
}
