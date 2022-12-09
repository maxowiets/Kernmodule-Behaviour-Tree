using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetStoppingDistance : BTNode
{
    float stoppingDistance;
    public SetStoppingDistance(Blackboard _blackboard, float _stoppingDistance) : base(_blackboard)
    {
        stoppingDistance = _stoppingDistance;
    }

    public override NoteStatus OnEnter()
    {
        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        blackboard.SetValue("stoppingDistance", stoppingDistance);
        NavMeshAgent agent = (NavMeshAgent)blackboard.GetValue("agent");
        agent.stoppingDistance = stoppingDistance;
        return status;
    }
}
