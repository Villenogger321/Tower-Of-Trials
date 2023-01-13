using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator menuToInventory;
    public GameObject menuInventory;
    public Animator menuToSettings;
    public Animator inventoryToMenu;
    public Animator settingsToMenu;

    public void MenuToInventory()
    {
        menuInventory.SetActive(true);
        menuToInventory.SetBool("menuToInventory", true);
        menuToInventory.SetBool("menuToInventory", false);
    }

    public void MenuToSettings()
    {

    }

    public void InventoryToMenu()
    {

    }

    public void SettingsToMenu()
    {

    }

}
