using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HutongGames.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour {
    public Stance CurrentStance;
    public Stance lastStance;

    public ActiveHand activeHand;
    public ActiveHand lastActiveHand;

    public int moveIndex;

    private Loadout loadout;
    private MainWeapon[] mainWeapons = new MainWeapon[2];
    private Vector3[] mainWeaponPivots = new Vector3[2];
    private Game Game;
    private MechController Mech;

    public bool InputLag;

    public RectTransform endLagUI;
    public float EndLagTimeLeft;

    public float DualTime;
    public float DualTimeLeft;

    public bool useDualMove;
    [InlineEditor] public Move dualMove;

    private void Start() {
        Mech = GetComponent<MechController>();
        Game = GetComponent<MechController>().Game;
        loadout = GetComponent<MechController>().Loadout;
        mainWeapons[0] = loadout.WeaponLeft;
        mainWeapons[1] = loadout.WeaponRight;
        mainWeaponPivots[0] = loadout.WeaponLeftPivot;
        mainWeaponPivots[1] = loadout.WeaponRightPivot;
    }

    private void Update() {
        endLagUI.localScale = new Vector3(ExtensionMethods.Remap(EndLagTimeLeft, 0, 4, 0, 1), 1, 1);
//        endLagUI.localScale = new Vector2(endLagTimeLeft, endLagUI.localScale.y);
        // = "end lag: " + Math.Round(endLagTimeLeft, 2) + "s";

        if (Game.controls.GetButtonDown("Left Weapon") || Game.controls.GetButtonDown("Right Weapon")) {
            if (InputLag) {
                Debug.Log("Can't attack during end lag!");
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
                Debug.Log("Stance changed");
            }

            if (activeHand != lastActiveHand) {
                moveIndex = 0;
                Debug.Log("Hand Changed");
            }

            Move move = null;
            switch (CurrentStance) {
                case Stance.Idle:
                    move = mainWeapons[(int) activeHand].IdleMoves[moveIndex];
                    if (CanDual(mainWeapons[0].IdleMoves[moveIndex], mainWeapons[1].IdleMoves[moveIndex])) {
                        StartCoroutine(GetSecondInput());
                        move = dualMove;
                    }

                    break;
                case Stance.Forward:
                    move = mainWeapons[(int) activeHand].ForwardMoves[moveIndex];
                    break;
                case Stance.Finisher:
                    move = mainWeapons[(int) activeHand].FinisherMoves[moveIndex];
                    break;
                case Stance.High:
                    move = mainWeapons[(int) activeHand].HighMoves[moveIndex];
                    break;
                case Stance.Low:
                    move = mainWeapons[(int) activeHand].LowMoves[moveIndex];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Do the move stuff!
            move.ActivateHitbox(gameObject.tag, Mech.GetWeaponInHand(activeHand), move.HitBoxPosition);
            UpdateStance(move.EndStance);
            StartCoroutine(PostWeaponTime(move.moveTime, move.EndLag));

            moveIndex++;

            lastActiveHand = activeHand;
//            useDualMove= false;
        }
    }


    private IEnumerator GetSecondInput() {
        for (DualTimeLeft = DualTime; DualTimeLeft > 0; DualTimeLeft -= Time.deltaTime) {
            while (!Game.controls.GetButtonDown("Left Weapon") || !Game.controls.GetButtonDown("Right Weapon")) {
                yield return null;
            }

            useDualMove = true;
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
        Debug.Log("Input lag over");
    }
}