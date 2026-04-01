using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private string playerName;
    private int health;
    private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    private int gold;
    [SerializeField] private bool isInvincible;

    // Getters
    public string GetPlayerName()
    {
        return playerName;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public int getGold()
    {
        return gold;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public bool GetIsInvincible()
    {
        return isInvincible;
    }

    // Setters
    public void SetName(string name)
    {
        playerName = name;
    }

    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
    }


    // Fonctions 
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
    }


    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            health -= amount;

            if (health < 0)
            {
                health = 0;
                Debug.Log("Player is dead");
            }
        }
    }

    public void GiveHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Update()
    {
        if (moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }
        if (moveSpeed < 0)
        {
            moveSpeed = 0;
        }
    }
}
