using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowards : BTNode
{
    Transform destination;
    float moveSpeed;
    NavMeshAgent agent;

    public MoveTowards(Blackboard _blackboard) : base(_blackboard)
    {
    }

    public override NoteStatus OnEnter()
    {
        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        //blackboard.SetValue("destination", null);
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        destination = (Transform)blackboard.GetValue("destination");
        moveSpeed = (float)blackboard.GetValue("moveSpeed");
        agent = (NavMeshAgent)blackboard.GetValue("agent");
        agent.speed = moveSpeed;
        agent.destination = destination.position;
        TextMeshPro text = (TextMeshPro)blackboard.GetValue("text");
        text.text = "MoveTowards " + destination.name;
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance)
        {
            moveSpeed = 0;
            status = NoteStatus.SUCCESS;
        }
        else
        {
            moveSpeed = (float)blackboard.GetValue("moveSpeed");
            if (agent.gameObject.name == "AllyNinja")
            {
                Debug.Log("Move Towards Running" + agent.gameObject.name);
            }
            status = NoteStatus.RUNNING;
        }
        return status;
    }
}
