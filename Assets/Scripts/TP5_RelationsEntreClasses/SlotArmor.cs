using TP5;
using UnityEngine;
using UnityEngine.UI;

public class SlotArmor : MonoBehaviour
{
    [SerializeField]
    private string slotName;

    [SerializeField]
    private Armor armorEquipped;
    [SerializeField]
    private Image slotArmorImage;


    public string SlotName { get => slotName; set => slotName = value; }
    public Armor ArmorEquipped { get => armorEquipped; set => armorEquipped = value; }



    public void EquipArmor(Armor armor)
    {
        if (ArmorEquipped != null)
        {
            UnequipArmor();
        }
        armor.DestroyItem();
        ArmorEquipped = armor;
        ArmorEquipped.AppearObject(gameObject);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (slotArmorImage != null)
        {
            if (armorEquipped != null)
                slotArmorImage.sprite = armorEquipped.ItemSprite;
            else
                slotArmorImage.sprite = null;
        }
    }

    public Armor UnequipArmor()
    {
        Debug.Log("Désiquipement de l'armure" + slotName);
        ArmorEquipped?.DestroyItem();
        Armor armor = ArmorEquipped;
        ArmorEquipped = null;
        UpdateUI();
        return armor;
    }

}
