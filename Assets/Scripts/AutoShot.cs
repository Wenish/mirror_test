using UnityEngine;

[CreateAssetMenu(fileName = "AutoShot", menuName = "ShootingBehavior/AutoShot")]
public class AutoShot : ShootingBehavior
{
    public override void Shoot(Transform shooterTransform, Bullet bulletType, ref int currentAmmo, float fireRate)
    {
        if (currentAmmo <= 0) return;
        currentAmmo--;
        FireBullet(shooterTransform.position, shooterTransform.rotation, bulletType);
    }
}