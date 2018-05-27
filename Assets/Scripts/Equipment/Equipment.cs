using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Equipment : ScriptableObject {
    [HideInInspector] public MechController mech;

    [BoxGroup("Equipment Base"), InlineButton("UpdateName", "Update File Name"), LabelText("Equipment Name")]
    public string Name;

    [BoxGroup("Equipment Base"), LabelText("Equipment Health")]
    public float Health;
    
    [BoxGroup("Equipment Base"), InlineEditor(InlineEditorModes.LargePreview), PropertyOrder(100),
     LabelText("Equipment Model")]
    public GameObject Model;

    public void InitializeModel(Transform position) {
        Instantiate(Model, position);
    }

    void UpdateName() {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, Name);
        AssetDatabase.SaveAssets();
    }
}