using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutMenuManager : MonoBehaviour {
    public Loadout loadout;

    [RequiredField] public MechController mechController;

    public string LoadoutSavePath;


    [BoxGroup("UI")] public Dropdown LoadoutDropdown;
    [BoxGroup("UI")] public Text LoadoutName;
    [BoxGroup("UI")] public Toggle LightFrameToggle;
    [BoxGroup("UI")] public Toggle HeavyFrameToggle;

    private SubWeapon SelectedSubWeapon;
    private MainWeapon SelectedLeftWeapon;
    private MainWeapon SelectedRightWeapon;

    private Mod SelectedMod;
//    private Armor SelectedArmor;

    public StatBlock SubWeapon;
    public StatBlock LeftWeapon;
    public StatBlock RightWeapon;
    public StatBlock Mod;
    public StatBlock Armor;


    private void Start() {
        FillDropdowns();


        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        // Filling of dropdowns must happen after start
        UpdateAllBlocks();
    }

    private void UpdateAllBlocks() {
        UpdateStatBlocks("Sub Weapon");
        UpdateStatBlocks("Left Weapon");
        UpdateStatBlocks("Right Weapon");
        UpdateStatBlocks("Mod");
        UpdateStatBlocks("Armor");
    }

    public void FillDropdowns() {
        //Empty dropdowns before filling them
        LoadoutDropdown.options.Clear();

        LeftWeapon.DropDown.options.Clear();
        RightWeapon.DropDown.options.Clear();
        SubWeapon.DropDown.options.Clear();
        Mod.DropDown.options.Clear();
        Armor.DropDown.options.Clear();


        // Fill loadout dropdown with all existing loadouts.
        foreach (Loadout loadout in Resources.LoadAll<Loadout>(ResourcePaths.LoadoutPath)) {
            LoadoutDropdown.options.Add(new Dropdown.OptionData(loadout.name));
        }

        // Fill equipment dropdowns with their corresponding equipment.
        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)) {
            LeftWeapon.DropDown.options.Add(new Dropdown.OptionData(weapon.Name));
        }

        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)) {
            RightWeapon.DropDown.options.Add(new Dropdown.OptionData(weapon.Name));
        }

        foreach (SubWeapon subWeapon in Resources.LoadAll<SubWeapon>(ResourcePaths.WeaponPath)) {
            SubWeapon.DropDown.options.Add(new Dropdown.OptionData(subWeapon.Name));
        }

        foreach (Mod mod in Resources.LoadAll<Mod>(ResourcePaths.ModPath)) {
            Mod.DropDown.options.Add(new Dropdown.OptionData(mod.Name));
        }

