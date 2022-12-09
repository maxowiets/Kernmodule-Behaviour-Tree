using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IsEnemyClose : ConditionNode
{
    LayerMask enemyLayer;
    Transform centerTransform;
    float perceptionRange;
    LayerMask obstacleLayer;

    public IsEnemyClose(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        enemyLayer = (LayerMask)blackboard.GetValue("enemyLayer");
        centerTransform = (Transform)blackboard.GetValue("transform");
        perceptionRange = (float)blackboard.GetValue("perceptionRange");
        obstacleLayer = (LayerMask)blackboard.GetValue("obstacleLayer");

        Transform enemyTransform = null;
        var enemy = Physics.OverlapSphere(centerTransform.position, perceptionRange, enemyLayer);
        if (enemy.Length > 0)
        {
            Transform tempTransform = enemy[0].transform;
            if (!Physics.Raycast(centerTransform.position, (tempTransform.position - centerTransform.position).normalized, (tempTransform.position - centerTransform.position).magnitude, obstacleLayer) 
                && !Physics.Raycast(tempTransform.position, (centerTransform.position - tempTransform.position).normalized, (centerTransform.position - tempTransform.position).magnitude, obstacleLayer))
            {
                enemyTransform = tempTransform;
                enemyTransform.GetComponent<Player>().gettingChased = true;
            }
        }
        blackboard.SetValue("enemy", enemyTransform);
        if (enemyTransform != null)
        {
            TextMeshProUGUI text = (TextMeshProUGUI)blackboard.GetValue("enemyCloseText");
            text.text = "Enemy Is Close";
            status = NoteStatus.SUCCESS;
        }
        else
        {
            TextMeshProUGUI text = (TextMeshProUGUI)blackboard.GetValue("enemyCloseText");
            text.text = "Enemy Is Not Close";
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