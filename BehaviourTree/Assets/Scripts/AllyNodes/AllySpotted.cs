using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpotted : BTNode
{
    public AllySpotted(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        //if SOMEBOOl is true
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
