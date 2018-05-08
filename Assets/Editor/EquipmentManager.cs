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
        tree.Add("Loadouts", new Loadouts());

        tree.AddAllAssetsAtPath("Weapons/Main Weapons", "Assets/Resources/Equipment/Weapons/", typeof(MainWeapon), true,
                true)
            .SortMenuItemsByName()
            .ForEach(this.AddDragHandles);

        tree.AddAllAssetsAtPath("Weapons/Sub Weapons", "Assets/Resources/Equipment/Weapons/", typeof(SubWeapon), true)
            .SortMenuItemsByName()
            .ForEach(this.AddDragHandles);

        tree.AddAllAssetsAtPath("Mods", "Assets/Resources/Equipment/Mods/", typeof(Mod), true)
            .SortMenuItemsByName()
            .ForEach(this.AddDragHandles);

        // Add icons to characters and items.
//        tree.EnumerateTree().AddIcons<Character>(x => x.Icon);
//        tree.EnumerateTree().AddIcons<Item>(x => x.Icon);

        // Add drag handles to items, so they can be easily dragged into the inventory if characters etc...
        tree.EnumerateTree().Where(x => x.ObjectInstance as Equipment)
            .ForEach(AddDragHandles);

        return tree;
    }

    private void AddDragHandles(OdinMenuItem menuItem) {
        menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.ObjectInstance, false, false);
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

            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Loadout"))) {
                ScriptableObjectCreator.ShowDialog<Loadout>("Assets/Resources/Equipment/Loadouts",
                    obj => {
                        obj.Name = obj.name;
                        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                    });
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
        public void UpdateEquipmentOverview() {
            // Finds and assigns all scriptable objects of type Character
            this.AllEquipment = AssetDatabase.FindAssets("t:Equipment")
                .Select(guid => AssetDatabase.LoadAssetAtPath<Equipment>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();
        }
#endif
    }

    public class AllEquipment {
        [BoxGroup("Weapons"), AssetList(Path = "Resources/Equipment/Weapons", AutoPopulate = true)] [InlineEditor]
        public List<MainWeapon> MainWeapons;

        [BoxGroup("Weapons"), AssetList(Path = "Resources/Equipment/Weapons", AutoPopulate = true)] [InlineEditor]
        public List<SubWeapon> SubWeapons;

        [BoxGroup("Mods"), AssetList(Path = "Resources/Equipment/Mods", AutoPopulate = true)] [InlineEditor]
        public List<Shield> Shields;
    }
}

public class Loadouts {
    public Loadouts() {
        RefreshList();
    }

    [InlineEditor] public List<Loadout> loadouts = new List<Loadout>();

    [Button("Refresh", ButtonSizes.Medium), PropertyOrder(-1)]
    public void RefreshList() {
        loadouts = AssetDatabase.FindAssets("t:Loadout")
            .Select(guid => AssetDatabase.LoadAssetAtPath<Loadout>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }
}