using Sirenix.OdinInspector;

public class Weapon : Equipment {
    [BoxGroup("Equipment Base/$Name", false)]
    public DamageType DamageType;
}