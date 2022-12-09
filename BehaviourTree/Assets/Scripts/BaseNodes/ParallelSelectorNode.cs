using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelSelectorNode : BTNode
{
    BTNode[] children;
    int index = 0;
    public ParallelSelectorNode(Blackboard _blackboard, params BTNode[] _children) : base(_blackboard)
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
        foreach (BTNode child in children)
        {
            child.OnExit();
        }
        index = 0;
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
                    status = NoteStatus.FAILURE;
                    break;
                case NoteStatus.RUNNING:
                    index = children.Length;
                    status = NoteStatus.RUNNING;
                    break;
                case NoteStatus.SUCCESS:
                    index = children.Length;
                    status = NoteStatus.SUCCESS;
                    break;
            }
        }
        index = 0;
        return status;
    }
}
