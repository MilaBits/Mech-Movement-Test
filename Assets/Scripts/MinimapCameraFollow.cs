using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour {
    public bool FollowTarget;
    public bool RotateWithTarget;

    public GameObject Target;
    public Vector3 Offset;
    [Range(4, 50)] public float Zoom;
    [Range(.1f, 15)] public float followSpeed;

    private Camera followCam;

    private void Start() {
        followCam = GetComponent<Camera>();
    }

    void LateUpdate() {
        if (FollowTarget) {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset,
                followSpeed * Time.deltaTime);
//            transform.position = Target.transform.position + Offset;
        }

        if (RotateWithTarget) {
            transform.localRotation = Quaternion.Euler(90, Target.transform.eulerAngles.y, 0);
        }
        else {
            if (transform.eulerAngles.y != 0) transform.localRotation = Quaternion.Euler(90, 0, 0);
        }

        followCam.orthographicSize = Zoom;
    }
}