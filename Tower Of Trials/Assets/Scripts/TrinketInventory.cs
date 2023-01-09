using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStats))]
public class TrinketInventory : MonoBehaviour
{
    [SerializeField] Trinket[] equippedTrinkets = new Trinket[5];
    public Trinket tempTrinket = new Trinket();
    PlayerStats playerStats;
    public void EquipTrinket(Trinket _trinket, int _slot)
    {
        UnequipTrinket(_slot);
        equippedTrinkets[_slot] = _trinket;

        playerStats.ApplyStats(_trinket.trinketStats);
        // despawn trinket in overworld
    }
    public void UnequipTrinket(int _slot)
    {
        if (equippedTrinkets[_slot] == null)
            return;

        playerStats.RemoveStats(equippedTrinkets[_slot].trinketStats);
        equippedTrinkets[_slot] = null;
        // spawn trinket in overworld
    }

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        
    }
    void AddNewTrinket(int _slot)
    {
        EquipTrinket(tempTrinket, _slot);
        // make sure trinket is available
    }
    void RemoveTrinket(int _slot)
    {
        UnequipTrinket(_slot);
    }
    void OnTrinketInteraction1(InputValue _input)
    {
        if (_input.isPressed)   // held
        {
            RemoveTrinket(0);
        }
        else    // tap
        {
            AddNewTrinket(0);
        } 
    }
    void OnTrinketInteraction2(InputValue _input)
    {
        if (_input.isPressed)   // held
        {
            RemoveTrinket(1);
        }
        else    // tap
        {
            AddNewTrinket(1);
        }
    }
    void OnTrinketInteraction3(InputValue _input)
    {
        if (_input.isPressed)   // held
        {
            RemoveTrinket(2);
        }
        else    // tap
        {
            AddNewTrinket(2);
        }
    }
    void OnTrinketInteraction4(InputValue _input)
    {
        if (_input.isPressed)   // held
        {
            RemoveTrinket(3);
        }
        else    // tap
        {
            AddNewTrinket(3);
        }
    }
    void OnTrinketInteraction5(InputValue _input)
    {
        if (_input.isPressed)   // held
        {
            RemoveTrinket(4);
        }
        else    // tap
        {
            AddNewTrinket(4);
        }
    }
}
