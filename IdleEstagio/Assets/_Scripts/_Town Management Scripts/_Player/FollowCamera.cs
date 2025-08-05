using UnityEngine;
using UnityEngine.AI;

public class FollowCamera : MonoBehaviour
{
    public LayerMask tileLayer;
    private Vector2 destination;
    public Vector2 distance;
    public Vector2 variableDistance;
    // public NavMeshAgent agent;

    // private void Start() 
    // {
    //     agent.updateRotation = false;
	// 	agent.updateUpAxis = false;
    // }
    
    private void Update() 
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, -Camera.main.transform.up, 15, tileLayer);
        
        if(hit)
        {
            destination = hit.point;

            distance = destination - (Vector2)transform.position;
            variableDistance = new Vector2(distance.x, 0) / 500;

            transform.position += (Vector3)variableDistance;
        }

    }
}
