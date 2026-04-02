using System.Collections.Generic;
using UnityEngine;

namespace TP3_Polymorphisme.Correction
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;
        [SerializeField] private KeyCode[] weaponHotkeys;

        private Weapon currentWeapon;
        private int currentWeaponIndex = 0;

        private void Start()
        {
            // Vérifie que les tableaux d'armes et de touches sont valides
            if (weapons == null || weapons.Length == 0)
            {
                Debug.LogError("No weapons assigned to WeaponManager!");
                return;
            }

            // S'assure que toutes les armes sont désactivées au départ
            foreach (var weapon in weapons)
            {
                if (weapon != null)
                    weapon.gameObject.SetActive(false);
            }

            // Active l'arme initiale
            SwitchWeapon(0);
        }

        private void Update()
        {
            // Écoute les touches pour changer d'arme
            if (weaponHotkeys != null && weaponHotkeys.Length > 0)
            {
                for (int i = 0; i < weaponHotkeys.Length && i < weapons.Length; i++)
                {
                    if (Input.GetKeyDown(weaponHotkeys[i]))
                    {
                        SwitchWeapon(i);
                        break;
                    }
                }
            }

            // Raccourci pour faire défiler les armes
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchToNextWeapon();
            }

            // Attaque avec l'arme actuelle
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        // Méthode d'attaque simplifiée grâce au polymorphisme
        public void Attack()
        {
            if (currentWeapon != null)
            {
                currentWeapon.Attack();
            }
        }

        // Méthode pour changer d'arme par index
        public void SwitchWeapon(int index)
        {
            if (index < 0 || index >= weapons.Length || weapons[index] == null)
                return;

            // Désactive l'arme précédente
            if (currentWeapon != null)
                currentWeapon.gameObject.SetActive(false);

            // Active la nouvelle arme
            currentWeaponIndex = index;
            currentWeapon = weapons[currentWeaponIndex];
            currentWeapon.gameObject.SetActive(true);

            Debug.Log($"Switched to {currentWeapon.WeaponName}");

            // Déclenche un événement ou une animation de changement d'arme
            OnWeaponSwitched();
        }

        // Méthode pour changer d'arme par nom
        public void SwitchWeapon(string weaponName)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] != null && weapons[i].WeaponName.ToLower() == weaponName.ToLower())
                {
                    SwitchWeapon(i);
                    return;
                }
            }

            Debug.LogWarning($"Weapon '{weaponName}' not found!");
        }

        // Méthode pour passer à l'arme suivante dans la liste
        public void SwitchToNextWeapon()
        {
            int nextIndex = (currentWeaponIndex + 1) % weapons.Length;
            SwitchWeapon(nextIndex);
        }

        // Méthode pour obtenir l'arme actuelle
        public Weapon GetCurrentWeapon()
        {
            return currentWeapon;
        }

        // Méthode appelée lors d'un changement d'arme
        private void OnWeaponSwitched()
        {
            // Pourrait déclencher des sons, animations, effets UI, etc.

            // Exemple d'accès à des fonctionnalités spécifiques selon le type d'arme
            if (currentWeapon is Bow bow)
            {
                // Afficher le nombre de flèches dans l'UI
                UpdateArrowCountUI(bow.GetArrowCount());
            }
            else if (currentWeapon is Wand)
            {
                // Afficher la barre de mana puisqu'on utilise une arme magique
                ShowManaUI(true);
            }
            else
            {
                // Cacher la barre de mana pour les armes non-magiques
                ShowManaUI(false);
            }
        }

        // Méthodes simulées pour l'UI
        private void UpdateArrowCountUI(int count)
        {
            Debug.Log($"Arrow count: {count}");
            // Mise à jour de l'UI ici
        }

        private void ShowManaUI(bool show)
        {
            Debug.Log(show ? "Showing mana bar" : "Hiding mana bar");
            // Mise à jour de l'UI ici
        }

        // NOUVELLE MÉTHODE: Ajouter une arme au manager
        public void AddWeapon(Weapon newWeapon, bool switchToWeaponOnPickup = true)
        {
            if (newWeapon == null)
                return;

            // Créer un nouveau tableau avec une taille augmentée de 1
            Weapon[] newWeapons = new Weapon[weapons.Length + 1];

            // Copier les armes existantes
            for (int i = 0; i < weapons.Length; i++)
            {
                newWeapons[i] = weapons[i];
            }

            // Ajouter la nouvelle arme
            newWeapons[newWeapons.Length - 1] = newWeapon;

            // Mettre à jour le tableau d'armes
            weapons = newWeapons;

            // Optionnel: ajouter également une nouvelle touche
            if (weaponHotkeys != null && weaponHotkeys.Length > 0)
            {
                KeyCode[] newHotkeys = new KeyCode[weaponHotkeys.Length + 1];
                for (int i = 0; i < weaponHotkeys.Length; i++)
                {
                    newHotkeys[i] = weaponHotkeys[i];
                }
                // Assigner une touche par défaut ou laisser vide
                newHotkeys[newHotkeys.Length - 1] = KeyCode.None;
                weaponHotkeys = newHotkeys;
            }

            // Si requis, passer immédiatement à cette arme
            if (switchToWeaponOnPickup)
            {
                SwitchWeapon(weapons.Length - 1);
            }

            Debug.Log($"Added new weapon: {newWeapon.WeaponName}");
        }
    }
}