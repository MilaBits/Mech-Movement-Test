using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Equipment : ScriptableObject {
    [HideInInspector] public MechController mech;
    [BoxGroup("Equipment Base")] public string Name;

    [BoxGroup("Equipment Base"), InlineEditor(InlineEditorModes.LargePreview)]
    public GameObject Model;

    public void InitializeModel(Transform position) {
        Instantiate(Model, position);
    }
}