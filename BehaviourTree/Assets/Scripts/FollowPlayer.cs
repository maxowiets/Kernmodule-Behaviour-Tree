using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : BTNode
{
    Transform playerTransform;
    Transform ownTransform;
    float moveSpeed;

    public FollowPlayer(Blackboard _blackboard) : base(_blackboard)
    {
//        playerTransform = _playerTransform;
        moveSpeed = (float)blackboard.GetValue("moveSpeed");
    }

    public override NoteStatus OnEnter()
    {
        ownTransform = (Transform)blackboard.GetValue("transform");

        status = NoteStatus.SUCCESS;
        return status;
    }

    public override NoteStatus OnExit()
    {
        return status;
    }

    public override NoteStatus OnUpdate()
    {
        ownTransform.position = Vector3.MoveTowards(ownTransform.position, playerTransform.transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(playerTransform.position, playerTransform.transform.position) <= 1f)
        {
            return NoteStatus.SUCCESS;
        }
        else
        {
            return NoteStatus.RUNNING;
        }
    }
}
