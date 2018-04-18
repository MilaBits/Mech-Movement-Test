using System;
using System.Collections.Generic;
using HardShellStudios.CompleteControl;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public class Controls : ScriptableObject {
    public ControllerType controllerType;

    public List<GameInput> Inputs = new List<GameInput>();

    [Serializable]
    public class GameInput {
        public String Name;
        public AxisCode Ps4;
        public AxisCode XBox;
        public KeyCode Pc;
    }

    [MenuItem("Assets/Create/Controls")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Controls>();
    }

    public void UpdateControls(Slider slider) {
        controllerType = (ControllerType) slider.value;
        Debug.Log(slider.value);
        Debug.Log(controllerType);
    }

    public enum ControllerType {
        Ps4,
        Xbox,
        Keyboard
    }
}