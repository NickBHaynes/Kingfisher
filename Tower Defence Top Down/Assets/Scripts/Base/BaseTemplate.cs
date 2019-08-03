using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseTemplate
{
    public int baseNumber;
    public string baseName;
    public int baseLevel;
    public Sprite baseImage;
    public GameObject basePrefab;
    public float unlockCost;

    //whether the base needs to be purchased or not
    public bool isUnlocked;
    public bool isSelected;
    //option if the base can be upgraded eg have a turret
    public bool upgradable;
    public string upgradeName;
    public bool isUpgraded;

}
