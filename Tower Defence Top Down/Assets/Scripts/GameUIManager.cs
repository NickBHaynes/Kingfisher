using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenuPos;
    public GameObject settingMenuPanel;

    [Space]

    [Header("Settings Menu Transforms")]
    public Transform settingMenuPanelTransform;
    public Transform origionalSettingsMenuPositions;

    //Audio Cache
    private AudioManager theAMan;

    // Start is called before the first frame update
    void Start()
    {
        theAMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void settingsMenuBtn()
    {
        if (settingMenuPanel == null) return;
        theAMan.PlaySound("Small Menu Click");
        settingMenuPanel.SetActive(true);
        settingMenuPanelTransform.DOMoveX(mainMenuPos.transform.position.x, 0.5f);
    }

    public void closeSettingsMenu()
    {
        if (settingMenuPanel == null) return;
        theAMan.PlaySound("Small Menu Click");
        settingMenuPanelTransform.DOMoveX(origionalSettingsMenuPositions.position.x, 0.5f);

    }
}
