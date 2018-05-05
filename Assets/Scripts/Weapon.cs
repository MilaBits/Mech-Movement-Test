using UnityEditor;

public class Weapon : Equipment {

	[MenuItem("Assets/Create/Game/Weapon")]
	public static void CreateAsset() {
		ScriptableObjectUtility.CreateAsset<Weapon>();
	}
}
