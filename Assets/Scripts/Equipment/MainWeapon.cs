using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainWeapon : Weapon {

	[MenuItem("Assets/Create/Game/MainWeapon")]
	public static void CreateAsset() {
		ScriptableObjectUtility.CreateAsset<MainWeapon>();
	}
}
