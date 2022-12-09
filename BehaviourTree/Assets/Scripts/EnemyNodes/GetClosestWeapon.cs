using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetClosestWeapon : BTNode
{
    List<Transform> weaponTransforms;
    Transform ownTransform;
    NavMeshAgent agent;

    public GetClosestWeapon(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        ownTransform = (Transform)blackboard.GetValue("transform");
        weaponTransforms = (List<Transform>)blackboard.GetValue("weaponsList");
        agent = (NavMeshAgent)blackboard.GetValue("agent");

        Transform closestWeaponTransform = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < weaponTransforms.Count; i++)
        {
            if (NavMesh.CalculatePath(ownTransform.position, weaponTransforms[i].position, agent.areaMask, path))
            {
                float distance = Vector3.Distance(ownTransform.position, path.corners[0]);
                for (int j = 1; j < path.corners.Length; j++)
                {
                    distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                }
                if (distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    closestWeaponTransform = weaponTransforms[i];
                }
            }
        }
        if (closestWeaponTransform != null)
        {
            blackboard.SetValue("destination", closestWeaponTransform);
        }

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
