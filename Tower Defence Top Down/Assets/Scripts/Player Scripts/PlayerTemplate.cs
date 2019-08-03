using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class PlayerTemplate
{
    public string playerName;
    public GameObject playerPrefab;
    public  int number;
    public string playerType;
    public string speed;
    public string projectile;
    public Sprite playerTemplateImage;
    public float unlockCost;
    public bool isUnlocked;
    public bool isSelected;
}
