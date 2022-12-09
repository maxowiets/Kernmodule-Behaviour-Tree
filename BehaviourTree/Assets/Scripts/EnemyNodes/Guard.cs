using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Patrolling
    public List<Transform> wayPoints = new List<Transform>();

    //Attacking
    public LayerMask playerLayer;
    public float attackSpeed;

    //Get Weapon
    public LayerMask weaponLayer;
    public List<Transform> weaponsList = new List<Transform>();

    public float moveSpeed;
    public float perceptionRange;
    public LayerMask obstacleLayer;
    public TextMeshPro text;
    public TextMeshProUGUI enemyCloseText;
    public TextMeshProUGUI inAttackRangeText;

    Blackboard blackboard;
    BTNode fullTree;
    BTNode patrolTree;
    BTNode attackTree;
    BTNode getWeaponTree;

    private void Start()
    {
        //initialize blackboard
        Blackboard blackboard = new Blackboard();
        blackboard.SetValue("wayPoints", wayPoints);
        blackboard.SetValue("enemyLayer", playerLayer);
        blackboard.SetValue("attackSpeed", attackSpeed);
        blackboard.SetValue("weaponLayer", weaponLayer);
        blackboard.SetValue("weaponsList", weaponsList);
        blackboard.SetValue("moveSpeed", moveSpeed);
        blackboard.SetValue("perceptionRange", perceptionRange);
        blackboard.SetValue("obstacleLayer", obstacleLayer);
        blackboard.SetValue("agent", GetComponent<NavMeshAgent>());
        blackboard.SetValue("transform", transform);
        blackboard.SetValue("text", text);
        blackboard.SetValue("enemyCloseText", enemyCloseText);
        blackboard.SetValue("inAttackRangeText", inAttackRangeText);

        patrolTree =
            new ParallelNode(
                blackboard,
                new InvertNode(
                    blackboard, 
                    new IsEnemyClose(blackboard)
                    ),
                new ParallelSequenceNode(
                    blackboard,
                    new WayPointsNode(blackboard),
                    new MoveTowards(blackboard)
                    )
                );

        attackTree =
            new ParallelNode(
                blackboard,
                new IsEnemyClose(blackboard),
                new ParallelSelectorNode(
                    blackboard,
                    new ParallelNode(
                        blackboard,
                        new InAttackRange(blackboard),
                        new Attack(blackboard)
                        ),
                    new ParallelSequenceNode(
                        blackboard,
                        new SetDestination(blackboard, "enemy"),
                        new MoveTowards(blackboard)
                        )
                    )
                );

        getWeaponTree =
            new SequenceNode(
                blackboard,
                new IsEnemyClose(blackboard),
                new InvertNode(
                    blackboard,
                    new HasWeapon(blackboard)
                    ),
                new GetClosestWeapon(blackboard),
                new MoveTowards(blackboard),
                new PickUpWeapon(blackboard)
                );

        fullTree =
            new SelectorNode(
                blackboard,
                getWeaponTree,
                attackTree,
                patrolTree
                );
    }

    private void FixedUpdate()
    {
        fullTree?.Update();
    }
}
