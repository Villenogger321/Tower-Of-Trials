using UnityEngine;

[CreateAssetMenu(menuName = "Tower Of Trials/OnHit/HitDamage")]
public class OnHitOvertime : OnHitBase
{
    public int amountOfTicks;

    public override void OnHit(Health _hitee)
    {
        // add poison damage to tick manager
        
    }
}