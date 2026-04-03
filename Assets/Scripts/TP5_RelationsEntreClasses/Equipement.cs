using UnityEngine;

namespace TP5
{
    public class Equipement : MonoBehaviour
    {
        [SerializeField] protected string name;
        [SerializeField] protected string description;
        [SerializeField] protected float weight;
        [SerializeField] protected int value;
        [SerializeField] protected string itemType;
    }
}
