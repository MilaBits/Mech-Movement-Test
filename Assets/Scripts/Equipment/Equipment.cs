using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Equipment : ScriptableObject {
    [HideInInspector] public MechController mech;
    [BoxGroup("Equipment Base")] public string Name;

    [BoxGroup("Equipment Base"), InlineEditor(InlineEditorModes.LargePreview), PropertyOrder(100), LabelText("Equipment Model")]
    public GameObject Model;

    public void InitializeModel(Transform position) {
        Instantiate(Model, position);
    }
}