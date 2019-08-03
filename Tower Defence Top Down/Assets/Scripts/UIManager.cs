using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header ("Menus")]
    public GameObject mainMenuPos;
    public GameObject selectPlayerPanel;
    public GameObject settingMenuPanel;
    public GameObject SelectBaseMenu;

    [Space]

    [Header ("Player Selection Transforms")]
    public Transform selectionPlayerPanelTransform;
    public Transform origionalSelectionHiddenPosition;

    [Header ("Settings Menu Transforms")]
    public Transform settingMenuPanelTransform;
    public Transform origionalSettingsMenuPositions;

    [Header("Base Menu Transforms")]
    public Transform baseMenuPanelTransform;
    public Transform origionalBaseMenuPosition;

    [Space]

    [Header ("Text")]
    public TMP_Text mainHeading;

    //Audio Cache
    private AudioManager theAMan;
    public string menusClickSound = "Small Menu Click";

    // Start is called before the first frame update
    void Start()
    {

        theAMan = FindObjectOfType<AudioManager>();
       // InvokeRepeating("TitleBounce", 0, 0.1f);
      //  TitleBounce();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void TitleBounce()
    {
        Sequence titleScaling = DOTween.Sequence();
        titleScaling.Append(mainHeading.DOOSScale(0.5f, 1f));
        titleScaling.Append(mainHeading.DOOSScale(1f, 1f));
    }

    public void selectPlayerMenuBtn()
    {
        if (selectPlayerPanel == null) return;
        theAMan.PlaySound(menusClickSound);
        selectPlayerPanel.SetActive(true);
        selectionPlayerPanelTransform.DOMove(mainMenuPos.transform.position, 0.5f);
    }

    public void closeSelectPlayerMenu()
    {
        if (selectPlayerPanel == null) return;
        theAMan.PlaySound(menusClickSound);
        selectionPlayerPanelTransform.DOMove(origionalSelectionHiddenPosition.position, 0.5f);
       // selectPlayerPanel.SetActive(false);
    }

    public void settingsMenuBtn()
    {
        if (settingMenuPanel == null) return;
        theAMan.PlaySound(menusClickSound);
        settingMenuPanel.SetActive(true);
        settingMenuPanelTransform.DOMove(mainMenuPos.transform.position, 0.5f);
    }

    public void closeSettingsMenu()
    {
        if (settingMenuPanel == null) return;
        theAMan.PlaySound(menusClickSound);
        settingMenuPanelTransform.DOMove(origionalSettingsMenuPositions.position, 0.5f);
        
    }

    public void BaseMenuBtnPressed()
    {
        if (SelectBaseMenu == null) return;
        theAMan.PlaySound(menusClickSound);
        SelectBaseMenu.SetActive(true);
        baseMenuPanelTransform.DOMove(mainMenuPos.transform.position ,0.5f);
    }

    public void CloseBaseMenu()
    {
        if (SelectBaseMenu == null) return;
        theAMan.PlaySound(menusClickSound);
        baseMenuPanelTransform.DOMove(origionalBaseMenuPosition.position, 0.5f);
    }


}
