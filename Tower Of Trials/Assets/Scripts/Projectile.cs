using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Action<Health> onHitTrigger;
    void OnTriggerEnter2D(Collider2D _col)
    {
        // do all checks
        if (_col.GetComponent<Health>() is Health _health)
        {
            onHitTrigger.Invoke(_health);
        }
        Destroy(gameObject);
    }
    public void AssignToProjectile(Action<Health> _onHit)
    {
        onHitTrigger += _onHit;
    }
}
public abstract class OnHitBase : ScriptableObject
{
    public abstract void OnHit(Health _hitee);
}
