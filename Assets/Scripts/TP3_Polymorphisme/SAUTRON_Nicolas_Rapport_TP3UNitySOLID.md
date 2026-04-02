# Analyse TP3 - SOLID | SAUTRON Nicolas MAAJIC 2 JV
## Consigne 1 : Analysez le script WeaponManager.cs et identifiez les problèmes liés à l'absence de polymorphisme

> Chaque gameobject n'est pas une classe et ne possède pas ses propres attributs. Ce ne sont pas des entités d'elle mêmes, ce qui pourrait être clarifier par du polymorphisme

## Consigne 2 :  Créez une classe abstraite ou une interface Weapon qui définit les méthodes et propriétés communes à toutes les armes.

> Après une tentative d'interface Weapon, j'ai décidé de partir sur une classe abstraite en vue des variables que je souhaitez donnée. Ma classe Weapon a un nom d'arme significatif et paramètrables et la méthode Attack partagé par toutes les armes. (ref Weapon.cs)

## Consigne 3 :  Implémentez des classes dérivées pour chaque type d'arme (Sword, Bow, Wand) avec leurs comportements spécifiques.

> Chaque arme est une sorte de Weapon, qui chacune a leur nom différent et une façon d'attaquer différentes. (ref Sword/Bow/Wand/Axe.cs)

## Consigne 4 :  Refactorisez le WeaponManager pour qu'il utilise le polymorphisme, simplifiant ainsi le code et le rendant plus extensible.

> J'ai gardé la méthode Attack pour pouvoir utilisé l'attaque de l'arme actuel du weapon manager. Une gestion de la liste d'arme disponible en List<Weapon\>, avec un SwitchWeapon changé pour passé en paramètre un Weapon. Le SwitchWeapon lui même est appelé dans une méthode NextWeapon, servant à passé d'arme en arme lorsque le joueur appuie sur la touche "E". (ref WeaponManager.cs / PlayerController.cs ligne 86). 

## Consigne 5 :  Ajoutez une nouvelle arme (par exemple, une hache) pour démontrer la facilité d'extension de votre système.

> La hache que j'ai rajouté a les mêmes fonctionnement de l'épée mais en ayant des dégats en +. L'implémentation n'est pas compliqué étant donné qu'il faut juste lui attribuer un nom et une façon d'attaquer. (ref Axe.csw²)
