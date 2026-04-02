# Analyse TP2 - SOLID | SAUTRON Nicolas MAAJIC 2 JV
## Consigne 1 : Analysez les scripts Zombie.cs et Skeleton.cs et identifiez les parties communes qui pourraient être factorisées.

> Toutes les parties peuvent être factorisées et on des points communs car vu comment le projet RPG est prévu à l'origine, la détection de l'update est la même pour les deux entités + font la même intéraction dans le OnCollisionEnter + même setup de player (Améliorable). 

## Consigne 2 : Créez une classe de base abstraite Enemy qui contient tout le code commun.

> J'ai donc construit ma classe abstraite Enemy (cf. Enemy.cs) en copie collant le code du Squelette (ayant le meilleur squelette de code (ironique)). En sachant que chaque variables pouvant être nécessaire dans l'implémentation ou le changement d'entités dans le futur à était passé en protected, ainsi que ce je pensais aussi nécéssaire a l'implémentation de différentes IA comme par exemple l'update passé en protected virtual ainsi que le OnCollisionEnter

## Consigne 3 : Modifiez les classes Zombie et Skeleton pour qu'elles héritent de la classe Enemy

> Pour correspondre à la consigne de base j'ai juste remplacé le MonoBehaviour des scripts Enemy et Skeleton. 

## Consigne 4 : Utilisez les modificateurs d'accès appropriés (private, protected, public) pour les attributs et méthodes

> Chaque attributs à été passé en protected. Le TakeDamage est resté public car il peut être appelé par n'importe quel script. Et le OnCollisionEnter est passé en protected virtual pour être surchargé au cas où.

## Consigne 5 : Implémentez des méthodes virtuelles dans la classe de base pour les comportements qui peuvent être surchargés

> TakeDamage et OnCollisionEnter comme dis plus haut.

## Consigne 6 : Ajoutez un troisième type d'ennemi (Boss.cs) qui hérite également de Enemy mais avec des comportements spécifiques

> Le boss possède un comportement de RangeEnemy dans le sens qu'il lance une fireball quand il est à porté du joueur (En prévision de l'IA augmenté pour le Squelette)