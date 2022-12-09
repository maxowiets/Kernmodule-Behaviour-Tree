using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetDebugTextNode : BTNode
{
    string debugText;
    TextMeshPro text;
    public SetDebugTextNode(Blackboard _blackboard, string _debugText) : base(_blackboard)
    {
        debugText = _debugText;
    }

    public override NoteStatus OnEnter()
    {
        text = (TextMeshPro)blackboard.GetValue("text");
        text.text = debugText;
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
