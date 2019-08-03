using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerMenuChars: MonoBehaviour
{
    public int menuNum;
    public TMP_Text playerType;
    public Image playerImage;
    public TMP_Text speedType;
    public TMP_Text projectileType;
    public GameObject unlockPlanel;
    public TMP_Text unlockCost;
    public bool isUnlocked;



    private void Start()
    {
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        if (GameManager.instance.players[menuNum] != null)
        {
            playerType.text = GameManager.instance.players[menuNum].playerType;
            playerImage.sprite = GameManager.instance.players[menuNum].playerTemplateImage;
            speedType.text = GameManager.instance.players[menuNum].speed;
            projectileType.text = GameManager.instance.players[menuNum].projectile;
            unlockCost.text = GameManager.instance.players[menuNum].unlockCost.ToString();
            isUnlocked = GameManager.instance.players[menuNum].isUnlocked;

            if (isUnlocked)
            {
                unlockPlanel.SetActive(false);
            }
        }
    }
}
