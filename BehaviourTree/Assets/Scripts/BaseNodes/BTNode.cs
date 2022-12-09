using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public Blackboard blackboard;
    public BTNode(Blackboard _blackboard)
    {
        blackboard = _blackboard;
    }

    protected NoteStatus status;
    protected bool isInitialized = false;

    public abstract NoteStatus OnEnter();
    public abstract NoteStatus OnUpdate();
    public abstract NoteStatus OnExit();

    public NoteStatus Update()
    {
        if (!isInitialized)
        {
            status = OnEnter();
            isInitialized = true;
        }
        status = OnUpdate();
        if (status != NoteStatus.RUNNING)
        {
            status = OnExit();
            isInitialized = false;
        }
        return status;
    }
}

public enum NoteStatus
{
    SUCCESS,
    FAILURE,
    RUNNING
}