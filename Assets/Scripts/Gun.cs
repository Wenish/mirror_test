using Mirror;
using UnityEngine;

public enum ShootingMode
{
    Single,
    Burst,
    Auto,
    Cone
}

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public float shootingDistance;
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public int maxAmmo;
    public ShootingBehavior shootingBehavior; // Reference to the shooting behavior
    public Bullet bulletType;
}


public static class GunSerializer
{
    public static void WriteGun(this NetworkWriter writer, Gun gun)
    {
       writer.WriteString(gun.gunName);
    }

    public static Gun ReadGun(this NetworkReader reader)
    {
        string gunName = reader.ReadString();
        Gun loadedGun = Resources.Load<Gun>(gunName);
        return loadedGun;
    }
}