using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class DualMove : SerializedScriptableObject {
    public string debugMessage;

    public List<Move> RequiredMoves;

//    public void ActivateHitbox(string ownerTag, Transform parent, Vector3 position) {
//        Hitbox hitBox = Instantiate(HitBox, parent);
//        hitBox.transform.localPosition = position;
//
//        hitBox.Damage = this.Damage;
//        hitBox.Element = this.Element;
//        hitBox.ElementStrength = this.ElementStrength;
//
//        hitBox.OwnerTag = ownerTag;
//    }
//
//    public void Execute() {
//        Debug.Log(debugMessage);
//    }
}