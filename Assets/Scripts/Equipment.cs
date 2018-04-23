using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject {
    [HideInInspector] public MechController mech;
    public string Name;
    public GameObject Model;

    public void InitializeModel(Transform position) {
        Instantiate(Model, position);
    }
}