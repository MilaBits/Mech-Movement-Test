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

    [FoldoutGroup("Equipment")]
    [HorizontalGroup("Equipment/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Equipment/Split/Left Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public MainWeapon WeaponLeft;

    [HorizontalGroup("Equipment/Split", 0.5f, LabelWidth = 20)]
    [BoxGroup("Equipment/Split/Right Weapon")]
    [LabelWidth(55)]
    [LabelText("Weapon")]
    public MainWeapon WeaponRight;

    [BoxGroup("Equipment/Subweapon")] [LabelWidth(105)]
    public SubWeapon SubWeapon;

    [FoldoutGroup("Equipment/Mod")] [LabelWidth(55)] public Mod Mod;

    void UpdateName() {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, Name);
        AssetDatabase.SaveAssets();
    }
}