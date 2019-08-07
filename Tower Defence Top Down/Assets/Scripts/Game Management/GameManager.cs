using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header ("Player Prefab array")]
    // cached array data for the player prefabs
    public PlayerTemplate[] players;
    private PlayerTemplate[] loadingPlayers;

    public int selectedPlayerNum;

    [Header("Base Prefab Array")]
    public BaseTemplate[] bases;
    public int selectedBaseNum;

    [Space]

    // the users currency
    public float playerCoinTotal;

    // selecting a player
    public bool playerSelected;
    public GameObject selectedPlayerPrefab;

    // selecting a base
    public bool baseSelected;
    public GameObject selectedBasePrefab;
    public bool baseIsUpgraded;

    [Header("Level points system")]
    public float[] pointsToNextLevel;
    public float totalPoints;
    public float baseExp = 1000;
    public int currentPlayerLevel = 1;
    [SerializeField] int maxLevel = 5;

    public float highScore;

    // Cached unlock data for saving
    private string playerTemplateFilePath = "playerTemplates";
    private string playerCoinTotalFilePath = "playerCoins";




    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        baseIsUpgraded = false;
        LoadGameSave();
        pointsToNextLevel = new float[maxLevel];
        pointsToNextLevel[1] = baseExp;

        for (int i = 2; i < pointsToNextLevel.Length; i++)
        {
            pointsToNextLevel[i] = Mathf.FloorToInt(pointsToNextLevel[i - 1] * 1.25f);
        }
    }

    public void SavePlayerTemplates()
    {
        ES3.Save<PlayerTemplate[]>(playerTemplateFilePath, players);
        SavePlayerCoinTotal();
    }

    public void SavePlayerCoinTotal()
    {
        ES3.Save<float>(playerCoinTotalFilePath, playerCoinTotal);
    }

    public void LoadGameSave()
    {
        if (ES3.KeyExists(playerTemplateFilePath))
        {

            players = ES3.Load<PlayerTemplate[]>(playerTemplateFilePath);
            Debug.Log("Player Template File Exists");
        }

        if (ES3.KeyExists(playerCoinTotalFilePath))
        {
            playerCoinTotal = ES3.Load<float>(playerCoinTotalFilePath);
            Debug.Log("Player Coins File Exists");
        }
    }


    public void AddToPlayerCoinTotal(float coinsWon)
    {
        playerCoinTotal += coinsWon;
    }

    public void AddToPlayerPointsTotal( float pointsEarnt)
    {
        totalPoints += pointsEarnt;
        if (currentPlayerLevel < maxLevel)
        {
            if (totalPoints > pointsToNextLevel[currentPlayerLevel])
            {
                totalPoints -= pointsToNextLevel[currentPlayerLevel];
                currentPlayerLevel++;
            }
        } else
        {
            totalPoints = 0;
        }
    }


}

