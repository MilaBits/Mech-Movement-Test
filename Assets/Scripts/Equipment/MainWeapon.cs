using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

public class MainWeapon : Weapon {
    [FoldoutGroup("Equipment Base/$Name"), BoxGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> IdleMoves;

    [BoxGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> ForwardMoves;

    [BoxGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> FinisherMoves;

    [BoxGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> HighMoves;

    [BoxGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> LowMoves;
}