//        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>("Equipment/Weapons/")) {
//            Armor.options.Add(new Dropdown.OptionData(weapon.Name));
//        }
    }

    public void LoadoutChanged() {
        loadout = Resources.LoadAll<Loadout>(ResourcePaths.LoadoutPath)
            .First(x => x.Name == LoadoutDropdown.GetComponentInChildren<Text>().text);

        if (loadout.MechType == MechTypes.Heavy) {
            HeavyFrameToggle.isOn = true;
        }
        else {
            LightFrameToggle.isOn = true;
        }

        LeftWeapon.DropDown.value = LeftWeapon.DropDown.options.IndexOf(LeftWeapon.DropDown.options.Single(
            x => x.text == loadout.WeaponLeft.Name));
        RightWeapon.DropDown.value = RightWeapon.DropDown.options.IndexOf(RightWeapon.DropDown.options.Single(
            x => x.text == loadout.WeaponRight.Name));
        SubWeapon.DropDown.value = SubWeapon.DropDown.options.IndexOf(SubWeapon.DropDown.options.Single(
            x => x.text == loadout.SubWeapon.Name));
        Mod.DropDown.value = Mod.DropDown.options.IndexOf(Mod.DropDown.options.Single(
            x => x.text == loadout.Mod.Name));
        //TODO: Armor at some point.

        UpdateAllBlocks();
    }

    public void Confirm() {
        Debug.Log("Confirm Pressed");
        mechController.Loadout = loadout;
        Debug.Log(mechController.Loadout);
    }

    public void UpdateStatBlocks(String name) {
        switch (name) {
            case "Sub Weapon":
                SelectedSubWeapon = Resources.LoadAll<SubWeapon>(ResourcePaths.WeaponPath)
                    .FirstOrDefault(x => x.Name == SubWeapon.DropDown.GetComponentInChildren<Text>().text);

                if (SelectedSubWeapon == null) {
                    SelectedSubWeapon = Resources.LoadAll<SubWeapon>(ResourcePaths.WeaponPath)[0];
                    SubWeapon.DropDown.GetComponentInChildren<Text>().text = SelectedSubWeapon.Name;

                    Debug.Log("auto-picked " + name + ".");
                }

                SubWeapon.Damage = SelectedSubWeapon.BaseDamage;
                SubWeapon.Health = SelectedSubWeapon.Health;
                SubWeapon.Element = SelectedSubWeapon.Element;
                SubWeapon.ElementPower = SelectedSubWeapon.ElementPower;
                SubWeapon.UpdateUI();
                break;
            case "Left Weapon":

                SelectedLeftWeapon = Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)
                    .FirstOrDefault(x => x.Name == LeftWeapon.DropDown.GetComponentInChildren<Text>().text);

                if (SelectedLeftWeapon == null) {
                    SelectedLeftWeapon = Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)[0];
                    LeftWeapon.DropDown.GetComponentInChildren<Text>().text = SelectedLeftWeapon.Name;

                    Debug.Log("auto-picked " + name + ".");
                }

                LeftWeapon.Damage = SelectedLeftWeapon.BaseDamage;
                LeftWeapon.Health = SelectedLeftWeapon.Health;
                LeftWeapon.Element = SelectedLeftWeapon.Element;
                LeftWeapon.ElementPower = SelectedLeftWeapon.ElementPower;
                LeftWeapon.UpdateUI();
                break;
            case "Right Weapon":

                SelectedRightWeapon = Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)
                    .FirstOrDefault(x => x.Name == RightWeapon.DropDown.GetComponentInChildren<Text>().text);

                if (SelectedRightWeapon == null) {
                    SelectedRightWeapon = Resources.LoadAll<MainWeapon>(ResourcePaths.WeaponPath)[0];
                    RightWeapon.DropDown.GetComponentInChildren<Text>().text = SelectedRightWeapon.Name;

                    Debug.Log("auto-picked " + name + ".");
                }

                RightWeapon.Damage = SelectedRightWeapon.BaseDamage;
                RightWeapon.Health = SelectedRightWeapon.Health;
                RightWeapon.Element = SelectedRightWeapon.Element;
                RightWeapon.ElementPower = SelectedRightWeapon.ElementPower;
                RightWeapon.UpdateUI();
                break;
            case "Mod":

                SelectedMod = Resources.LoadAll<Mod>(ResourcePaths.ModPath)
                    .FirstOrDefault(x => x.Name == Mod.DropDown.GetComponentInChildren<Text>().text);

                if (SelectedMod == null) {
                    SelectedMod = Resources.LoadAll<Mod>(ResourcePaths.ModPath)[0];
                    Mod.DropDown.GetComponentInChildren<Text>().text = SelectedMod.Name;

                    Debug.Log("auto-picked " + name + ".");
                }

                Mod.Health = SelectedMod.Health;
                Mod.UpdateUI();
                break;
            case "Armor":
                break;
        }
    }

    public void SaveLoadout() {
        // Instantiate new Loadout asset.
        Loadout loadout = ScriptableObject.CreateInstance<Loadout>();

        // Fill in loudout values.
        loadout.Name = LoadoutName.text;

        loadout.MechType = GetFrameToggleValue();

        // TODO: Temporarily hardcoded.
        if (loadout.MechType == MechTypes.Light) {
            loadout.TopFrame = Resources.LoadAll<TopFrame>(ResourcePaths.FramePath)
                .FirstOrDefault(x => x.Name == "Light Torso");
            loadout.BottomFrame = Resources.LoadAll<BottomFrame>(ResourcePaths.FramePath)
                .FirstOrDefault(x => x.Name == "Light Legs");
        }
        else {
            loadout.TopFrame = Resources.LoadAll<TopFrame>(ResourcePaths.FramePath)
                .FirstOrDefault(x => x.Name == "Heavy Torso");
            loadout.BottomFrame = Resources.LoadAll<BottomFrame>(ResourcePaths.FramePath)
                .FirstOrDefault(x => x.Name == "Heavy Legs");
        }

        loadout.SubWeapon = SelectedSubWeapon;
        loadout.WeaponLeft = SelectedLeftWeapon;
        loadout.WeaponRight = SelectedRightWeapon;
        loadout.Mod = SelectedMod;

        // Save loadout as asset.
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(
            LoadoutSavePath + "loadout_" + LoadoutName.text + ".asset");

        AssetDatabase.CreateAsset(loadout, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private MechTypes GetFrameToggleValue() {
        if (LightFrameToggle.isOn) {
            return MechTypes.Light;
        }

        return MechTypes.Heavy;
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}

[Serializable]
public class StatBlock {
    [BoxGroup("", false)] public Dropdown DropDown;

    [HorizontalGroup("Split", 0.5f, LabelWidth = 100)] [BoxGroup("Split/Value")]
    public float Damage;

    [BoxGroup("Split/UI"), HideLabel] public Text uiDamage;

    [BoxGroup("Split/Value")] public float Health;

    [BoxGroup("Split/UI"), HideLabel] public Text uiHealth;

    [BoxGroup("Split/Value")] public Element Element;

    [BoxGroup("Split/UI"), HideLabel] public Image uiElement;

    [BoxGroup("Split/Value")] public float ElementPower;

    [BoxGroup("Split/UI"), HideLabel] public Text uiElementPower;

    public void UpdateUI() {
        uiDamage.text = "DMG:\n" + Damage;
        uiHealth.text = "HP:\n" + Health;
        uiElement.sprite = getElementSprite(Element);
        uiElementPower.text = ElementPower.ToString();
    }

    private Sprite getElementSprite(Element element) {
        Sprite[] ElementIconAtlas = Resources.LoadAll<Sprite>("Sprites/UI/Elements");
        switch (element) {
            case Element.None:
                return ElementIconAtlas.Single(s => s.name == "Elem_None");
            case Element.Blast:
                return ElementIconAtlas.Single(s => s.name == "Elem_Blast");
            case Element.Rust:
                return ElementIconAtlas.Single(s => s.name == "Elem_Rust");
            case Element.Spark:
                return ElementIconAtlas.Single(s => s.name == "Elem_Spark");
            case Element.Frost:
                return ElementIconAtlas.Single(s => s.name == "Elem_Frost");
            case Element.Glitch:
                return ElementIconAtlas.Single(s => s.name == "Elem_Glitch");
            case Element.Quake:
                return ElementIconAtlas.Single(s => s.name == "Elem_Quake");
            default:
                return ElementIconAtlas.Single(s => s.name == "Elem_None");
        }
    }
}