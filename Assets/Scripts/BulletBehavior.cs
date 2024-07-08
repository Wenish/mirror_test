using UnityEngine;
using Mirror;

public abstract class BulletBehavior : NetworkBehaviour
{
    public Bullet bulletData;

    public abstract void Initialize(Bullet bulletData);

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isServer) return;
        OnHit(other);
    }

    protected abstract void OnHit(Collider2D other);

    void Start() {
        if (!isServer) return;

        Invoke(nameof(DestroyBullet), bulletData.lifetime);
    }

    [Server]
    void DestroyBullet()
    {
        // Ensure this method is only called on the server
        NetworkServer.Destroy(gameObject);
    }
}