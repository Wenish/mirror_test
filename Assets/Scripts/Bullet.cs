using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Objects/Bullet")]
public class Bullet : ScriptableObject
{
    public string bulletName;
    public float speed;
    public float damage;
    public string specialEffect;
    public GameObject bulletPrefab;
    public float lifetime;
}