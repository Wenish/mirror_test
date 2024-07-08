using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ShootingBehavior/SingleShot")]
public class SingleShot : ShootingBehavior
{
    public override void Shoot(Transform shooterTransform, Bullet bulletType, ref int currentAmmo, float fireRate)
    {
        if (currentAmmo <= 0) return;
        currentAmmo--;
        FireBullet(shooterTransform.position, shooterTransform.rotation, bulletType);
    }
}

[CreateAssetMenu(menuName = "ShootingBehavior/BurstShot")]
public class BurstShot : ShootingBehavior
{
    private int burstAmmo;

    public override void Shoot(Transform shooterTransform, Bullet bulletType, ref int currentAmmo, float fireRate)
    {
        if (currentAmmo <= 0) return;
        burstAmmo = currentAmmo; // Store current ammo in a class field
        shooterTransform.GetComponent<MonoBehaviour>().StartCoroutine(FireBurst(shooterTransform, bulletType, fireRate));
        currentAmmo = burstAmmo; // Update the original ammo count after the burst
    }

    IEnumerator FireBurst(Transform shooterTransform, Bullet bulletType, float fireRate)
    {
        int burstCount = 3;
        for (int i = 0; i < burstCount; i++)
        {
            if (burstAmmo <= 0) yield break;
            burstAmmo--;
            FireBullet(shooterTransform.position, shooterTransform.rotation, bulletType);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }

}

[CreateAssetMenu(menuName = "ShootingBehavior/ConeShot")]
public class ConeShot : ShootingBehavior
{
    public override void Shoot(Transform shooterTransform, Bullet bulletType, ref int currentAmmo, float fireRate)
    {
        if (currentAmmo <= 0) return;

        int bulletCount = 5;
        float spreadAngle = 30f;
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            if (currentAmmo <= 0) break;
            currentAmmo--;
            float angle = -spreadAngle / 2 + angleStep * i;
            Quaternion rotation = shooterTransform.rotation * Quaternion.Euler(0, 0, angle);
            FireBullet(shooterTransform.position, rotation, bulletType);
        }
    }
}