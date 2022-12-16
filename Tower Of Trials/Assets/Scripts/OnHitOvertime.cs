using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Of Trials/OnHit/HitDamage")]
public class OnHitOvertime : OnHitBase
{
    [SerializeField] private int amountOfTicks;
    [SerializeField] private int damage;
    [SerializeField] private DamageType type;

    public override void OnHit(Health _hitee)
    {
        // add fire damage to tick manager
        _hitee.ApplyNewDamageOverTime(new DamageOverTime(amountOfTicks, damage, type));
    }
}