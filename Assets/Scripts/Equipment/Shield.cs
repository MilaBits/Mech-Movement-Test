using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Shield : Mod {
    [BoxGroup("Equipment Base/$Name", false)] public float BlockPower;

    [BoxGroup("Equipment Base/$Name")] [InlineEditor(InlineEditorModes.LargePreview)] [PropertyOrder(100)]
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