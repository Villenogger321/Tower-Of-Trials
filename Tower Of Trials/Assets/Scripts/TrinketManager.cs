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
    public float floor = 1;
    public TrinketRarity rarity;

    void Start()
    {
        GenerateTrinket();
    }

    void Update()
    {

    }
    void GenerateTrinket()
    {
        Trinket trinket = new Trinket();

        trinket.name = generateName();
        print(trinket.name);
    }

    [ContextMenu("math")]
    void TrinketStatMath()
    {
        Trinket trinket = new Trinket();
        TrinketStatInfo info = trinket.GetRarityInfo(rarity);

        for (int i = 0; i < 10; i++)
        {
            float temp = ((info.floorMultiplier * floor)) + (Random.Range(info.healthRange.x, info.healthRange.y));
            print((int)temp);

        }
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
public class Trinket
{
    public string name;
    public TrinketRarity rarity;


    public TrinketStatInfo GetRarityInfo(TrinketRarity _rarity)
    {
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
    }
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
