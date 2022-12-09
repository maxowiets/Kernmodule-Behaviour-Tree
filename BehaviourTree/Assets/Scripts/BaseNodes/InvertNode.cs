using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertNode : BTNode
{
    BTNode child;

    public InvertNode(Blackboard _blackboard, BTNode _child) : base(_blackboard)
    {
        child = _child;
    }

    public override NoteStatus OnEnter()
    {
        status = NoteStatus.FAILURE;
        return status;
    }

    public override NoteStatus OnExit()
    {
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        switch (child.Update())
        {
            case NoteStatus.SUCCESS:
                status = NoteStatus.FAILURE;
                break;
            case NoteStatus.FAILURE:
                status = NoteStatus.SUCCESS;
                break;
            case NoteStatus.RUNNING:
                status = NoteStatus.RUNNING;
                break;
        }
        return status;
    }
}
