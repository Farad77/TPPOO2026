using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public string _playerName;
    [SerializeField] protected int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed = 10;
    private int gold;
    private bool isInvincible;


  
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }

        if (health > maxHealth)
        {
            health = maxHealth; // Empêche la vie de dépasser son maximum.
        }
        moveSpeed = 10; //Empêche la vitesse de dépasser 10.
    }


    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            health = 0;
        }
    }

    public void Healing(int amount)
    {

    }

    public void GainGold(int amount)
    {
        gold += amount;
    }

    public void LoseGold(int amount)
    {
        gold -= amount;
        if (gold < 0)
        {
            gold = 0;
        }

        if (gold == 0)
        {
            print("Plus d'or");
        }
    }

    public void Regeneration()
    {

    }
}
