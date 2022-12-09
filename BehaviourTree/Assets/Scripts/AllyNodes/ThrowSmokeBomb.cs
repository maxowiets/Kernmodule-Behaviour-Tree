using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmokeBomb : BTNode
{
    GameObject smokeBomb;
    Transform enemyTransform;
    public ThrowSmokeBomb(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        smokeBomb = (GameObject)blackboard.GetValue("smokeBomb");
        enemyTransform = (Transform)blackboard.GetValue("enemyTransform");
        GameObject.Instantiate(smokeBomb, enemyTransform.position, Quaternion.identity);

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
