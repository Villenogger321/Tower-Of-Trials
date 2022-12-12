using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Action<Health> onHitTrigger;
    private bool playerOwned;

    void OnTriggerEnter2D(Collider2D _col)
    {
        // do all checks
        if (playerOwned && _col.CompareTag("Player"))
            return;

        if (_col.GetComponent<Health>() is Health _health)
        {
            onHitTrigger.Invoke(_health);
        }
        Destroy(gameObject);
    }
    // assign on hit methods to action
    public void AssignToProjectile(Action<Health> _onHit)
    {
        onHitTrigger += _onHit;
    }
    // assign the owner of the projectile (for hit detection)
    public void AssignOwner(bool _playerOwned)
    {
        playerOwned = _playerOwned;
    }
}
public abstract class OnHitBase : ScriptableObject
{
    public abstract void OnHit(Health _hitee);
}
