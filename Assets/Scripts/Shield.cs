using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Shield : Mod {
    [BoxGroup("$Name")] [InlineEditor(InlineEditorModes.LargePreview)]
    public GameObject ShieldModel;

    [MenuItem("Assets/Create/Game/Shield")]
    public new static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Shield>();
    }

    public override bool Action() {
        Debug.Log("Shield!");
        Destroy(Instantiate(ShieldModel, mech.transform), 2f);

        return true;
    }
}