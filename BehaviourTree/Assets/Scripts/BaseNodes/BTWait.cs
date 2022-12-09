using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTNode
{
    float currentWaitTime;
    float maxWaitTime;

    public BTWait(Blackboard _blackboard, float waitTime) : base(_blackboard)
    {
        maxWaitTime = waitTime;
    }

    public override NoteStatus OnEnter()
    {
        currentWaitTime = 0;
        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        currentWaitTime = 0;
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        currentWaitTime += Time.deltaTime;
        if (currentWaitTime >= maxWaitTime)
        {
            return NoteStatus.SUCCESS;
        }
        return NoteStatus.RUNNING;
    }
}