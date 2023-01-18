using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStats))]
public class TrinketInventory : MonoBehaviour
{
    [SerializeField] float pickupDistance = 1;
    [SerializeField] Trinket[] equippedTrinkets = new Trinket[5];
    [SerializeField] List<Transform> closeTrinkets = null;
    [SerializeField] Transform closestTrinket = null;
    [SerializeField] Transform trinketTooltip, trinketTooltipPrefab;
    [SerializeField] Vector3 tooltipOffset;

    PlayerStats playerStats;
    TrinketManager trinketManager;
    UIManager uiManager;
    InventoryUI inventoryUI;


    private FMOD.Studio.EventInstance equipTrinketInstance;
    private FMOD.Studio.EventInstance unEquipTrinketInstance;

    public void EquipTrinket(Trinket _trinket, int _slot)
    {
        ////////////////////////// equip sfx
        equipTrinketInstance.start();


        UnequipTrinket(_slot);
        equippedTrinkets[_slot] = _trinket;
        playerStats.ApplyStats(_trinket.trinketStats);
        // despawn trinket in overworld
        Destroy(closestTrinket.gameObject);
    }
    public void UnequipTrinket(int _slot)
    {
        if (equippedTrinkets[_slot].name == "" || equippedTrinkets[_slot].name == null)
            return;

        ////////////////////////// unequip/drop sfx
        unEquipTrinketInstance.start();

        Trinket selectedTrinket = equippedTrinkets[_slot];
        
        // spawn trinket in overworld
        trinketManager.SpawnTrinket(new Vector2(transform.position.x,
            transform.position.y + 0.75f), selectedTrinket);

        // remove trinket stats from player & reset name
        playerStats.RemoveStats(equippedTrinkets[_slot].trinketStats);
        equippedTrinkets[_slot] = new Trinket();
    }
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        trinketManager = TrinketManager.Instance;
        uiManager = UIManager.Instance;
        inventoryUI = InventoryUI.Instance;

        if (trinketTooltip == null)
            trinketTooltip = Instantiate(trinketTooltipPrefab);

        //FMOD
        equipTrinketInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/env/TrinketEquip");
        unEquipTrinketInstance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/env/TrinketUnequip");
    }
    void FixedUpdate()
    {
        closeTrinkets.Clear();
        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, pickupDistance);
        foreach (Collider2D col in closeColliders)
        {
            if (col.CompareTag("Trinket"))
                closeTrinkets.Add(col.transform);
        }
        closestTrinket = GetClosestTrinket();
        TrinketTooltipPosition();
    }
    public Trinket GetTrinket(int _slot)
    {
        if (equippedTrinkets[_slot].name == "" || equippedTrinkets[_slot].name == null)
            return null;

        return equippedTrinkets[_slot];
    }
    void TrinketTooltipPosition()
    {
        if (closestTrinket == null)
        {
            trinketTooltip.gameObject.SetActive(false);
            return;
        }
        trinketTooltip.gameObject.SetActive(true);
        trinketTooltip.position = closestTrinket.transform.position + tooltipOffset;
        
    }
    Transform GetClosestTrinket()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in closeTrinkets)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    void OnSpawnTrinket()
    {
        trinketManager.SpawnTrinket(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    public void Interact()
    {
        if (uiManager.IsPaused() && closestTrinket != null)
        {
            EquipTrinket(closestTrinket.GetComponent<TrinketItem>().Trinket, inventoryUI.selectedSlotInt);
            uiManager.Resume();
            return;
        }
        if (closestTrinket != null)
        {
            uiManager.OpenInventory();
            inventoryUI.PickupTrinket(closestTrinket.GetComponent<TrinketItem>().Trinket);
        }
    }

    void AddNewTrinket(int _slot)
    {
        if (closestTrinket != null)
            EquipTrinket(closestTrinket.GetComponent<TrinketItem>().Trinket, _slot);
        // make sure trinket is available to equip
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
