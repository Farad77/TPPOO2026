# Analyse TP1 - SOLID | SAUTRON Nicolas MAAJIC 2 JV
## Consigne 1 : 
1. Analysez le script PlayerCharacterBroken.cs et identifiez tous les problèmes liés à l'absence
d'encapsulation.

``` C#

using UnityEngine;

namespace TP1_Encapsulation
{
    public class PlayerCharacterBroken : MonoBehaviour
    {
        // Toutes les données sont publiques et peuvent être modifiées n'importe où
        public string playerName;
        public int health; 
        public int maxHealth;
        public float moveSpeed;
        public int gold;
        public bool isInvincible;
        
        void Update()
        {
            
            if (health <= 0)
            {
                Debug.Log("Player is dead");
            }

            // La vitesse peut être modifiée à n'importe quelle valeur
            // Le personnage peut avoir une vitesse négative car rien ne l'empêche
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
        public void GainGold(int amount)
        {
            gold += amount;
        }

       
        public void TakeDamage(int amount)
        {
            health -= amount;
        }
    }
}

```

> # Analyse 
> 1. Toutes les variables sont publiques et donc peuvent être modifiées n'importe où
> 2. Aucun Getter Setter n'est mi en place pour la sécurité des données
> 3. Etant donnée la non présence d'encapsulation, beaucoup de problème pourront être vu et compliqué à corriger à la longue sur la gestion des valeurs positif et négatif des variables (health, moveSpeed, gold)

## Consigne 2
> Pour corrigé le problème d'encapsulation, j'ai passé les variables en private, rendu ceux pouvant être nécéssaire a des moments de débug ou de test éditable dans l'éditeur, et fais les getters/setters + autre méthode au besoin.

``` C#

    [SerializeField]
    private string playerName;
    [Header("Health Settings")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [Header("Speed Settings")]
    [Range(0f, maxSpeed)]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private const float maxSpeed = 100f;
    [Header("Gold Settings")]
    [SerializeField]
    private int gold;
    [Header("Invincibility Settings")]
    [SerializeField]
    private bool isInvincible;
    [SerializeField]
    private float invincibilityDuration = 5f;

```

### PlayerName

> #### getPlayerName
> Seul point de lecture de la variable playerName.

> #### setPlayerName
> Seul point de modification du playerName pour pas que la variable soit paramètrable à tous va.
``` C#
    [SerializeField]
    private string playerName;
     #region PlayerName
    /// <summary>
    /// Permet d'obtenir le nom du personnage. Le nom peut être définiuniquement via le setter
    /// </summary>
    /// <returns></returns>
    public string getPlayerName()
    {
        return playerName;
    }
    /// <summary>
    /// Permet de définir le nom du personnage. Le nom peut être définiuniquement via ce setter pour éviter des modifications non contrôlées.
    /// </summary>
    /// <param name="newName"></param>
    public void setPlayerName(string newName)
    {
        playerName = newName;
    }
    #endregion
```

### Health
> #### getHealth
> Seul point de lecture de la variable health.

``` c#

    /// <summary>
    /// Permet d'obtenir la santé actuelle du personnage. La santé est limitéeentre 0 et maxHealth via 
    /// les méthodes TakeDamage et Heal.
    /// </summary>
    /// <returns></returns>
    public float getHealth()
    {
        return health;
    }

```

> #### setHealth :  
> Seul point pour modifier la variable health, vérifiant le montant de PV donné afin qu'il n'aille pas dans le négatif ou biens au delà des PV Max. Utilisable pour un changement de PV classique mais aussi utilisé dans le TakeDamage / Heal

``` C#
    /// <summary>
    /// Permet de définir la santé du personnage. La santé est limitée entre 0et maxHealth pour éviter des valeurs non réalistes ou négatives.
    /// La santé ne peut pas être modifiée directement, mais uniquement viales méthodes TakeDamage et Heal, qui appliquent les règles de santé.
    /// </summary>
    /// <param name="newHealth"></param>
    /// <returns></returns>
    public float setHealth(int newHealth)
    {
        if (newHealth < 0)
            health = 0;
        else if (newHealth > maxHealth)
            health = maxHealth;
        else
            health = newHealth;
        return health;
    }

```
> #### Heal : 
> Va rajouter des PV au fonction du montant donné. Appel du setHealth pour la vérification.


