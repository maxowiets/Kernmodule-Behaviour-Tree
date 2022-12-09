using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AllyNinja : MonoBehaviour
{
    Blackboard blackboard;

    BTNode fullTree;

    BTNode followPlayerTree;
    public Transform playerTransform;
    public float stoppingDistance;

    BTNode hideFromEnemy;
    public List<Transform> hidingSpots;
    public Transform enemyTransform;
    public LayerMask obstacleLayerMask;
    public GameObject smokeBomb;

    public float moveSpeed = 1;
    public NavMeshAgent agent;
    public TextMeshPro text;

    private void Start()
    {
        blackboard = new Blackboard();
        blackboard.SetValue("moveSpeed", moveSpeed);
        blackboard.SetValue("player", playerTransform);
        blackboard.SetValue("agent", agent);
        blackboard.SetValue("text", text);
        blackboard.SetValue("hidingSpots", hidingSpots);
        blackboard.SetValue("transform", transform);
        blackboard.SetValue("enemyTransform", enemyTransform);
        blackboard.SetValue("destination", playerTransform);
        blackboard.SetValue("obstacleLayerMask", obstacleLayerMask);
        blackboard.SetValue("hiding", false);
        blackboard.SetValue("smokeBomb", smokeBomb);

        followPlayerTree =
            new ParallelSequenceNode(
                blackboard,
                new InvertNode(
                    blackboard,
                    new IsEnemyChasing(blackboard)
                    ),
                new SetDestination(blackboard, "player"),
                new SetStoppingDistance(blackboard, stoppingDistance),
                new MoveTowards(blackboard)
                );

        hideFromEnemy =
            new ParallelNode(
                blackboard,
                new IsEnemyChasing(blackboard),
                new ParallelSequenceNode(
                    blackboard,
                    new GetClosestHidingSpot(blackboard),
                    new SetStoppingDistance(blackboard, 0.2f),
                    new MoveTowards(blackboard),
                    new SetDebugTextNode(blackboard, "Throwing Smoke"),
                    new BTWait(blackboard, 1f),
                    new ThrowSmokeBomb(blackboard)
                    )
                );


        fullTree =
            new ParallelSelectorNode(
                blackboard,
                hideFromEnemy,
                followPlayerTree
                );
    }

    private void FixedUpdate()
    {
        fullTree?.Update();
    }
}
