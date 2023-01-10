using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform[] trinketSlots;
    [SerializeField] TrinketInventory trinketInventory;
    TrinketManager trinketManager;
    void Awake()
    {
        trinketManager = TrinketManager.instance;
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < trinketSlots.Length; i++)
        {
            Trinket curTrinket = trinketInventory.GetTrinket(i);
            Image trinketSlotImg = trinketSlots[i].GetChild(0).GetComponent<Image>();
            if (curTrinket != null)
            {
                trinketSlotImg.enabled = true;
                trinketSlotImg.sprite = curTrinket.sprite;

            }
            else
            {
                trinketSlotImg.enabled = false;
            }

            
        }
    }

}
