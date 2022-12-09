using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelSequenceNode : BTNode
{
    BTNode[] children;
    int index = 0;
    public ParallelSequenceNode(Blackboard _blackboard, params BTNode[] _children) : base(_blackboard)
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
                    index = children.Length;
                    foreach (BTNode child in children)
                    {
                        child.OnExit();
                    }
                    status = NoteStatus.FAILURE;
                    break;
                case NoteStatus.RUNNING:
                    status = NoteStatus.RUNNING;
                    Debug.Log("RETURNING RUNNING");
                    return status;
                case NoteStatus.SUCCESS:
                    status = NoteStatus.SUCCESS;
                    break;
            }
        }
        index = 0;
        return status;
    }
}
