using Mirror;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootingBehavior", menuName = "Scriptable Objects/ShootingBehavior")]
public abstract class ShootingBehavior : ScriptableObject
{
    public abstract void Shoot(Transform shooterTransform, Bullet bulletType, ref int currentAmmo, float fireRate);

    public void FireBullet(Vector3 position, Quaternion rotation, Bullet bulletType)
    {
        GameObject bulletObject = Instantiate(bulletType.bulletPrefab, position, rotation);
        NetworkServer.Spawn(bulletObject);
        BulletBehavior bulletBehavior = bulletObject.GetComponent<BulletBehavior>();
        bulletBehavior.Initialize(bulletType);
    }
}
