using Sirenix.OdinInspector;
using UnityEngine;

public class MechController : MonoBehaviour {
    [Required] public Game Game;
    private Controls controls;

    [TabGroup("Main", "Mech"), BoxGroup("Main/Mech/Base", false)] [EnumToggleButtons]
    public MechTypes MechType;

    //TODO: Perhaps update these to find children by tag or something having to put them in manually seems clunky
    [BoxGroup("Main/Mech/Base", false)] public GameObject Top;
    [BoxGroup("Main/Mech/Base", false)] public GameObject Bottom;

    [Space] [BoxGroup("Main/Mech/Base", false)]
    public float MovementSpeed = .1f;

    [Space] [TabGroup("Main", "Camera")] public LayerMask LevelMask;

    [TabGroup("Main", "Camera")] public Camera PlayerCamera;
    [Space] [TabGroup("Main", "Camera")] public float MaxDistance = 3.5f;

    private float distance;
    [TabGroup("Main", "Camera")] public float WallOffset = 1;
    private Vector3 cameraVelocity = Vector3.zero;
    [TabGroup("Main", "Camera")] public float smoothTime = 0.3f;
    [Space] [TabGroup("Main", "Camera")] public float upperLimit = 40;

    [TabGroup("Main", "Camera")] public float lowerLimit = -15;
    [Space] [TabGroup("Main", "Camera")] public float HorizontalSensitivity = 2;

    [TabGroup("Main", "Camera")] public float VerticalSensitivity = 1;
    [TabGroup("Main", "Camera")] public float WeaponDampening;

    private float LookRotation;
    private float LookJaw;

    private float verticalCamRotation;
    private float facing;
    private float moveHorizontal;
    private float moveVertical;
    private float heading;


    [TabGroup("Main", "Mech"), InlineEditor]
    public Loadout Loadout;

    private MainWeapon WeaponLeft;

    [BoxGroup("Main/Mech/Pivots"), LabelText("Pivot - Left Weapon"), LabelWidth(125)]
    public GameObject WeaponLeftPivot;

    private MainWeapon WeaponRight;

    [BoxGroup("Main/Mech/Pivots"), LabelText("Pivot - Right Weapon"), LabelWidth(125)]
    public GameObject WeaponRightPivot;

    private SubWeapon SubWeapon;

    [BoxGroup("Main/Mech/Pivots"), LabelText("Pivot - Sub-Weapon"), LabelWidth(125)]
    public GameObject SubWeaponPivot;

    private Mod Mod;

    [BoxGroup("Main/Mech/Pivots"), LabelText("Pivot - Mod"), LabelWidth(125)]
    public GameObject ModPivot;

    void Start() {
        if (!Game)
            Game = GameObject.FindObjectOfType<Game>();
        controls = Game.controls;

        InitializeEquipment();
    }

    private void InitializeEquipment() {
        WeaponLeft = (Resources.Load<MainWeapon>("Equipment/Weapons/" + Loadout.WeaponLeft.name));
        WeaponLeft.InitializeModel(WeaponLeftPivot.transform);
        WeaponLeft.mech = this;

        WeaponRight = (Resources.Load<MainWeapon>("Equipment/Weapons/" + Loadout.WeaponRight.name));
        WeaponRight.InitializeModel(WeaponRightPivot.transform);
        WeaponRight.mech = this;

        SubWeapon = (Resources.Load<SubWeapon>("Equipment/Weapons/" + Loadout.SubWeapon.name));
        SubWeapon.InitializeModel(SubWeaponPivot.transform);
        SubWeapon.mech = this;

        Mod = (Resources.Load<Mod>("Equipment/Mods/" + Loadout.Mod.name));
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
            Loadout.Mod.Action();
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
            transform.position += movement * MovementSpeed;

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
        verticalCamRotation = ClampAngle(PlayerCamera.transform.localEulerAngles.x + LookJaw * VerticalSensitivity,
            lowerLimit, upperLimit);
        PlayerCamera.transform.localRotation = Quaternion.Euler(verticalCamRotation, 0, 0);
        Vector3 lookAtPoint = PlayerCamera.transform.parent.position +
                              (PlayerCamera.transform.forward * -GetDistanceToWall());
        PlayerCamera.transform.position =
            Vector3.SmoothDamp(PlayerCamera.transform.position, lookAtPoint, ref cameraVelocity, smoothTime);

        //TODO: Public float forcamera dampening speed between >0 and 1, if 0 no effect
        //TODO: Change view to cockpit camera when below a certain distance
    }

    private float GetDistanceToWall() {
        RaycastHit hit;
        float dist = MaxDistance;
        if (Physics.Raycast(PlayerCamera.transform.parent.position, -PlayerCamera.transform.forward, out hit,
            MaxDistance, LevelMask)) {
            dist = hit.distance - WallOffset;
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

    public Transform GetWeaponInHand(ActiveHand activeHand) {
        if (activeHand == ActiveHand.Left) {
            return WeaponLeftPivot.transform;
        }

        return WeaponRightPivot.transform;
    }
}