using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Loadout : ScriptableObject {
    [BoxGroup("Name", false), InlineButton("UpdateName", "Update File Name"), LabelText("Loadout Name")]
    public string Name;

    //TODO: Probably add mech type to loadout rather than the mech itself.

    [FoldoutGroup("Mech")] [FoldoutGroup("Mech")] [EnumToggleButtons]
    public MechTypes MechType;

    //TODO: Perhaps update these to find children by tag or something having to put them in manually seems clunky
    [BoxGroup("Mech/Body", false)] public TopFrame TopFrame;
    [BoxGroup("Mech/Body", false)] public BottomFrame BottomFrame;

    [FoldoutGroup("Weapons")]
    [HorizontalGroup("Weapons/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Weapons/Split/Left Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public MainWeapon WeaponLeft;

    [BoxGroup("Weapons/Split/Left Weapon")] [LabelWidth(55)] [LabelText("Offset")]
    public Vector3 WeaponLeftPivot;

    [HorizontalGroup("Weapons/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Weapons/Split/Right Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public MainWeapon WeaponRight;

    [BoxGroup("Weapons/Split/Right Weapon")] [LabelWidth(55)] [LabelText("Offset")]
    public Vector3 WeaponRightPivot;

    [BoxGroup("Weapons/Subweapon")] [LabelWidth(55)]
    public SubWeapon SubWeapon;

    [BoxGroup("Weapons/Subweapon")] [LabelWidth(55)] [LabelText("Offset")]
    public Vector3 SubWeaponPivot;

    [FoldoutGroup("Mod")] [LabelWidth(55)] public Mod Mod;

    [FoldoutGroup("Mod")] [LabelWidth(55)] [LabelText("Offset")]
    public Vector3 ModPivotOffset;

    void UpdateName() {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, Name);
        AssetDatabase.SaveAssets();
    }
}