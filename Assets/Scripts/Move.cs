using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Move : SerializedScriptableObject {
    //public IMoveAction Action;
    public string debugMessage;

    [BoxGroup("Stats")] public float Damage;

    [BoxGroup("Stats/Elemental", false)] public Element Element;
    [BoxGroup("Stats/Elemental")] public float ElementStrength;

    [BoxGroup("Stats/Post-move", false)] public float moveTime;
    [BoxGroup("Stats/Post-move")] public Stance EndStance;
    [BoxGroup("Stats/Post-move")] public float EndLag;

    [BoxGroup("Stats/HitBox")] public Hitbox HitBox;
    [BoxGroup("Stats/HitBox")] public float hitBoxLifetime;
    [BoxGroup("Stats/HitBox")] public Vector3 HitBoxPosition;

    public void ActivateHitbox(string ownerTag, Transform parent, Vector3 position) {
        Hitbox hitBox = Instantiate(HitBox, parent);
        hitBox.transform.localPosition = position;

        hitBox.Damage = this.Damage;
        hitBox.Element = this.Element;
        hitBox.ElementStrength = this.ElementStrength;

        hitBox.OwnerTag = ownerTag;
    }

    public void Execute() {
        Debug.Log(debugMessage);
    }
}