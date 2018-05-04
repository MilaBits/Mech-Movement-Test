using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Controls : ScriptableObject {
    [EnumToggleButtons, HideLabel] public InputMethod inputMethod;

    public List<GameInput> Inputs = new List<GameInput>();

    private SerializedProperty axisArray;

    private void OnValidate() {
        GetAllAxes();
        foreach (var input in Inputs) {
            if (input.CopyInputName) {
                input.Ps4 = input.InputName + "_Ps4";
                input.Xbox = input.InputName + "_Xbox";
                input.Keyboard = input.InputName + "_Keyboard";
            }

            input.ps4Invalid = !inputExists(input.Ps4);
            input.Ps4Message = "Input " + input.Ps4 + " does not yet exist";

            input.XboxInvalid = !inputExists(input.Xbox);
            input.XboxMessage = "Input " + input.Xbox + " does not yet exist";

            input.KeyboardInvalid = !inputExists(input.Keyboard);
            input.KeyboardMessage = "Input " + input.Keyboard + " does not yet exist";
        }
    }

    [Serializable]
    public class GameInput {
        [FoldoutGroup("$InputName"), LabelWidth(110)]
        public String InputName;

        [FoldoutGroup("$InputName"), LabelWidth(110)]
        public bool CopyInputName;

        [FoldoutGroup("$InputName/Unity Inputs", false), LabelWidth(65)]
        [InfoBox("$Ps4Message", InfoMessageType.Error, "ps4Invalid")]
        public string Ps4;

        [HideInInspector] public string Ps4Message;
        [HideInInspector] public bool ps4Invalid;

        [FoldoutGroup("$InputName/Unity Inputs", false), LabelWidth(65)]
        [InfoBox("$XboxMessage", InfoMessageType.Error, "XboxInvalid")]
        public string Xbox;

        [HideInInspector] public string XboxMessage;
        [HideInInspector] public bool XboxInvalid;

        [FoldoutGroup("$InputName/Unity Inputs", false), LabelWidth(65)]
        [InfoBox("$KeyboardMessage", InfoMessageType.Error, "KeyboardInvalid")]
        public string Keyboard;

        [HideInInspector] public string KeyboardMessage;
        [HideInInspector] public bool KeyboardInvalid;
    }

    [Button("Open Input Manager", ButtonSizes.Medium)]
    private void OpenInputManager() {
        EditorApplication.ExecuteMenuItem("Edit/Project Settings/Input");
    }

    public bool inputExists(string input) {
        for (int i = 0; i < axisArray.arraySize; ++i) {
            var axis = axisArray.GetArrayElementAtIndex(i);

            if (input.Equals(axis.FindPropertyRelative("m_Name").stringValue))
                return true;
        }

        return false;
    }

    public void GetAllAxes() {
        var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

        SerializedObject obj = new SerializedObject(inputManager);

        axisArray = obj.FindProperty("m_Axes");

        if (axisArray.arraySize == 0)
            Debug.Log("No Axes");

//        for (int i = 0; i < axisArray.arraySize; ++i) {
//            var axis = axisArray.GetArrayElementAtIndex(i);
//
//            var name = axis.FindPropertyRelative("m_Name").stringValue;
//            var axisVal = axis.FindPropertyRelative("axis").intValue;
//            var inputType = (InputType) axis.FindPropertyRelative("type").intValue;
//
//        }
    }

    public enum InputType {
        KeyOrMouseButton,
        MouseMovement,
        JoystickAxis,
    };

    [MenuItem("Assets/ReadInputManager")]
    public static void DoRead() { }

    [MenuItem("Assets/Create/Game/Controls")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Controls>();
    }

    public void UpdateControls(Slider slider) {
        inputMethod = (InputMethod) slider.value;
        Debug.Log(slider.value);
        Debug.Log(inputMethod);
    }

    public enum InputMethod {
        Ps4,
        Keyboard,
        Xbox
    }

    public float GetAxis(string input) {
        GameInput gameInput = null;
        foreach (var current in Inputs) {
            if (input.Equals(current.InputName)) {
                gameInput = current;
                switch (inputMethod) {
                    case InputMethod.Ps4:
                        return Input.GetAxis(gameInput.Ps4);
                    case InputMethod.Keyboard:
                        return Input.GetAxis(gameInput.Keyboard);
                    case InputMethod.Xbox:
                        return Input.GetAxis(gameInput.Xbox);
                    default:
                        throw new ArgumentOutOfRangeException("inputMethod", inputMethod, null);
                }
            }
        }

        return 0;
    }

    public bool GetButton(string input) {
        GameInput gameInput = null;
        foreach (var current in Inputs) {
            if (input.Equals(current.InputName)) {
                gameInput = current;
                switch (inputMethod) {
                    case InputMethod.Ps4:
                        return Input.GetButton(gameInput.Ps4);
                    case InputMethod.Keyboard:
                        return Input.GetButton(gameInput.Keyboard);
                    case InputMethod.Xbox:
                        return Input.GetButton(gameInput.Xbox);
                    default:
                        throw new ArgumentOutOfRangeException("inputMethod", inputMethod, null);
                }
            }
        }

        return false;
    }

    public bool GetButtonUp(string input) {
        GameInput gameInput = null;
        foreach (var current in Inputs) {
            if (input.Equals(current.InputName)) {
                gameInput = current;
                switch (inputMethod) {
                    case InputMethod.Ps4:
                        return Input.GetButtonUp(gameInput.Ps4);
                    case InputMethod.Keyboard:
                        return Input.GetButtonUp(gameInput.Keyboard);
                    case InputMethod.Xbox:
                        return Input.GetButtonUp(gameInput.Xbox);
                    default:
                        throw new ArgumentOutOfRangeException("inputMethod", inputMethod, null);
                }
            }
        }

        return false;
    }

    public bool GetButtonDown(string input) {
        GameInput gameInput = null;
        foreach (var current in Inputs) {
            if (input.Equals(current.InputName)) {
                gameInput = current;
                switch (inputMethod) {
                    case InputMethod.Ps4:
                        return Input.GetButtonDown(gameInput.Ps4);
                    case InputMethod.Keyboard:
                        return Input.GetButtonDown(gameInput.Keyboard);
                    case InputMethod.Xbox:
                        return Input.GetButtonDown(gameInput.Xbox);
                    default:
                        throw new ArgumentOutOfRangeException("inputMethod", inputMethod, null);
                }
            }
        }

        return false;
    }
}