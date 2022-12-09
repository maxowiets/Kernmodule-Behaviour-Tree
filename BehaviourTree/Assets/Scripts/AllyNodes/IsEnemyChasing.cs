using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyChasing : ConditionNode
{
    Player player;
    public IsEnemyChasing(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        Transform playerTransform = (Transform)blackboard.GetValue("player");
        player = playerTransform.GetComponent<Player>();

        if (player.gettingChased)
        {
            player.gettingChased = false;
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
