using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class InAttackRange : ConditionNode
{
    float attackRange;
    float rangeIncreaseOnAttack;
    Transform ownTransform;
    public InAttackRange(Blackboard _blackboard) : base(_blackboard)
    {
        rangeIncreaseOnAttack = 0f;
    }

    public override NoteStatus OnEnter()
    {
        ownTransform = (Transform)blackboard.GetValue("transform");
        Transform enemy = (Transform)blackboard.GetValue("enemy");
        Weapon weapon = (Weapon)blackboard.GetValue("weapon");
        attackRange = weapon.range;
        if (Vector3.Distance(ownTransform.position, enemy.position) < attackRange + rangeIncreaseOnAttack)
        {
            TextMeshProUGUI text = (TextMeshProUGUI)blackboard.GetValue("inAttackRangeText");
            text.text = "Is In Attack Range";
            rangeIncreaseOnAttack = 1;
            status = NoteStatus.SUCCESS;
        }
        else
        {
            TextMeshProUGUI text = (TextMeshProUGUI)blackboard.GetValue("inAttackRangeText");
            text.text = "Is Not In Attack Range";
            rangeIncreaseOnAttack = 0;
            status = NoteStatus.FAILURE;
        }
        NavMeshAgent agent = (NavMeshAgent)blackboard.GetValue("agent");
        agent.stoppingDistance = attackRange + rangeIncreaseOnAttack;

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