``` C#
        /// <summary>
        /// Permet de soigner le personnage en augmentant sa santé. La santé ne peut pas dépasser maxHealth et 
        /// ne peut pas être soignée si elle est déjà à 0 ou à maxHealth.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount)
        {
            if (health >= maxHealth || health <= 0)
            {
                return;
            }

            int temphealth = health + amount;
            setHealth(temphealth);
        }
```
> #### TakeDamage : 
> Va enlever des PV en fonction du montant donnée. Appel du setHealth pour la vérification. Impossible de perdre des PV si le personnage est invincible.

``` C#
        /// <summary>
        /// Permet d'infliger des dégâts au personnage en réduisant sa santé. La santé ne peut pas descendre en dessous de 0 
        /// et ne peut pas être endommagée si elle est déjà à 0 ou si le personnage est invincible.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if(health <= 0 || isInvincible)
            {
                return;
            }

            health -= amount;
            if (health < 0)
            {
                health = 0;
            }
        }
```


### Speed
> #### getMoveSpeed
> Seul point d'accès a la variable moveSpeed

``` c#

    /// <summary>
    /// Permet d'obtenir la vitesse de déplacement du personnage. La vitesse est limitée entre 0 et maxSpeed 
    /// via la méthode setMoveSpeed.
    /// </summary>
    /// <returns></returns>
    public float getMoveSpeed()
    {
        return moveSpeed;
    }

```

> #### setMoveSpeed
> Seul point d'accès pour la modification de moveSpeed. La vitesse est limité entre 0 et la valeur mise dans MaxSpeed afin d'éviter les valeurs non réalistes ou/et négatives.
``` c#
        /// <summary>
        /// Permet de définir la vitesse de déplacement du personnage. La vitesse est limitée entre 0 et 100 
        /// pour éviter des valeurs non réalistes ou négatives.
        /// </summary>
        /// <param name="newSpeed"></param>
        public void setMoveSpeed(float newSpeed)
        {
            if (newSpeed <= 0)
                moveSpeed = 0;

            else if (newSpeed > 100)
                moveSpeed = 100;

            else
                moveSpeed = newSpeed;
        }

```
### Gold

> #### getGold
> Seul point d'accès de lecture pour la variable gold
``` c#
    /// <summary>
    /// Permet d'obtenir la quantité d'or du personnage. L'or ne peut êtremodifié que via les méthodes GainGold et SpendGold.
    /// </summary>
    /// <returns></returns>
    public float getGold()
    {
        return gold;
    }
```

