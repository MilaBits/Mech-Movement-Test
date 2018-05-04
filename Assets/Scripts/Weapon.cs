using UnityEditor;

public class Weapon : Equipment {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	[MenuItem("Assets/Create/Game/Weapon")]
	public static void CreateAsset() {
		ScriptableObjectUtility.CreateAsset<Weapon>();
	}
}
