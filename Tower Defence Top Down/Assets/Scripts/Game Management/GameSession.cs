using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;

public class GameSession : MonoBehaviour
{
    [Header("Menu/Canvas References")]
    public GameObject pauseMenu;
    public GameObject losePanel;

    public TMP_Text coinTotalText;
    private float levelTime;
    public TMP_Text levelTimer;

    //  creating the flashing debuff text
    public GameObject theDebuffText;
    public TMP_Text debuffActiveText;
    public float timePauseForFlash;
    private float timeLeftForFlash;

    private string debuffName;
    public bool debuffLabelShowing;

    public GameObject theJoystick;


    [SerializeField] float coinsCollected;
    public float pointsEarntInLevel;

    [Header ("Game Object References")]

    public GameObject basicPlayer;
    public Transform playerSpawnPoint;
    public GameObject basicBase;
    public Transform baseSpawnPoint;

    [Space]

    public GameObject pickUpContainer;
    public GameObject projectileContainer;
    public GameObject blackoutSprite;

    private bool gameOverActive;

    [Header ("Player HealthBar")]
    public Image playerHealthBar;

    // Audio references

    private AudioSource theAudioSource;
    public Slider theMusicSlider;

    [Header("Rand En Movement Boundries")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;




    // Start is called before the first frame update
    void Start()
    {
        // choosing a player if none already there
        if (FindObjectOfType<PlayerMovement>() == null)
        {
            ChoosePlayer();
        }
        if (FindObjectOfType<Base>() == null)
        {
            ChooseBase();
        }

        gameOverActive = false;

        timeLeftForFlash = 0;
        levelTimer.text = levelTime.ToString();
        PickUpCoins(0);
        theAudioSource = GetComponent<AudioSource>();

        theAudioSource.volume = FindObjectOfType<AudioManager>().musicVolume;
        theMusicSlider.value = FindObjectOfType<AudioManager>().musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverActive)
        {
            
            
            int levelTimeRemaining = (int)levelTime;
            levelTimer.text = levelTimeRemaining.ToString();
            levelTime += Time.deltaTime;
            
        }

        if (theDebuffText.activeInHierarchy)
        {
            if (timeLeftForFlash <= 0)
            {
                if (debuffLabelShowing)
                {
                    debuffActiveText.text = debuffName;
                    debuffLabelShowing = false;
                    timeLeftForFlash = timePauseForFlash;
                }
                else
                {
                    debuffActiveText.text = "";
                    debuffLabelShowing = true;
                    timeLeftForFlash = timePauseForFlash;
                }
            }
            timeLeftForFlash -= Time.deltaTime;
        }

       
    }

    public void ShakeCamera(float magnitude, float roughness)
    {

            CameraShaker.Instance.ShakeOnce(magnitude, roughness, .1f, 1f);
        
    }


    public void ChoosePlayer()
    {
        // if for some reason no player has been selected, the basic one is chosen.
        if (!GameManager.instance.playerSelected)
        {
            Instantiate(basicPlayer, playerSpawnPoint.position, Quaternion.identity);
        }

        // instantiating the chosen player into the scene
        else
        {
            Instantiate(GameManager.instance.selectedPlayerPrefab, playerSpawnPoint.position, Quaternion.identity);
        }

    }

    public void ChooseBase()
    {
        if (!GameManager.instance.baseSelected)
        {
            Instantiate(basicBase, baseSpawnPoint.position, Quaternion.identity);
        } else
        {
            Instantiate(GameManager.instance.selectedBasePrefab, baseSpawnPoint.position, Quaternion.identity);
        }
    }

    public void MainMenu()
    {
        // restarting the time for future games.
        Time.timeScale = 1;
        SceneManager.LoadScene("PlayerChoice Menu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        theJoystick.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        //stopping the time so that enemys and projectiles stop spawning and the player cannot be moved.
        
            losePanel.SetActive(true);
            theJoystick.SetActive(false);
            // updating the Game Manager with the new collected coins
            GameManager.instance.AddToPlayerCoinTotal(coinsCollected);
            //Saving the players coins in the game manager save files
            GameManager.instance.SavePlayerCoinTotal();

            GameManager.instance.AddToPlayerPointsTotal(pointsEarntInLevel);
            Time.timeScale = 0;
            



        // SaveSystem.SavePlayerStats(GameManager.instance);
    }

    public void PauseBtnPressed()
    {
        Time.timeScale = 0;
        theJoystick.SetActive(false);
        pauseMenu.SetActive(true);

    }

    public void ContinueBtnPressed()
    {
        pauseMenu.SetActive(false);
        theJoystick.SetActive(true);
        Time.timeScale = 1;
    }

    public void OptionsBtnPressed()
    {
        FindObjectOfType<UIManager>().settingsMenuBtn();
    }


    // called from ontriggerenters on the coin assets.
    public void PickUpCoins(float coinsToCollect)
    {
        coinsCollected += coinsToCollect;
        coinTotalText.text = coinsCollected.ToString();
    }

    public void StartFlashingDebugLable(string newDebuffName)
    {
        debuffName = newDebuffName;

        theDebuffText.SetActive(true);
        //debuffLable = StartCoroutine(FlashingDebuffText());
    }

    public void StopFlashingDebugLable()
    {
       // StopCoroutine(debuffLable);
        theDebuffText.SetActive(false);
    }

    private IEnumerator FlashingDebuffText()
    {
        bool show = true;
        while (true)
        {
            if (show)
            {
                debuffActiveText.text = debuffName;
            }
            else
            {
                debuffActiveText.text = "";
            }
            show = !show;
            yield return 0;
            yield return new WaitForSeconds(timePauseForFlash);
        }
    }

    public void UpdatePlayerHealthBar(float healthLeft, float healthTotal)
    {
        playerHealthBar.fillAmount = healthLeft / healthTotal;
    }

    public void adjustMusicVolume(float sliderValue)
    {
        theAudioSource.volume = sliderValue;
        FindObjectOfType<AudioManager>().musicVolume = sliderValue;
    }
}
