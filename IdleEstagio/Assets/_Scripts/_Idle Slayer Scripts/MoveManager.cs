using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager instance;
    public List<MoveBase> allMovableEntities = new List<MoveBase>();
    public bool isMoving {get; private set;}

    public void AddMovableEntityToList(MoveBase entity)
    {
        allMovableEntities.Add(entity);
    }

     public void RemoveMovableEntityToList(MoveBase entity)
    {
        allMovableEntities.Remove(entity);
    }

    private void Awake() 
    {
        instance = this;
        allMovableEntities = FindObjectsOfType<MoveBase>().ToList();
        PlayerSlayer.CanMove += SetAllMovement;
    }

    public void SetAllMovement(bool isMoving)
    {
        this.isMoving = isMoving;
        for (int i = 0; i < allMovableEntities.Count; i++)
        {
            allMovableEntities[i].SetIsMoving(isMoving);
        }
    }
}
