using UnityEditor;
using UnityEngine;

public class Shield : Mod {
    public GameObject ShieldModel;
    
    [MenuItem("Assets/Create/Game/Shield")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<Shield>();
    }
}
