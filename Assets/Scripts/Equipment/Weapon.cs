using Sirenix.OdinInspector;

public class Weapon : Equipment {
    [BoxGroup("Equipment Base/$Name")]
    public float BaseDamage;

    [BoxGroup("Equipment Base/$Name")]
    public Element Element;

    [BoxGroup("Equipment Base/$Name")]
    public float ElementPower;
}