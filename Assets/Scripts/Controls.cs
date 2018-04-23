using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public class Controls : ScriptableObject {
    public InputMethod inputMethod;

    public List<GameInput> Inputs = new List<GameInput>();

    private void OnValidate() {
        foreach (var input in Inputs) {
            if (input.CopyInputName) {
                input.Ps4 = input.InputName + "_Ps4";
                input.Xbox = input.InputName + "_Xbox";
                input.Keyboard = input.InputName + "_Keyboard";
            }
        }
    }

    [Serializable]
    public class GameInput {
        public String InputName;

        [Header("Inputs")] public bool CopyInputName;
        public String Ps4;
        public String Xbox;
        public String Keyboard;
    }

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
                        break;
                    case InputMethod.Keyboard:
                        return Input.GetAxis(gameInput.Keyboard);
                        break;
                    case InputMethod.Xbox:
                        return Input.GetAxis(gameInput.Xbox);
                        break;
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
                        break;
                    case InputMethod.Keyboard:
                        return Input.GetButton(gameInput.Keyboard);
                        break;
                    case InputMethod.Xbox:
                        return Input.GetButton(gameInput.Xbox);
                        break;
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
                        break;
                    case InputMethod.Keyboard:
                        return Input.GetButtonUp(gameInput.Keyboard);
                        break;
                    case InputMethod.Xbox:
                        return Input.GetButtonUp(gameInput.Xbox);
                        break;
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
                        break;
                    case InputMethod.Keyboard:
                        return Input.GetButtonDown(gameInput.Keyboard);
                        break;
                    case InputMethod.Xbox:
                        return Input.GetButtonDown(gameInput.Xbox);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("inputMethod", inputMethod, null);
                }
            }
        }

        return false;
    }
}