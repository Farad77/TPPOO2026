# TP1 : Encapsulation en C# pour Unity
## Explication de mes choix d'implémentation
L'ensemble des variables ont été passées en privées.
### ```playerName```
Deux méthodes ont été ajoutées : 
- Le getter ```GetPlayerName```. Il retourne simplement le nom du joueur.
- Le setter ```SetPlayerName```. Permet de changer le nom du joueur en vérifiant les points suivants :
    - Le nouveau nom ne doit pas être null ou vide.
### ```health```
Quatre méthodes ont été ajoutées ici : 
- Un getter ```GetHealth```. Il retourne la santé actuelle du joueur.
- ```IsDead```. Elle retourne true ou false en fonction de l'état de santé du joueur. Il s'agit d'une fonction helper qui évite de devoir faire le calcul ailleurs.
- ```TakeDamage```. Permet d'appliquer des dégâts au joueur de manière contrôlée. Des vérifications sont faites avant de retirer des points de vie.
    - Le joueur ne doit pas être invincible.
    - On s'assure que la santé du joueur reste dans les bornes 0 et ```maxHealth``` après avoir appliqué les dégâts.
    - On vérifie si le joueur est mort après avoir appliqué les dégâts.
- ```Heal```. Permet d'ajouter des points de vie au joueur. On vérifie les points suivants : 
    - On ne peut ajouter de points de vie négatifs.
    - On vérifie que les points de vie finaux après ajout restent dans les limites 0 et ```maxHealth```.
### ```moveSpeed```
Deux méthodes ont été ajoutées ici :
- Le getter ```GetMoveSpeed```. Elle retourne simplement la vitesse de déplacement du joueur.
- Le setter ```SetMoveSpeed```. Permet d'appliquer une nouvelle valeur à la vitesse du joueur. Les vérifications suivantes sont faites à cette nouvelle valeur : 
    - La nouvelle valeur doit rester dans les bornes 0 et 100.
### ```gold```
Trois méthodes ont été ajoutées : 
- Le getter ```GetGold```. Il retourne simplement la quantité d'or que possède le joueur.
- ```AddGold```. Ajoute une certaine quantité d'or au joueur. Les vérifications suivantes sont faites à la nouvelle valeur : 
    - On ne peut pas ajouter de quantité négative d'or.
- ```RemoveGold```. Retire une certaine quantité d'or au joueur. Les vérifications suivantes sont faites sur la nouvelle valeur :
    - On ne peut pas retirer de valeur négative d'or.
    - On ne peut pas retirer plus d'or que n'en possède le joueur.
### ```isInvincible```
Trois méthodes ont été ajoutées :
- ```IsInvincible```. Retourne l'état d'invincibilité du joueur.
- ```ActivateInvincibility```. Active l'état d'invincibilité pendant une période passée en paramètres. La méthode vérifie si le joueur n'est pas déjà invincible.
- ```ActiveInvincibilityRoutine```. Une coroutine qui se charge de modifier en interne la valeur de ```isInvincible``` pendant le temps passé en paramètres.