using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BTNode
{
    BTNode[] children;
    int index = 0;
    public SequenceNode(Blackboard _blackboard, params BTNode[] _children) : base(_blackboard)
    {
        children = _children;
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
        for (; index < children.Length; index++)
        {
            NoteStatus result = children[index].Update();
            switch (result)
            {
                case NoteStatus.FAILURE:
                    index = 0;
                    return NoteStatus.FAILURE;
                case NoteStatus.RUNNING:
                    return NoteStatus.RUNNING;
                case NoteStatus.SUCCESS:
                    break;
            }
        }
        index = 0;
        return NoteStatus.SUCCESS;
    }
}