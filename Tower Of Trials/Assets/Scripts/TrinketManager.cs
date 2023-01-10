using FMOD;
using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrinketManager : MonoBehaviour
{
    [SerializeField] TextAsset firstNameList, middleNameList, lastNameList;
    [SerializeField] string[] firstName, middleName, lastName;
    [SerializeField] TrinketInventory inventory;
    [SerializeField] GameObject trinketGO;
    [SerializeField] Transform trinketParent;

    public Sprite[] CommonInventoryTrinketSprite, CommonItemTrinketSprite;
    public Sprite[] RareInventoryTrinketSprite, RareItemTrinketSprite;
    public Sprite[] EpicInventoryTrinketSprite, EpicItemTrinketSprite;

    public static TrinketManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void SpawnTrinket(Vector3 _position, Trinket _trinket = null)
    {
        GameObject trinket = Instantiate(trinketGO, _position, Quaternion.identity, trinketParent);
        TrinketItem trinketItem = trinket.GetComponent<TrinketItem>();

        if (_trinket == null)
            trinketItem.Trinket = GenerateTrinket();
        else
            trinketItem.Trinket = _trinket;

        trinket.GetComponent<SpriteRenderer>().sprite = trinketItem.Trinket.sprite;
        trinket.name = trinketItem.Trinket.name;

        if (trinket.GetComponent<Rigidbody2D>() is Rigidbody2D _rb)
            _rb.AddForce(new Vector2(Random.Range(-60, 60), 100));
    }   
    public Trinket GenerateTrinket()
    {
        Trinket trinket = new Trinket();
        trinket.name = generateName();
        trinket.rarity = TrinketRarityGenerator();
        trinket.sprite = TrinketSpriteGenerator(trinket.rarity);
        trinket.trinketStats.Add(TrinketStatMath(trinket));
        return trinket;
    }
    TrinketStat TrinketStatMath(Trinket _trinket)
    {
        TrinketStat trinketStat = new TrinketStat();
        TrinketStatInfo info = _trinket.GetRarityInfo(_trinket.rarity);
        // first choose stat type(s)
        trinketStat.type = (TrinketStatsType)Random.Range(0, 5);


        // calculate statmodifier depending on chosen stat
        Vector2 newModifierRange = ModifierRange(trinketStat.type, info);
        
        trinketStat.statModifier = info.floorMultiplier * LevelManager.Instance.GetFloor()
                + Random.Range(newModifierRange.x, newModifierRange.y);

        return trinketStat;
    }
    Vector2 ModifierRange(TrinketStatsType _type, TrinketStatInfo _info)
    {
        if (_type == TrinketStatsType.health)
            return _info.healthRange;
        if (_type == TrinketStatsType.damage)
            return _info.damageRange;
        if (_type == TrinketStatsType.attackSpeed)
            return _info.attackSpeedRange;
        if (_type == TrinketStatsType.movementSpeed)
            return _info.movementSpeedRange;
        else
            return _info.projectileSpeedRange;
    }
    Sprite TrinketSpriteGenerator(TrinketRarity _rarity)
    {
        if (_rarity == TrinketRarity.common)
            return CommonItemTrinketSprite[
                Random.Range(0, CommonItemTrinketSprite.Length)];

        if (_rarity == TrinketRarity.rare)
            return RareItemTrinketSprite[
                Random.Range(0, RareItemTrinketSprite.Length)];

            return EpicItemTrinketSprite[
                Random.Range(0, EpicItemTrinketSprite.Length)];
    }
    TrinketRarity TrinketRarityGenerator()
    {
        float rand = Random.Range(1, 101);
        if (rand <= 15)     // epic trinket
            return TrinketRarity.epic;
        if (rand <= 40)     // rare trinket
            return TrinketRarity.rare;
        else                // common trinket
            return TrinketRarity.common;
    }
    String generateName()
    {
        string name = firstName[Random.Range(0, firstName.Length - 1)];
        name = name + " " + middleName[Random.Range(0, middleName.Length - 1)];
        name = name + " " + lastName[Random.Range(0, lastName.Length - 1)];
        return name;
    }
    [ContextMenu("Update namelist")]
    void UpdateNameList()
    {
        firstName = firstNameList.text.Split('\n');
        middleName = middleNameList.text.Split('\n');
        lastName = lastNameList.text.Split('\n');
    }
}
[Serializable]
public class Trinket
{
    public string name;
    public TrinketRarity rarity;
    public Sprite sprite;
    public List<TrinketStat> trinketStats = new List<TrinketStat>();


    public TrinketStatInfo GetRarityInfo(TrinketRarity _rarity)
    {
#pragma warning disable CS8524
        return _rarity switch
        {
            TrinketRarity.common => new TrinketStatInfo(
                         healthRange : new Vector2(1, 5),
                         damageRange : new Vector2(5, 10),
                    attackSpeedRange : new Vector2(3, 9),
                  movementSpeedRange : new Vector2(2, 5),
                projectileSpeedRange : new Vector2(4, 12),
                     floorMultiplier : 0.5f),

            TrinketRarity.rare => new TrinketStatInfo(
                         healthRange : new Vector2(5, 10),
                         damageRange : new Vector2(10, 15),
                    attackSpeedRange : new Vector2(7, 13),
                  movementSpeedRange : new Vector2(5, 8),
                projectileSpeedRange : new Vector2(12, 20),
                     floorMultiplier : 0.6f),

            TrinketRarity.epic => new TrinketStatInfo(
                         healthRange : new Vector2(10, 15),
                         damageRange : new Vector2(15, 20),
                    attackSpeedRange : new Vector2(13, 19),
                  movementSpeedRange : new Vector2(8, 11),
                projectileSpeedRange : new Vector2(20, 28),
                     floorMultiplier : 0.7f),


            TrinketRarity.legendary => new TrinketStatInfo(
                         healthRange : new Vector2(15, 25),
                         damageRange : new Vector2(20, 30),
                    attackSpeedRange : new Vector2(19, 31),
                  movementSpeedRange : new Vector2(11, 14),
                projectileSpeedRange : new Vector2(28, 36),
                     floorMultiplier : 0.8f)
        };
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
    }
}

[Serializable]
public class TrinketStat
{
    public TrinketStatsType type;
    public float statModifier;
}
public struct TrinketStatInfo
{
    public Vector2 healthRange;
    public Vector2 damageRange;
    public Vector2 attackSpeedRange;
    public Vector2 movementSpeedRange;
    public Vector2 projectileSpeedRange;
    public float floorMultiplier;

    public TrinketStatInfo(Vector2 healthRange, Vector2 damageRange, Vector2 attackSpeedRange, Vector2 movementSpeedRange, Vector2 projectileSpeedRange, float floorMultiplier)
    {
        this.healthRange = healthRange;
        this.damageRange = damageRange;
        this.attackSpeedRange = attackSpeedRange;
        this.movementSpeedRange = movementSpeedRange;
        this.projectileSpeedRange = projectileSpeedRange;
        this.floorMultiplier = floorMultiplier;
    }
}
public enum TrinketRarity
{
    common,
    rare,
    epic,
    legendary
}
public enum TrinketStatsType
{
    health,
    damage,
    attackSpeed,
    movementSpeed,
    projectileSpeed
}

[CreateAssetMenu(menuName = "Tower Of Trials/TrinketUnique")]
public class TrinketUniqueInfo : ScriptableObject
{
    
}
