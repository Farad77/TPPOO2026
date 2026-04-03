using UnityEngine;
using TP5;
public class ArmorBoots : MonoBehaviour
{
    [SerializeField] private Item equippedBoots;

    protected virtual void EquipArmor(Item armor)
    {
        equippedBoots = armor;
    }
}
