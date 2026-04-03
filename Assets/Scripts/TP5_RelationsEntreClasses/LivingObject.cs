using TP5;
using UnityEngine;

public class LivingObject : MonoBehaviour, IHealth
{
    [SerializeField] private string livingName;
    public string LivingName
    {
        get => livingName;
        private set { }
    }


    [SerializeField] protected int maxHealth;
    [SerializeField] private int health;
    public int Health
    {
        get => health;
        set
        {
            health = (value <= 0) ? 0 : (value >= maxHealth) ? maxHealth : value;
            if (health == 0) Death();
        }
    }

    public void Damage(int amount)
    {
        if (amount <= 0) return;
        health -= amount;
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        System.Console.WriteLine($"{livingName} restaure {amount} points de vie!");
        health += amount;
    }


    public void Death() { }

}
