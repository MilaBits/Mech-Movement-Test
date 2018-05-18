using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {
    private Loadout loadout;
    private MainWeapon[] mainWeapons = new MainWeapon[2];
    private Vector3[] mainWeaponPivots = new Vector3[2];
    private Game Game;
    private MechController Mech;

    [BoxGroup("Dual Move")] public string DualTimerName;
    [BoxGroup("Dual Move")] public float DualTime;
    [BoxGroup("Dual Move")] public bool dualPerformed;
    [BoxGroup("Dual Move")] [InlineEditor] public Move dualMove;

    [BoxGroup("End Lag")] public bool InputLag;
    [BoxGroup("End Lag")] public Image endLagUI;
    [BoxGroup("End Lag")] public float EndLagTimeLeft;
    private Timer dualTimer;

    [BoxGroup("Stance", false), HorizontalGroup("Stance/Split"), LabelWidth(100)]
    public Stance CurrentStance;

    [HorizontalGroup("Stance/Split"), LabelWidth(100)]
    public Stance lastStance;

    [BoxGroup("Hand", false), HorizontalGroup("Hand/Split"), LabelWidth(100)]
    public ActiveHand activeHand;

    [HorizontalGroup("Hand/Split"), LabelWidth(100)]
    public ActiveHand lastActiveHand;

    [BoxGroup("Move", false), LabelWidth(100)]
    public int moveIndex;

    private void Start() {
        Mech = GetComponent<MechController>();
        Game = GetComponent<MechController>().Game;
        loadout = GetComponent<MechController>().Loadout;
        dualTimer = GetComponents<Timer>().FirstOrDefault(t => t.Name == DualTimerName);
        mainWeapons[0] = loadout.WeaponLeft;
        mainWeapons[1] = loadout.WeaponRight;
        mainWeaponPivots[0] = loadout.WeaponLeftPivot;
        mainWeaponPivots[1] = loadout.WeaponRightPivot;
    }

    private void Update() {
        endLagUI.rectTransform.localScale = new Vector3(ExtensionMethods.Remap(EndLagTimeLeft, 0, 4, 0, 1), 1, 1);

        if (Game.controls.GetButtonDown("Left Weapon") || Game.controls.GetButtonDown("Right Weapon")) {
            if (InputLag) {
                //Debug.Log("Can't attack during end lag!");
                return;
            }

            if (Game.controls.GetButtonDown("Left Weapon")) {
                activeHand = ActiveHand.Left;
            }
            else if (Game.controls.GetButtonDown("Right Weapon")) {
                activeHand = ActiveHand.Right;
            }

            if (lastStance != CurrentStance) {
                moveIndex = 0;
                //Debug.Log("Stance changed");
            }

            if (activeHand != lastActiveHand) {
                moveIndex = 0;
                //Debug.Log("Hand Changed");
            }

            Move move = null;
            switch (CurrentStance) {
                case Stance.Idle:
                    if (mainWeapons[(int) activeHand].IdleMoves.Count <= moveIndex) moveIndex = 0;
                    move = mainWeapons[(int) activeHand].IdleMoves[moveIndex];
                    if (CanDual(mainWeapons[0].IdleMoves[moveIndex], mainWeapons[1].IdleMoves[moveIndex])) {
                        //Sets move to be dualmove if the other move is used in time
                        if (!dualPerformed) {
                            dualPerformed = true;
                            StartCoroutine(GetSecondInput(move, DualTime, Game.controls));
                        }
                    }

                    break;
                case Stance.Forward:
                    move = mainWeapons[(int) activeHand].ForwardMoves[moveIndex];
                    if (CanDual(mainWeapons[0].ForwardMoves[moveIndex], mainWeapons[1].ForwardMoves[moveIndex])) {
                        //Sets move to be dualmove if the other move is used in time
                        if (!dualPerformed) {
                            dualPerformed = true;
                            StartCoroutine(GetSecondInput(move, DualTime, Game.controls));
                        }
                    }

                    break;
                case Stance.Finisher:
                    move = mainWeapons[(int) activeHand].FinisherMoves[moveIndex];
                    if (CanDual(mainWeapons[0].FinisherMoves[moveIndex], mainWeapons[1].FinisherMoves[moveIndex])) {
                        //Sets move to be dualmove if the other move is used in time
                        if (!dualPerformed) {
                            dualPerformed = true;
                            StartCoroutine(GetSecondInput(move, DualTime, Game.controls));
                        }
                    }

                    break;
                case Stance.High:
                    move = mainWeapons[(int) activeHand].HighMoves[moveIndex];
                    if (CanDual(mainWeapons[0].HighMoves[moveIndex], mainWeapons[1].HighMoves[moveIndex])) {
                        //Sets move to be dualmove if the other move is used in time
                        if (!dualPerformed) {
                            dualPerformed = true;
                            StartCoroutine(GetSecondInput(move, DualTime, Game.controls));
                        }
                    }

                    break;
                case Stance.Low:
                    move = mainWeapons[(int) activeHand].LowMoves[moveIndex];
                    if (CanDual(mainWeapons[0].LowMoves[moveIndex], mainWeapons[1].LowMoves[moveIndex])) {
                        //Sets move to be dualmove if the other move is used in time
                        if (!dualPerformed) {
                            dualPerformed = true;
                            StartCoroutine(GetSecondInput(move, DualTime, Game.controls));
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void ExecuteMove(Move move) {
        EndLagTimeLeft = move.EndLag;
        Debug.Log(move.name);

        // Do the move stuff!
        move.ActivateHitbox(gameObject.tag, Mech.GetWeaponInHand(activeHand), move.HitBoxPosition);
        UpdateStance(move.EndStance);

        //Block weapon usage for a short while afterwards
        StartCoroutine(PostWeaponTime(move.moveTime, move.EndLag));

        moveIndex++;
        lastActiveHand = activeHand;
    }

    private IEnumerator GetSecondInput(Move move, float time, Controls controls) {
        dualTimer.StartTimer(DualTime);
        bool useDual = false;
        yield return new WaitForSecondsOrInput(activeHand, time, controls, result => useDual = result);

        if (useDual) {
            //dual move
            move = dualMove;
            moveIndex = 0;
        }

        ExecuteMove(move);
    }

    public class WaitForSecondsOrInput : CustomYieldInstruction {
        private float numSeconds;
        private float startTime;
        private Controls controls;
        private ActiveHand activeHand;
        private Action<bool> Result;

        public WaitForSecondsOrInput(ActiveHand activeHand, float numSeconds, Controls controls, Action<bool> result) {
            startTime = Time.time;
            this.numSeconds = numSeconds;
            this.controls = controls;
            this.activeHand = activeHand;
            Result = result;
        }

        public override bool keepWaiting {
            get {
                if (activeHand == ActiveHand.Left && controls.GetButtonDown("Right Weapon")) {
                    Debug.Log("Stopping timer");
                    Result(true);
                    return false;
                }

                if (activeHand == ActiveHand.Right && controls.GetButtonDown("Left Weapon")) {
                    Debug.Log("Stopping timer");
                    Result(true);
                    return false;
                }

                return Time.time - startTime < numSeconds;
                //TODO: Add trigger weapons
            }
        }
    }

    private bool CanDual(Move first, Move second) {
        return first.EndStance == second.EndStance;
    }

    private void UpdateStance(Stance newStance) {
        lastStance = CurrentStance;
        CurrentStance = newStance;
    }

    IEnumerator PostWeaponTime(float moveTime, float endLag) {
        yield return new WaitForSeconds(moveTime);
        InputLag = true;

        for (EndLagTimeLeft = endLag; EndLagTimeLeft > 0; EndLagTimeLeft -= Time.deltaTime) yield return null;

        InputLag = false;
        dualPerformed = false;
        Debug.Log("Input lag over");
    }
}