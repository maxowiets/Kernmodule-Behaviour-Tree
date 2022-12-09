using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasWeapon : ConditionNode
{
    public HasWeapon(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        if (blackboard.GetValue("weapon") != null)
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
