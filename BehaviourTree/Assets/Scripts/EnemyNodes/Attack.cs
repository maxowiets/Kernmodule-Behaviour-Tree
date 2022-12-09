using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attack : BTNode
{
    float currentWaitTime;
    float maxWaitTime;
    Weapon currentWeapon;

    public Attack(Blackboard _blackboard) : base(_blackboard)
    {
        maxWaitTime = (float)blackboard.GetValue("attackSpeed");
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
        TextMeshPro text = (TextMeshPro)blackboard.GetValue("text");
        text.text = "Attacking";

        if (currentWaitTime >= maxWaitTime)
        {
            Transform player = (Transform)blackboard.GetValue("enemy");
            player.gameObject.SetActive(false);
            return NoteStatus.SUCCESS;
        }
        return NoteStatus.RUNNING;
    }
}
