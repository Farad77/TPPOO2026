using UnityEngine;

namespace TP2_Heritage
{
    public class Zombie : Enemy
    {
        public Zombie(int Health = 100, int Damage = 10, float Speed = 2f, float DetectionRange = 10f) : base(Health, Damage, Speed, DetectionRange) { }
    }
}