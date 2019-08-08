using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerChoiceMenu : MonoBehaviour
{
  
    public GameObject startBtn;
    //
    // public string sceneToLoad;
    public LevelArrays[] scenesToLoad;
    private int levelToLoad;
    private int levelArrayToLoad;

    [Header("Loading Screen Vars")]
    public GameObject loadingPanel;
    public Slider loaidngSlider;


    [Header("Show selected player")]
    public GameObject selectedPlayer;
    public TMP_Text selectedPlayerType;
    public Image selectedPlayerImage;

    [Header("Show selected base")]
    public GameObject selectedBase;
    public TMP_Text selectedBaseName;
    public Image selectedBaseImage;


    // [Header("Failed to unlock/Success panel")]
    // public GameObject failedToUnlockPanel;
    

    [Header("New player selection panel references")]
    public TMP_Text playerName;
    public Image currentPlayerImage;
    public TMP_Text currentPlayerType;
    public TMP_Text currentPlayerSpeedType;
    public TMP_Text currentPlayerProjectile;
    public TMP_Text unlock_SelectBtn;
    public TMP_Text unlockCost;
    public TMP_Text currentCoinsOwned;

    [Header("New unlock selected player panel")]
    public GameObject newUnlockPanel;
    public TMP_Text canOrCantUnlockBtn;
    public TMP_Text canOrCantUnlockMessage;

    public GameObject costGrouping;

    [Header("Base selection panel references")]
    public TMP_Text baseName;
    public Image baseImage;
    public TMP_Text baseLevel;
    public TMP_Text unlock_SelectBaseBtn;
    public TMP_Text baseUnlockCost;
    public TMP_Text currentCoinsOwned_Base;

    [Header("Base Upgrade Panel References")]
    public GameObject baseUpgradePanel;
    public Image upgradeImage;
    public TMP_Text upgradeCost;
    public GameObject UnlockUpgradeBaseGroup;
    public GameObject UpgradeUnlockedMessage;

    [Space]

    public GameObject unlockUpgradeBasePanel;
    public GameObject confirmBaseUpgradeBtn;
    public TMP_Text upgradeBaseMessage;


    [Header("Base unlock selected panel")]
    public GameObject baseCostGrouping;
    public GameObject baseUnlockPanel;
    public TMP_Text canOrCantUnlock_BaseBtn;
    public TMP_Text canOrCantUnlockMessage_Base;

    private int currentPlayerShownNum;
    private int currentBaseShownNum;

    private int storedBaseNum;
    private int storedPlayerNum;

    // used to tell the game manager which player is selected when unlocking
    public int selectedPlayerNum;

    // chached references
    GameManager theGm;
    UIManager theUIMan;
    AudioManager theAMan;
    public string buttonClickSound = "Small Menu Click";


    // Start is called before the first frame update
    void Start()
    {
        theGm = GameManager.instance;
        theUIMan = FindObjectOfType<UIManager>();
        theAMan = FindObjectOfType<AudioManager>();

       // UpdatePlayerItems();
        UpdateMenu();
        UpdatePlayerShown();
        UpdateSelectedPlayerShow();
        UpdateSelectedBaseShown();
        UpdateBaseShown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateMenu()
    {

        //changing the text on the start button to reveal if the player needs to choose a char.
        if (theGm.playerSelected && theGm.baseSelected)
        {
            startBtn.SetActive(true);
            currentBaseShownNum = theGm.selectedBaseNum;
            currentPlayerShownNum = theGm.selectedPlayerNum;
        }
        else
        {
            startBtn.SetActive(false);
        }
    }


   

    public void StartGameBtnPressed()
    {
        theAMan.PlaySound(buttonClickSound);
        
            // SceneManager.LoadScene(sceneToLoad);
            //

            loadingPanel.SetActive(true);
            levelArrayToLoad = theGm.currentPlayerLevel - 1;

            levelToLoad = Random.Range(0, scenesToLoad[levelArrayToLoad].levels.Length);
            Debug.Log("There are " + scenesToLoad[levelArrayToLoad].levels.Length + " Levls");

            StartCoroutine(LoadLevelsAsynchronously());
            //SceneManager.LoadScene(scenesToLoad[levelArrayToLoad].levels[levelToLoad]);
        
    }

    // coroutine to add a loading bar whilst the level loads insted of just freezing the screen
    // if confused watch brackeys tutorial on loading bars again
    IEnumerator LoadLevelsAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenesToLoad[levelArrayToLoad].levels[levelToLoad]);

        // checks to see whether the level has been loaded yet, if it hasnt it will run the code
        // will run once a frame until operation.isdone has finished
        while (!operation.isDone)
        {
            /// the loading process only takes up 0.9f of the loading variable, so we
            /// are converting it into a percentage of 1 to use in outr loading bar
            
            float levelLoadProgress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);
            loaidngSlider.value = operation.progress;
            yield return null;
        }

    }

   

    /// new style menu testing code, if it works delete old style and keep the new
    /// some code is reused for instance creating the arrays of data from the player
    /// data that is stored on the game manager.
    ///

    // The following funcitons are for selecting a player


    // this update whic player is shown in te selection player menu


    public void UpdatePlayerShown()
    {
        
        
        playerName.text = theGm.players[currentPlayerShownNum].playerName;
        currentPlayerImage.sprite = theGm.players[currentPlayerShownNum].playerTemplateImage;
        currentPlayerType.text = theGm.players[currentPlayerShownNum].playerType;
        currentPlayerSpeedType.text = theGm.players[currentPlayerShownNum].speed;
        currentPlayerProjectile.text = theGm.players[currentPlayerShownNum].projectile;
        currentCoinsOwned.text = theGm.playerCoinTotal.ToString();
        unlockCost.text = theGm.players[currentPlayerShownNum].unlockCost.ToString();

        if (theGm.players[currentPlayerShownNum].isUnlocked)
        {
            
            unlock_SelectBtn.text = "Select";
            currentPlayerImage.color = Color.white;
            costGrouping.SetActive(false);

        } else
        {
            unlock_SelectBtn.text = "Unlock";
            currentPlayerImage.color = Color.black;
            costGrouping.SetActive(true);
        }
    }


    public void nextBtnPressed()
    {
        if (currentPlayerShownNum < theGm.players.Length - 1)
        {
            currentPlayerShownNum++;
            UpdatePlayerShown();
        }
        theAMan.PlaySound(buttonClickSound);
    }


    public void lastPlayerBtnPressed()
    {
        if (currentPlayerShownNum > 0)
        {
            currentPlayerShownNum--;
            UpdatePlayerShown();
        }
        theAMan.PlaySound(buttonClickSound);
    }
    


    // if the player is locked when selected, the following code is run to unlock tat player
    public void unlock_SelectBtnPressed()
    {
        /// select that player, and close the menu if unlocked
        /// open unlock panel if locked
        theAMan.PlaySound(buttonClickSound);
        for (int i = 0; i < theGm.players.Length; i++)
        {
            if (i == currentPlayerShownNum && theGm.players[i].isUnlocked)
            {
                theGm.players[i].isSelected = true;
                theGm.playerSelected = true;
                theGm.selectedPlayerPrefab = theGm.players[i].playerPrefab;
               

                theGm.selectedPlayerNum = currentPlayerShownNum;
                UpdateMenu();

                // displaying the chosen player on the choose player screen
                if (theGm.playerSelected)
                {
                    selectedPlayer.SetActive(true);
                    selectedPlayerType.text = theGm.players[i].playerType;
                    selectedPlayerImage.sprite = theGm.players[i].playerTemplateImage;

                }

                // closing the menu
                theUIMan.closeSelectPlayerMenu();
            }
            else
            if (i == currentPlayerShownNum && !theGm.players[i].isUnlocked)
            {
                newUnlockPanel.SetActive(true);

                if (theGm.playerCoinTotal >= theGm.players[currentPlayerShownNum].unlockCost)
                {
                    canOrCantUnlockBtn.text = "Yes";
                    canOrCantUnlockMessage.text = "Are You Sure?";
                } else
                {
                    canOrCantUnlockBtn.text = "Close";
                    canOrCantUnlockMessage.text = "Sorry, you need more coins";
                }
            }
            else
            {
                theGm.players[i].isSelected = false;
                UpdateMenu();
            }
        }
    }

    public void unlockCharBtnPressed()
    {
        theAMan.PlaySound(buttonClickSound);
        if (canOrCantUnlockBtn.text == "Close")
        {
            newUnlockPanel.SetActive(false);
            Debug.Log("No");
        } else if (canOrCantUnlockBtn.text == "Yes")
        {
            Debug.Log("Yes");
            theGm.players[currentPlayerShownNum].isUnlocked = true;
            theGm.playerCoinTotal -= theGm.players[currentPlayerShownNum].unlockCost;
            UpdatePlayerShown();
            theGm.SavePlayerTemplates();
            newUnlockPanel.SetActive(false);    
        }
    }

    public void UpdateSelectedPlayerShow()
    {
        if (theGm.playerSelected)
        {
            selectedPlayer.SetActive(true);
            selectedPlayerType.text = theGm.players[currentPlayerShownNum].playerType;
            selectedPlayerImage.sprite = theGm.players[currentPlayerShownNum].playerTemplateImage;

        }
    }

    //
    //
    // The following funtions are for selecting a base.
    //
    //

    public void UpdateBaseShown()
    {

        
        baseName.text = theGm.bases[currentBaseShownNum].baseName;
        baseImage.sprite = theGm.bases[currentBaseShownNum].baseImage;
        baseLevel.text = theGm.bases[currentBaseShownNum].baseLevel.ToString();
        baseUnlockCost.text = theGm.bases[currentBaseShownNum].unlockCost.ToString();
        currentCoinsOwned_Base.text = theGm.playerCoinTotal.ToString();

        if (theGm.bases[currentBaseShownNum].upgradable
            && theGm.bases[currentBaseShownNum].isUnlocked)
        {
            baseUpgradePanel.SetActive(true);
            baseImage.sprite = theGm.bases[currentBaseShownNum].baseImage;
            upgradeCost.text = theGm.bases[currentBaseShownNum].upgradeCost.ToString();
            if (theGm.bases[currentBaseShownNum].isUpgraded)
            {
                UnlockUpgradeBaseGroup.SetActive(false);
                UpgradeUnlockedMessage.SetActive(true);
            } else
            {
                UnlockUpgradeBaseGroup.SetActive(true);
                UpgradeUnlockedMessage.SetActive(false);
            }
        } else
        {
            baseUpgradePanel.SetActive(false);
        }

        if (theGm.bases[currentBaseShownNum].isUnlocked)
        {
            unlock_SelectBaseBtn.text = "Select";
            baseImage.color = Color.white;
            baseCostGrouping.SetActive(false);
        } else
        {
            unlock_SelectBaseBtn.text = "Unlock";
            baseImage.color = Color.black;
            baseCostGrouping.SetActive(true);
        }
    }

    public void UpdateSelectedBaseShown()
    {
        if (theGm.baseSelected)
        {
            selectedBase.SetActive(true);
            selectedBaseName.text = theGm.bases[currentBaseShownNum].baseName;
            selectedBaseImage.sprite = theGm.bases[currentBaseShownNum].baseImage;
        }
        
    }

    public void NextBaseBtn()
    {
        if (!baseUnlockPanel.activeInHierarchy && !unlockUpgradeBasePanel.activeInHierarchy)
        {
            if (currentBaseShownNum < theGm.bases.Length - 1)
            {
                currentBaseShownNum++;
                UpdateBaseShown();
            }
            theAMan.PlaySound(buttonClickSound);
        }
        
    }

    public void LastBaseBtn()
    {
        if (!baseUnlockPanel.activeInHierarchy && !unlockUpgradeBasePanel.activeInHierarchy)
        {
            if (currentBaseShownNum > 0)
            {
                currentBaseShownNum--;
                UpdateBaseShown();
            }
            theAMan.PlaySound(buttonClickSound);

        }
        
    }

    public void unlock_SelectBaseBtnPressed()
    {
        theAMan.PlaySound(buttonClickSound);

        for (int i = 0; i < theGm.bases.Length; i++)
        {
            if (i == currentBaseShownNum && theGm.bases[i].isUnlocked)
            {
                theGm.baseSelected = true;
                theGm.bases[i].isSelected = true;
                theGm.selectedBasePrefab = theGm.bases[i].basePrefab;

                theGm.selectedBaseNum = currentBaseShownNum;
                UpdateMenu();

                // displaying the chosen base on the choose player screen

                UpdateSelectedBaseShown();
                if (theGm.bases[i].isUpgraded)
                {
                    theGm.baseIsUpgraded = true;
                } else
                {
                    theGm.baseIsUpgraded = false;
                }

                // closing the menu
                theUIMan.CloseBaseMenu();

            }
            else
            if (i == currentBaseShownNum && !theGm.bases[i].isUnlocked)
            {
                baseUnlockPanel.SetActive(true);

                if (theGm.playerCoinTotal >= theGm.bases[currentPlayerShownNum].unlockCost)
                {
                    canOrCantUnlock_BaseBtn.text = "Yes";
                    canOrCantUnlockMessage_Base.text = "Are You Sure?";
                }
                else
                {
                    canOrCantUnlock_BaseBtn.text = "Close";
                    canOrCantUnlockMessage_Base.text = "Sorry, you need more coins";
                }
            }
            else
            {
                theGm.bases[i].isSelected = false;
                UpdateMenu();
            }
        }
    }

    public void UnlockBaseBtnPressed()
    {
        theAMan.PlaySound(buttonClickSound);
        if (canOrCantUnlock_BaseBtn.text == "Close")
        {
            baseUnlockPanel.SetActive(false);
            Debug.Log("No");
        }
        else if (canOrCantUnlock_BaseBtn.text == "Yes")
        {
            Debug.Log("Yes");
            theGm.bases[currentBaseShownNum].isUnlocked = true;
            theGm.playerCoinTotal -= theGm.bases[currentBaseShownNum].unlockCost;
            UpdateBaseShown();
            theGm.SaveBaseTemplates();
            // Add save function for bases here
            baseUnlockPanel.SetActive(false);
        }
    }

    public void UpgradeBaseBtnPressed()
    {
        unlockUpgradeBasePanel.SetActive(true);
        if (theGm.playerCoinTotal >= theGm.bases[currentBaseShownNum].upgradeCost)
        {
            upgradeBaseMessage.text = "Are you sure?";
            confirmBaseUpgradeBtn.SetActive(true);
        } else
        {
            upgradeBaseMessage.text = "Sorry you do not have the coins";
            confirmBaseUpgradeBtn.SetActive(false);
        }
    }

    public void CancelUpgradeBtnPressed()
    {
        unlockUpgradeBasePanel.SetActive(false);
    }

    public void ConfirmUpgradeBaseBtn()
    {
        theGm.bases[currentBaseShownNum].isUpgraded = true;
        theGm.playerCoinTotal -= theGm.bases[currentBaseShownNum].upgradeCost;
        UpdateBaseShown();
        theGm.SaveBaseTemplates();
        unlockUpgradeBasePanel.SetActive(false);
    }


}

