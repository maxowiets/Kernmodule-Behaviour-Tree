using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsHidden : ConditionNode
{
    NavMeshAgent agent;
    public IsHidden(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        Debug.Log("TEst");
        agent = (NavMeshAgent)blackboard.GetValue("agent");
        if (agent.velocity == Vector3.zero)
        {
            status = NoteStatus.SUCCESS;
        }
        else
        {
            status = NoteStatus.FAILURE;
        }
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
