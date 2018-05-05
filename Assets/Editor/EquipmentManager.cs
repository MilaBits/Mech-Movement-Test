using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class EquipmentManager : OdinMenuEditorWindow {
    [MenuItem("Tools/Equipment Manager")]
    public static void OpenWindow() {
        GetWindow<EquipmentManager>().Show();
    }

    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree(true);
        tree.DefaultMenuStyle.IconSize = 28.00f;
        tree.Config.DrawSearchToolbar = true;

        tree.Add("Overview", new AllEquipment());

        tree.AddAllAssetsAtPath("Weapons", "Assets/Resources/Equipment/Weapons/", typeof(Weapon), true, true)
            .SortMenuItemsByName();

        // Add all scriptable object items.
        tree.AddAllAssetsAtPath("Sub-Weapons", "Assets/Resources/Equipment/Weapons/", typeof(SubWeapon), true)
            .SortMenuItemsByName();

        tree.AddAllAssetsAtPath("Mods", "Assets/Resources/Equipment/Mods/", typeof(Mod), true)
            .SortMenuItemsByName();

        // Add icons to characters and items.
//        tree.EnumerateTree().AddIcons<Character>(x => x.Icon);
//        tree.EnumerateTree().AddIcons<Item>(x => x.Icon);

        return tree;
    }

    protected override void OnBeginDrawEditors() {
        var selected = this.MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        // Draws a toolbar with the name of the currently selected menu item.
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null) {
                GUILayout.Label(selected.Name);
            }

            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Equipment"))) {
                ScriptableObjectCreator.ShowDialog<Equipment>("Assets/Resources/Equipment",
                    obj => {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    public class EquipmentOverview : GlobalConfig<EquipmentOverview> {
        [ReadOnly] [ListDrawerSettings(Expanded = true)]
        public Equipment[] AllEquipment;

#if UNITY_EDITOR
        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateCharacterOverview() {
            // Finds and assigns all scriptable objects of type Character
            this.AllEquipment = AssetDatabase.FindAssets("t:Equipment")
                .Select(guid => AssetDatabase.LoadAssetAtPath<Equipment>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
        }
#endif
    }

    public class AllEquipment {
        [AssetList(Path = "Resources/Equipment/Weapons", AutoPopulate = true)] [InlineEditor]
        public List<Weapon> weapons;

        [AssetList(Path = "Resources/Equipment/Weapons", AutoPopulate = true)] [InlineEditor]
        public List<SubWeapon> SubWeapons;

        [AssetList(Path = "Resources/Equipment/Mods", AutoPopulate = true)] [InlineEditor]
        public List<Mod> Mods;
    }
}