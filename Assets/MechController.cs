using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour {
    [Header("Mech")]
    public MechTypes MechType;
    public GameObject Top;
    public GameObject Bottom;
    public float Speed;
    public float CameraSpeed;
    [Header("Equipment")]
    public WeaponSet WeaponSet1;
    public WeaponSet WeaponSet2;
    public Mod Mod;
    [Header("Camera")]
    public Camera camera;
    public float upperLimit;
    public float lowerLimit;


    private float LookRotation;
    private float LookJaw;

    private float facing;
    private float moveHorizontal;
    private float moveVertical;
    private float heading;
    
    void Update() {
        Movement();
        CameraMovement();
    }

    private void CameraMovement() {

        LookRotation = hInput.GetAxis("Camera Horizontal");
        LookJaw = hInput.GetAxis("Camera Vertical");

        Debug.Log("Horizontal: " + LookRotation + ", Vertical: " + LookJaw);

        Top.transform.Rotate(0.0f, LookRotation * CameraSpeed, 0.0f);
        camera.transform.RotateAround(Top.transform.position, Top.transform.right, LookJaw);
    }

    private void Movement() {

        moveHorizontal = hInput.GetAxis("Horizontal");
        moveVertical = hInput.GetAxis("Vertical");

        facing = Top.transform.eulerAngles.y;
        heading = Mathf.Atan2(moveHorizontal, moveVertical);


        if (moveHorizontal != 0 || moveVertical != 0) {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = Quaternion.Euler(0, facing, 0) * movement;
            transform.position += movement * Speed;

            Mathf.LerpAngle(heading, facing + heading * Mathf.Rad2Deg, Time.deltaTime);
            
            //TODO: CONFUSING ROTATION MATH I DON'T GET
            Quaternion newRotation = Quaternion.Euler(0f, facing + heading * Mathf.Rad2Deg, 0f);
            transform.rotation = Quaternion.Lerp(Bottom.transform.rotation, Quaternion.Euler(newRotation.eulerAngles), Time.deltaTime);

            // just keeping this instead for now
            Bottom.transform.rotation = Quaternion.Euler(0f, facing + heading * Mathf.Rad2Deg, 0f);

        }
    }
}
