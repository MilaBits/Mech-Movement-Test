using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

public class MainWeapon : Weapon {
    [FoldoutGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> IdleMoves;

    [FoldoutGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> ForwardMoves;

    [FoldoutGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> FinisherMoves;

    [FoldoutGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> HighMoves;

    [FoldoutGroup("Equipment Base/$Name/Moves"), InlineEditor]
    public List<Move> LowMoves;

    [MenuItem("Assets/Create/Game/MainWeapon")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<MainWeapon>();
    }
}