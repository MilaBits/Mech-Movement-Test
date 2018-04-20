using HutongGames.PlayMaker.Actions;
using UnityEditor.VersionControl;
using UnityEngine;

public class MechController : MonoBehaviour {
    public Game Game;
    private Controls controls;

    [Header("Mech")] public MechTypes MechType;
    public GameObject Top;
    public GameObject Bottom;
    public float Speed;
    public float CameraSpeed;
    [Header("Equipment")] public WeaponSet WeaponSet1;
    public WeaponSet WeaponSet2;
    public Mod Mod;
    [Header("Camera")] public Camera camera;

    public float MaxDistance;
    private float distance;
    public LayerMask LevelMask;
    public float WallOffset;
    public float upperLimit;
    public float lowerLimit;

    public float HorizontalSensitivity;
    public float VerticalSensitivity;
    public float WeaponDampening;

    private float LookRotation;
    private float LookJaw;

    private float verticalCamRotation;
    private float facing;
    private float moveHorizontal;
    private float moveVertical;
    private float heading;

    void Start() {
        if (!Game)
            Game = GameObject.FindObjectOfType<Game>();
        controls = Game.controls;
    }

    void Update() {
        Movement();
    }

    void LateUpdate() {
        CameraMovement();
    }

    private void Movement() {
        moveHorizontal = controls.GetAxis("Horizontal");
        moveVertical = controls.GetAxis("Vertical");

        Debug.Log(moveHorizontal);

        facing = Top.transform.eulerAngles.y;
        heading = Mathf.Atan2(moveHorizontal, moveVertical);

        if (moveHorizontal != 0 || moveVertical != 0) {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = Quaternion.Euler(0, facing, 0) * movement;
            transform.position += movement * Speed;

            Quaternion newRotation = Quaternion.Euler(0f, facing + heading * Mathf.Rad2Deg, 0f);
            Bottom.transform.rotation = Quaternion.Lerp(Bottom.transform.rotation,
                Quaternion.Euler(newRotation.eulerAngles), Time.deltaTime * 5);
        }
    }

    private void CameraMovement() {
        LookRotation = controls.GetAxis("Camera Horizontal");
        LookJaw = controls.GetAxis("Camera Vertical");

        // Horizontal rotation
        Top.transform.Rotate(0.0f, LookRotation * HorizontalSensitivity, 0.0f);


        // Vertical rotation
        verticalCamRotation = ClampAngle(camera.transform.localEulerAngles.x + LookJaw * VerticalSensitivity,
            lowerLimit, upperLimit);
        camera.transform.localRotation = Quaternion.Euler(verticalCamRotation, 0, 0);
        Vector3 lookAtPoint = camera.transform.parent.position + (camera.transform.forward * -GetDistanceToWall());
        camera.transform.position = lookAtPoint;

        //TODO: Public float forcamera dampening speed between >0 and 1, if 0 no effect
    }

    private float GetDistanceToWall() {
        RaycastHit hit;
        float dist = MaxDistance;
        Debug.DrawRay(camera.transform.parent.position, -camera.transform.forward * MaxDistance, Color.red, 1f);
        if (Physics.Raycast(camera.transform.parent.position, -camera.transform.forward, out hit,
            MaxDistance, LevelMask)) {
            Debug.DrawLine(camera.transform.parent.position, hit.point, Color.green, 1f);
            dist = hit.distance - WallOffset;
            Debug.Log("Wall at: " + dist);
        }

        return dist;
    }

    private float ClampAngle(float angle, float min, float max) {
        if (min < 0 && max > 0 && (angle > max || angle < min)) {
            angle -= 360;
            if (angle > max || angle < min) {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        else if (min > 0 && (angle > max || angle < min)) {
            angle += 360;
            if (angle > max || angle < min) {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }

        if (angle < min) return min;
        else if (angle > max) return max;
        else return angle;
    }
}