using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mod : Equipment {
    public string ActionName;

    private void Start() { }

    public virtual bool Action() {
        Debug.Log("Default mod");
        return false;
    }

    [MenuItem("Assets/Create/Game/Mod")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Mod>();
    }
}