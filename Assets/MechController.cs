using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour {
    public MechTypes MechType;
    public GameObject Top;
    public GameObject Bottom;
    public float Speed;
    public float CameraSpeed;
    [Space]
    public WeaponSet WeaponSet1;
    public WeaponSet WeaponSet2;
    public Mod Mod;



    void Start() {
        float LookRotation = Input.GetAxis("Camera Horizontal");
        float LookJaw = Input.GetAxis("Camera Vertical");

        float facing = Top.transform.eulerAngles.y;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float heading = Mathf.Atan2(moveHorizontal, moveVertical);
    }

    void Update() {
        Movement();
        CameraMovement();
    }

    private void CameraMovement() {

        LookRotation = Input.GetAxis("Camera Horizontal");
        LookJaw = Input.GetAxis("Camera Vertical");

        Debug.Log("Horizontal: " + LookRotation + ", Vertical: " + LookJaw);

        Top.transform.Rotate(0.0f, LookRotation * CameraSpeed, 0.0f);
    }

    private void Movement() {




        if (moveHorizontal != 0 || moveVertical != 0) {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = Quaternion.Euler(0, facing, 0) * movement;
            transform.position += movement * Speed;

            Bottom.transform.rotation = Quaternion.Euler(0f, facing + heading * Mathf.Rad2Deg, 0f);

        }

    }
}
