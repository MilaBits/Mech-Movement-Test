using System;
using JetBrains.Annotations;
using UnityEngine;

public class Attack {
    public Stance EndStance;
    public float EndLag;
    public Attack FollowupAttack;

    public Attack(Stance endStance, float endLag) : this(endStance, endLag, null) { }

    public Attack(Stance endStance, float endLag, [CanBeNull] Attack followupAttack) {
        EndStance = endStance;
        EndLag = endLag;
        FollowupAttack = followupAttack;
    }
}