> #### setGold
> Seul point d'accès de modification pour la variable Gold. Utilisé dans GainGold et SpendGold.
``` c#
    /// <summary>
    /// Permet de définir la quantité d'or du personnage. L'or ne peut êtrenégatif, 
    /// et ne peut être modifié que via les méthodes GainGold et SpendGold.
    /// </summary>
    /// <param name="amount"></param>
    public void setGold(int amount)
    {
        gold = amount;
    }
```
> #### GainGold
> Ajout d'or par la valeur passé en paramètre. Vérification que cette valeur soit positif
``` c#
    /// <summary>
    /// Permet d'augmenter la quantité d'or du personnage. L'or ne peut pasêtre négatif, 
    /// et ne peut être modifié que via les méthodes GainGold et SpendGold.
    /// </summary>
    /// <param name="amount"></param>
    public void GainGold(int amount)
    {
        if(amount < 0)
        {
            Debug.Log("Cannot gain a negative amount of gold!");
            return;
        }
        int tempGold = gold + amount;
        setHealth(tempGold);
    }
```
> #### SpendGold
> Retirez l'or en fonction de la valeur passé en paramètre. Vérification si il y a assez d'or à retirez, sinon cela veut dire que le personnage ne peut pas dépensé cette argent et donc aller dans le négatif.
>> ( Pratique pour plus tard pour un système d'achat)
``` c#
    /// <summary>
    /// Permet de réduire la quantité d'or du personnage. L'or ne peut pasêtre négatif. 
    /// Le personnage ne peut pas dépenser plus d'or qu'il n'en possède.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool SpendGold(int amount)
    {
        if (amount > gold)
        {
            Debug.Log("Not enough gold to spend!");
            return false;
        }
        int tempGold = gold - amount;
        setHealth(tempGold);
        return true;
    }
```

### Invincibility

> #### isPlayerInvincible
> Seul point d'accès de lecture à la varialbe isInvincible
>> ( Pratique pour plus tard pour un système d'achat)
``` c#
    public bool isPlayerInvincible()
    {
        return isInvincible;
    }
```


> #### SetInvincibility
> Permet de : en fonction de si on souhaite ou non passé le personnage invincible, de si mettre une durée paramètre de sa phase d'invincibilité. (A animé)
``` c#
    /// <summary>
    /// Permet de définir l'invincibilité du personnage. Lorsque le personnagedevient invincible, 
    /// il ne peut pas subir de dégâts pendant une durée définie parinvincibilityDuration.
    /// </summary>
    /// <param name="invincible"></param>
    public void SetInvincibility(bool invincible)
    {
        if (invincible)
        {
            Debug.Log("Player is now invincible!");
            StartCoroutine(InvincibilityDuration(invincibilityDuration));
        }
        else
        {
            Debug.Log("Player is no longer invincible.");
            isInvincible = false;
        }
    }
```

> #### InvincibilityDuration
> Passe le personnage invincible durant une certaines durée puis le remet sensible aux dégats.
``` c# 
    /// <summary>
    /// Coroutine qui gère la durée de l'invincibilité du personnage. Pendantla durée d'invincibilité, le personnage ne peut pas subir de dégâts.
    /// </summary>
    /// <param name="invincibilityDuration"></param>
    /// <returns></returns>
    private IEnumerator InvincibilityDuration(float invincibilityDuration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        Debug.Log("Player's invincibility has worn off.");
    }
    #endregion
```

#### XP & Level

> #### getXP
> Seul point d'accès de lecture de la variable xp
``` c#
    public int getXP()
    {
        return xp;
    }
```


> #### setXP
> Permet de modifier l'xp tout en vérifiant si l'xp donné est négatif ou que si elle dépasse l'exp nécessaire pour monté de level, dans ce cas là augmenté de niveau, dans le pire des cas juste établir l'xp actuel à celui mis en paramètre.
``` c#
    public void setXP(int newXP)
    {
        if (newXP < 0)
            xp = 0;
        else if (newXP > xpToNextLevel)
        {
            xp = xpToNextLevel - xp; // Reste de l'XP après le niveau 
            xpToNextLevel +=100; // Augmente la quantité d'XP nécessaire pour le prochain
            AddLevel(1);
        }
        else
            xp = newXP;
    }
```

> #### AddXP
> Permet de rajouté une bonne quantité d'XP. La valeur sera vérifieé dans le setXP
``` c#
    public void AddXP(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("Cannot add a negative amount of XP!");
            return;
        }
        int tempXP = xp + amount;
        setXP(tempXP);
    }
```

> #### getLevel
> Seul point d'accès de lecture de la variable level
``` c#
    public int getLevel()
    {
        return level;
    }
```
> #### setLevel
> Seul point d'accés à la modification du level, accéptant que des level positif.
``` c#
    public void setLevel(int newLevel)
    {
        if (newLevel < 0)
            {
            Debug.Log("Level cannot be negative!");
            return;
        }
        level = newLevel;
    }
```
> #### AddLevel
> Permet de rajouter un nombre de niveau positif, (vérifier dans le setLevel) + augmenté le multiplicateur de stat
``` c#
    public void AddLevel(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("Cannot add a negative amount of levels!");
            return;
        }
        statMultiplier += 0.1f; // Augmente le multiplicateur de stats à chaque niveau
        int tempLevel = level + amount;
        setLevel(tempLevel);
    }
```


