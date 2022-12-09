using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDebug : BTNode
{
    string debugText;
    public BTDebug(Blackboard _blackboard, string _debugText) : base(_blackboard)
    {
        debugText = _debugText;
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
        Debug.Log(debugText);
        return status;
    }
}