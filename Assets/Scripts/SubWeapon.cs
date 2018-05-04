using UnityEditor;

public class SubWeapon : Equipment {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	[MenuItem("Assets/Create/Game/SubWeapon")]
	public static void CreateAsset() {
		ScriptableObjectUtility.CreateAsset<SubWeapon>();
	}
}
