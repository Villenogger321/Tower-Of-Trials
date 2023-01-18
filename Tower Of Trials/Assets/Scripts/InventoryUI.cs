using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform[] trinketSlots;
    [SerializeField] Transform pickupSlot, pickupBox;

    [SerializeField] TextMeshProUGUI pickupTitleText, pickupDescText;
    [SerializeField] TextMeshProUGUI titleText, descText;

    public int selectedSlotInt;
 
    [SerializeField] TrinketInventory trinketInventory;
    TrinketManager trinketManager;
    public static InventoryUI Instance;
    void Start()
    {
        trinketManager = TrinketManager.Instance;
        trinketInventory = GameObject.Find("Player").GetComponent<TrinketInventory>();
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void UpdateInventory()
    {
        pickupBox.gameObject.SetActive(false);
        for (int i = 0; i < trinketSlots.Length; i++)
        {
            Trinket curTrinket = trinketInventory.GetTrinket(i);
            Image trinketSlotImg = trinketSlots[i].GetChild(0).GetComponent<Image>();
            if (curTrinket != null)
            {
                trinketSlotImg.enabled = true;
                trinketSlotImg.sprite = trinketManager.GetInventoryTrinketSprite
                    (curTrinket.rarity, curTrinket.spriteId);
            }
            else
            {
                trinketSlotImg.enabled = false;
            }            
        }
    }
    public void SelectedSlot(int _slot)
    {
        for (int i = 0; i < trinketSlots.Length; i++)
        {
            if (i == _slot) // selected this slot
            {
                selectedSlotInt = i;
                SelectSlot();
                return;
            }
        }
    }
    void UnselectSlot(int _slot)
    {
        titleText.text = "";
        descText.text = "";
    }
    void SelectSlot()
    {
        Trinket selectedTrinket = trinketInventory.GetTrinket(selectedSlotInt);
        
        if (selectedTrinket == null || selectedTrinket.name == "")
        {
            UnselectSlot(selectedSlotInt);
            return;
        }
        titleText.text = trinketInventory.GetTrinket(selectedSlotInt).name;
        descText.text = "";

        descText.text = DescriptionGenerator(selectedTrinket);
    }
    public void PickupTrinket(Trinket _trinket)
    {
        pickupBox.gameObject.SetActive(true);

        Image pickupSlotImg = pickupSlot.GetComponent<Image>();
        pickupSlotImg.sprite = trinketManager.GetInventoryTrinketSprite
                    (_trinket.rarity, _trinket.spriteId);

        pickupTitleText.text = _trinket.name;
        pickupDescText.text = DescriptionGenerator(_trinket);
    }
    string DescriptionGenerator(Trinket _trinket)
    {
        string description = "";
        for (int i = 0; i < _trinket.trinketStats.Count; i++)
        {
            description = description + "+" + _trinket.trinketStats[i].statModifier.ToString()
                + " " + UpperFirst(_trinket.trinketStats[i].type.ToString());
        }
        description = description + "\n" + _trinket.rarity;
        description = description.Replace("common", "<color=green>Common</color>");
        description = description.Replace("rare", "<color=blue>Rare</color>");
        description = description.Replace("epic", "<color=purple>Epic</color>");
        description = description.Replace("legendary", "<color=yellow>Legendary</color>");

        return description;
    }
    string UpperFirst(string text)
    {
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }
}
