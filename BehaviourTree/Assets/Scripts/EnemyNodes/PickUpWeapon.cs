using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : BTNode
{
    Transform ownTransform;
    LayerMask weaponLayerMask;

    public PickUpWeapon(Blackboard _blackboard) : base(_blackboard)
    {

    }

    public override NoteStatus OnEnter()
    {
        ownTransform = (Transform)blackboard.GetValue("transform");
        weaponLayerMask = (LayerMask)blackboard.GetValue("weaponLayer");
        Collider[] col = Physics.OverlapSphere(ownTransform.position, 1f, weaponLayerMask);

        if (col.Length > 0)
        {
            if (col[0].GetComponent<Weapon>())
            {
                blackboard.SetValue("weapon", col[0].GetComponent<Weapon>());
                col[0].gameObject.SetActive(false);
            }
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
