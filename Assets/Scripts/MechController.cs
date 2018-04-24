using HutongGames.PlayMaker.Actions;
using UnityEditor.VersionControl;
using UnityEngine;

public class MechController : MonoBehaviour {
    public Game Game;
    private Controls controls;

    [Header("Mech")] public MechTypes MechType;
    public GameObject Top;
    public GameObject Bottom;
    public float Speed = .1f;

    [Header("Weapon Left")] public Weapon WeaponLeft;
    public GameObject WeaponLeftPivot;
    [Header("Weapon Right")] public Weapon WeaponRight;
    public GameObject WeaponRightPivot;
    [Header("Mod")] public Mod Mod;
    public GameObject ModPivot;
    public float JumpForce = 2;

    [Header("Camera")] public Camera camera;

    public float MaxDistance = 3.5f;
    private float distance;
    public LayerMask LevelMask;
    public float WallOffset = 1;
    private Vector3 cameraVelocity = Vector3.zero;
    public float smoothTime = 0.3f;

    public float upperLimit = 40;
    public float lowerLimit = -15;

    public float HorizontalSensitivity = 2;
    public float VerticalSensitivity = 1;
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

        InitializeEquipment();
    }

    private void InitializeEquipment() {
        //TODO: invert X scale on left weapon
        WeaponLeft = Instantiate(Resources.Load<Weapon>("Weapons/" + WeaponLeft.name));
        WeaponRight.InitializeModel(WeaponLeftPivot.transform);
        WeaponLeft.mech = this;

        WeaponRight = Instantiate(Resources.Load<Weapon>("Weapons/" + WeaponRight.name));
        WeaponRight.InitializeModel(WeaponRightPivot.transform);
        WeaponRight.mech = this;

        Mod = Instantiate(Resources.Load<Mod>("Mods/" + Mod.name));
        Mod.InitializeModel(ModPivot.transform);
        Mod.mech = this;
    }

    void Update() {
        Movement();
    }

    void LateUpdate() {
        CameraMovement();
        WeaponMovement();
    }

    private void WeaponMovement() {
        WeaponLeftPivot.transform.localRotation = Quaternion.Euler(verticalCamRotation, 0, 0);
        WeaponRightPivot.transform.localRotation = Quaternion.Euler(verticalCamRotation, 0, 0);
    }

    private void FixedUpdate() {
        ModAction();
    }

    private void ModAction() {
        if (controls.GetButtonDown("Mod")) {
            Debug.Log("Mod!");
            GetComponent<Rigidbody>().AddForce(Vector3.forward * JumpForce);
        }
    }

    private void Movement() {
        moveHorizontal = controls.GetAxis("Horizontal");
        moveVertical = controls.GetAxis("Vertical");

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
        LookRotation = controls.GetAxis("Horizontal Camera");
        LookJaw = controls.GetAxis("Vertical Camera");

        // Horizontal rotation
        Top.transform.Rotate(0.0f, LookRotation * HorizontalSensitivity, 0.0f);


        // Vertical rotation
        verticalCamRotation = ClampAngle(camera.transform.localEulerAngles.x + LookJaw * VerticalSensitivity,
            lowerLimit, upperLimit);
        camera.transform.localRotation = Quaternion.Euler(verticalCamRotation, 0, 0);
        Vector3 lookAtPoint = camera.transform.parent.position + (camera.transform.forward * -GetDistanceToWall());
        camera.transform.position =
            Vector3.SmoothDamp(camera.transform.position, lookAtPoint, ref cameraVelocity, smoothTime);

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