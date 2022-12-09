using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromGuard : BTNode
{
    List<Transform> hidingSpots;
    Transform gameObjectTransform;
    float moveSpeed;
    public HideFromGuard(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        moveSpeed = (float)blackboard.GetValue("moveSpeed");
        hidingSpots = (List<Transform>)blackboard.GetValue("hidingSpots");
        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        List<Transform> possibleHidingSpots = new List<Transform>();
        //CHECK WHICH HIDING SPOT IS SUITABLE
        foreach (Transform hidingSpot in hidingSpots)
        {

        }

        //GET CLOSEST HIDING SPOT
        Transform closestHidingSpot;
        if (possibleHidingSpots.Count > 0)
        {
            closestHidingSpot = possibleHidingSpots[0];
            foreach (Transform possibleHidingSpot in possibleHidingSpots)
            {
                if (Vector3.Distance(possibleHidingSpot.position, gameObjectTransform.position) < Vector3.Distance(closestHidingSpot.position, gameObjectTransform.position))
                {
                    closestHidingSpot = possibleHidingSpot;
                }
            }

            //WALK TO HIDINGSPOT
            gameObjectTransform.position = Vector3.MoveTowards(gameObjectTransform.position, closestHidingSpot.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(gameObjectTransform.position, closestHidingSpot.position) <= 0.1f)
            {
                status = NoteStatus.SUCCESS;
            }
            else
            {
                status = NoteStatus.RUNNING;
            }
            return status;
        }
        status = NoteStatus.FAILURE;
        return status;
    }
}
