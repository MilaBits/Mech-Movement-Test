using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Move : SerializedScriptableObject {
    public IMoveAction Action;

    public Stance EndStance;
    public float EndLag;
    //[InlineEditor] public Move FollowupMove;
    public GameObject HitBox;
    public float Damage;
}