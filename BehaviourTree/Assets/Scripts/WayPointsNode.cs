using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointsNode : BTNode
{
    List<Transform> wayPoints;
    NavMeshAgent agent;
    int currentWayPointIndex;

    public WayPointsNode(Blackboard _blackboard) : base(_blackboard)
    {
        currentWayPointIndex = 0;
    }

    public override NoteStatus OnEnter()
    {
        wayPoints = (List<Transform>)blackboard.GetValue("wayPoints");
        agent = (NavMeshAgent)blackboard.GetValue("agent");
        Transform destination = (Transform)blackboard.GetValue("destination");
        if (destination == null)
        {
            currentWayPointIndex = 0;
        }
        else if (destination == (Transform)blackboard.GetValue("enemy"))
        {
            destination = wayPoints[currentWayPointIndex];
        }
        else if (destination == wayPoints[currentWayPointIndex] && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Count)
            {
                currentWayPointIndex = 0;
            }
        }
        blackboard.SetValue("destination", wayPoints[currentWayPointIndex]);
        agent.stoppingDistance = 0.2f;
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
