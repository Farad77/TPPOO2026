using UnityEngine;

namespace TP2_Heritage
{
    public class Skeleton : Enemy
    {
        public Skeleton(int Health = 80, int Damage = 15, float Speed = 3f, float DetectionRange = 12f) : base(Health, Damage, Speed, DetectionRange) { }
    }
}