using Sirenix.OdinInspector;
using UnityEngine;

public class MechController : MonoBehaviour {
    [Required] public Game Game;
    private Controls controls;

    [TabGroup("Main","Mech")] [EnumToggleButtons] public MechTypes MechType;

    [TabGroup("Main","Mech")] public GameObject Top;
    [TabGroup("Main","Mech")] public GameObject Bottom;
    [Space]
    [TabGroup("Main","Mech")] public float MovementSpeed = .1f;

    [Space] [TabGroup("Main","Camera")] public LayerMask LevelMask;

    [TabGroup("Main","Camera")] public Camera PlayerCamera;
    [Space] [TabGroup("Main","Camera")] public float MaxDistance = 3.5f;

    private float distance;
    [TabGroup("Main","Camera")] public float WallOffset = 1;
    private Vector3 cameraVelocity = Vector3.zero;
    [TabGroup("Main","Camera")] public float smoothTime = 0.3f;
    [Space] [TabGroup("Main","Camera")] public float upperLimit = 40;

    [TabGroup("Main","Camera")] public float lowerLimit = -15;
    [Space] [TabGroup("Main","Camera")] public float HorizontalSensitivity = 2;

    [TabGroup("Main","Camera")] public float VerticalSensitivity = 1;
    [TabGroup("Main","Camera")] public float WeaponDampening;

    private float LookRotation;
    private float LookJaw;

    private float verticalCamRotation;
    private float facing;
    private float moveHorizontal;
    private float moveVertical;
    private float heading;

    
    [FoldoutGroup("Main/Mech/Equipment")]
    [HorizontalGroup("Main/Mech/Equipment/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Main/Mech/Equipment/Split/Left Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public Weapon WeaponLeft;

    [BoxGroup("Main/Mech/Equipment/Split/Left Weapon")] [LabelWidth(55)] [LabelText("Pivot")]
    public GameObject WeaponLeftPivot;

    [HorizontalGroup("Main/Mech/Equipment/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Main/Mech/Equipment/Split/Right Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public Weapon WeaponRight;

    [BoxGroup("Main/Mech/Equipment/Split/Right Weapon")] [LabelWidth(55)] [LabelText("Pivot")]
    public GameObject WeaponRightPivot;

    [BoxGroup("Main/Mech/Equipment/Subweapon")] [LabelWidth(55)] public SubWeapon SubWeapon;

    [BoxGroup("Main/Mech/Equipment/Subweapon")] [LabelWidth(55)] [LabelText("Pivot")]
    public GameObject SubWeaponPivot;
    
    [BoxGroup("Main/Mech/Equipment/Mod")] [LabelWidth(55)] public Mod Mod;

    [BoxGroup("Main/Mech/Equipment/Mod")] [LabelWidth(55)] [LabelText("Pivot")]
    public GameObject ModPivot;

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
            Mod.Action();
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
        Vector3 lookAtPoint = PlayerCamera.transform.parent.position + (PlayerCamera.transform.forward * -GetDistanceToWall());
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
}