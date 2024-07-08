using UnityEngine;
using Mirror;
using System.Collections;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public Ship ship;
    [SyncVar]
    private Gun currentGun;
    [SyncVar]
    private int currentAmmo;
    [SyncVar]
    private float nextFireTime;

    void Start()
    {
        if (!isLocalPlayer) return;

        // Initialize the player's ship and its guns
        if (!isServer) return;
        EquipGun(ship.guns[0]); // Example: equip the first gun
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        // Shooting logic
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0)
        {
            CmdShoot();
        }

        // Reload logic
        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdReload();
        }
    }

    [Command]
    void CmdShoot()
    {
        currentGun.shootingBehavior.Shoot(transform, currentGun.bulletType, ref currentAmmo, currentGun.fireRate);
        nextFireTime = Time.time + 1f / currentGun.fireRate;
    }

        [Command]
    void CmdReload()
    {
        StartCoroutine(Reload());
    }

    [Server]
    void EquipGun(Gun gun)
    {
        currentGun = gun;
        currentAmmo = currentGun.magazineSize;
    }

    IEnumerator Reload()
    {
        // Reload logic
        yield return new WaitForSeconds(currentGun.reloadTime);
        currentAmmo = currentGun.magazineSize;
    }
}