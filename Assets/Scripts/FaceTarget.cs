using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour {

	public Transform Target;
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt(Target);
	}
}
