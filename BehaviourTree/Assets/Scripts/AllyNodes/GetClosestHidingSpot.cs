using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class GetClosestHidingSpot : BTNode
{
    List<Transform> hidingSpots;
    Transform enemyTransform;
    NavMeshAgent agent;
    Transform ownTransform;
    LayerMask obstacleLayerMask;

    public GetClosestHidingSpot(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        hidingSpots = (List<Transform>)blackboard.GetValue("hidingSpots");
        enemyTransform = (Transform)blackboard.GetValue("enemyTransform");
        ownTransform = (Transform)blackboard.GetValue("transform");
        agent = (NavMeshAgent)blackboard.GetValue("agent");
        obstacleLayerMask = (LayerMask)blackboard.GetValue("obstacleLayerMask");

        Transform closestHidingSpot = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < hidingSpots.Count; i++)
        {
            Vector3 vectorToHidingSpot = hidingSpots[i].position - enemyTransform.position;
            if (Physics.Raycast(enemyTransform.position, vectorToHidingSpot.normalized, vectorToHidingSpot.magnitude, obstacleLayerMask))
            {
                if (NavMesh.CalculatePath(ownTransform.position, hidingSpots[i].position, agent.areaMask, path))
                {
                    float distance = Vector3.Distance(ownTransform.position, path.corners[0]);
                    for (int j = 1; j < path.corners.Length; j++)
                    {
                        distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                    }
                    if (distance < closestTargetDistance)
                    {
                        closestTargetDistance = distance;
                        closestHidingSpot = hidingSpots[i];
                    }
                }
            }
        }
        if (closestHidingSpot != null)
        {
            blackboard.SetValue("destination", closestHidingSpot);
        }

        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        return status;
    }
}
