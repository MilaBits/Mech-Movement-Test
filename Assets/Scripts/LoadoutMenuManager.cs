using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutMenuManager : MonoBehaviour {
    public Dropdown LoadoutDropdown;
    public Loadout loadout;

    [RequiredField] public MechController mechController;

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
        //Your Function You Want to Call
        UpdateAllBlocks();
    }

    private void UpdateAllBlocks() {
        UpdateStatBlocks("Sub Weapon");
        UpdateStatBlocks("Left Weapon");
        UpdateStatBlocks("Right Weapon");
        UpdateStatBlocks("Mod");
        UpdateStatBlocks("Armor");
    }

    private void FillDropdowns() {
        //Empty dropdowns before filling them
        LoadoutDropdown.options.Clear();
        LeftWeapon.DropDown.options.Clear();
        RightWeapon.DropDown.options.Clear();
        SubWeapon.DropDown.options.Clear();
        Mod.DropDown.options.Clear();
        Armor.DropDown.options.Clear();

        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>("Equipment/Weapons/")) {
            LeftWeapon.DropDown.options.Add(new Dropdown.OptionData(weapon.Name));
        }

        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>("Equipment/Weapons/")) {
            RightWeapon.DropDown.options.Add(new Dropdown.OptionData(weapon.Name));
        }

        foreach (SubWeapon subWeapon in Resources.LoadAll<SubWeapon>("Equipment/Weapons/")) {
            SubWeapon.DropDown.options.Add(new Dropdown.OptionData(subWeapon.Name));
        }

        foreach (Mod mod in Resources.LoadAll<Mod>("Equipment/Mods/")) {
            Mod.DropDown.options.Add(new Dropdown.OptionData(mod.Name));
        }

//        foreach (MainWeapon weapon in Resources.LoadAll<MainWeapon>("Equipment/Weapons/")) {
//            Armor.options.Add(new Dropdown.OptionData(weapon.Name));
//        }

        //Loadout stuff
        foreach (Loadout loadout in Resources.LoadAll<Loadout>("Equipment/Loadouts/")) {
            LoadoutDropdown.options.Add(new Dropdown.OptionData(loadout.name));
        }
    }

    public void LoadoutChanged() {
        loadout = Resources.LoadAll<Loadout>("Equipment/Loadouts/")
            .First(x => x.Name == LoadoutDropdown.GetComponentInChildren<Text>().text);

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
                SubWeapon subWeapon = Resources.LoadAll<SubWeapon>("Equipment/Weapons/")
                    .First(x => x.Name == SubWeapon.DropDown.GetComponentInChildren<Text>().text);
                SubWeapon.Damage = subWeapon.BaseDamage;
                SubWeapon.Health = subWeapon.Health;
                SubWeapon.Element = subWeapon.Element;
                SubWeapon.ElementPower = subWeapon.ElementPower;
                SubWeapon.UpdateUI();
                break;
            case "Left Weapon":
                MainWeapon leftWeapon = Resources.LoadAll<MainWeapon>("Equipment/Weapons/")
                    .First(x => x.Name == LeftWeapon.DropDown.GetComponentInChildren<Text>().text);
                LeftWeapon.Damage = leftWeapon.BaseDamage;
                LeftWeapon.Health = leftWeapon.Health;
                LeftWeapon.Element = leftWeapon.Element;
                LeftWeapon.ElementPower = leftWeapon.ElementPower;
                LeftWeapon.UpdateUI();
                break;
            case "Right Weapon":
                MainWeapon rightWeapon = Resources.LoadAll<MainWeapon>("Equipment/Weapons/")
                    .First(x => x.Name == RightWeapon.DropDown.GetComponentInChildren<Text>().text);
                RightWeapon.Damage = rightWeapon.BaseDamage;
                RightWeapon.Health = rightWeapon.Health;
                RightWeapon.Element = rightWeapon.Element;
                RightWeapon.ElementPower = rightWeapon.ElementPower;
                RightWeapon.UpdateUI();
                break;
            case "Mod":
                Mod mod = Resources.LoadAll<Mod>("Equipment/Mods/")
                    .First(x => x.Name == Mod.DropDown.GetComponentInChildren<Text>().text);
                Mod.Health = mod.Health;
                Mod.UpdateUI();
                break;
            case "Armor":
                break;
        }
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