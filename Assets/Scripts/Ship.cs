using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Scriptable Objects/Ship")]
public class Ship : ScriptableObject
{
    public string shipName;
    public float speed;
    public Gun[] guns; // Array of guns the ship can equip
}
