using Sirenix.OdinInspector;
using UnityEditor;

public class Weapon : Equipment {
    [BoxGroup("Equipment Base/$Name", false)] public float Damage;
    [BoxGroup("Equipment Base/$Name")] public DamageType DamageType;
}