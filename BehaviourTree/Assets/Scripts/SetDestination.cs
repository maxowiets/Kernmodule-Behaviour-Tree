using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestination : BTNode
{
    string destinationName;
    public SetDestination(Blackboard _blackboard, string _destinationName) : base(_blackboard)
    {
        destinationName = _destinationName;
    }

    public override NoteStatus OnEnter()
    {
        blackboard.SetValue("destination", blackboard.GetValue(destinationName));
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
