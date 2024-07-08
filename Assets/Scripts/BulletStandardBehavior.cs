using UnityEngine;
using Mirror;

public class BulletStandardBehavior : BulletBehavior
{
    public override void Initialize(Bullet bulletData)
    {
        this.bulletData = bulletData;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletData.speed;
    }

    protected override void OnHit(Collider2D other)
    {
        // Apply damage
        // other.GetComponent<Health>()?.TakeDamage(bulletData.damage);
        // Apply special effect
        // SpecialEffectManager.Instance.ApplyEffect(bulletData.specialEffect, other.gameObject);
        NetworkServer.Destroy(gameObject);
    }
}
