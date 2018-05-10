using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour {
    public Stance CurrentStance;
    public Stance lastStance;

    public ActiveHand activeHand;
    public ActiveHand lastActiveHand;

    public int moveIndex;

    private Loadout loadout;
    private MainWeapon[] mainWeapons = new MainWeapon[2];
    private Game Game;

    private void Start() {
        Game = GetComponent<MechController>().Game;
        loadout = GetComponent<MechController>().Loadout;
        mainWeapons[0] = loadout.WeaponLeft;
        mainWeapons[1] = loadout.WeaponRight;
    }

    private void Update() {
        if (Game.controls.GetButtonDown("Left Weapon") || Game.controls.GetButtonDown("Right Weapon")) {
            if (Game.controls.GetButtonDown("Left Weapon")) {
                activeHand = ActiveHand.Left;
            }
            else if (Game.controls.GetButtonDown("Right Weapon")) {
                activeHand = ActiveHand.Right;
            }


            if (lastStance != CurrentStance || activeHand != lastActiveHand) {
                moveIndex = 0;
            }

            switch (CurrentStance) {
                case Stance.Idle:
                    mainWeapons[(int) activeHand].IdleMoves[moveIndex].Action.Execute();
                    UpdateStance(mainWeapons[(int) activeHand].IdleMoves[moveIndex].EndStance);
                    break;
                case Stance.Forward:
                    mainWeapons[(int) activeHand].ForwardMoves[moveIndex].Action.Execute();
                    UpdateStance(mainWeapons[(int) activeHand].ForwardMoves[moveIndex].EndStance);
                    break;
                case Stance.Finisher:
                    mainWeapons[(int) activeHand].FinisherMoves[moveIndex].Action.Execute();
                    UpdateStance(mainWeapons[(int) activeHand].FinisherMoves[moveIndex].EndStance);
                    break;
                case Stance.High:
                    mainWeapons[(int) activeHand].HighMoves[moveIndex].Action.Execute();
                    UpdateStance(mainWeapons[(int) activeHand].HighMoves[moveIndex].EndStance);
                    break;
                case Stance.Low:
                    mainWeapons[(int) activeHand].LowMoves[moveIndex].Action.Execute();
                    UpdateStance(mainWeapons[(int) activeHand].LowMoves[moveIndex].EndStance);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            moveIndex++;

            lastActiveHand = activeHand;
        }
    }

    private void UpdateStance(Stance newStance) {
        lastStance = CurrentStance;
        CurrentStance = newStance;
    }
}