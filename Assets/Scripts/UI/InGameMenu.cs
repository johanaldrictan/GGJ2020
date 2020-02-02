using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public delegate void ToggleTransition(bool transitionIn);
    public static event ToggleTransition OnTransition;
    public delegate void BackToTitleScreen();
    public static event BackToTitleScreen OnBackToMenu;
    public delegate void QuitGame();
    public static event QuitGame OnQuit;
    private bool InEscapeMenu { get; set; }
    private bool InVolumeMenu { get; set; }
    private Animator transitioner;

    private void Awake()
    {
        transitioner = GetComponent<Animator>();
        InEscapeMenu = false;
        InVolumeMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscapeMenu();
        }
    }
    public void ToggleEscapeMenu()
    {
        InEscapeMenu = !InEscapeMenu;
        if(InEscapeMenu)
            StartCoroutine(EnableEscapeMenu());
        else
            StartCoroutine(DisableEscapeMenu());
    }
    public void CloseEscapeMenu()
    {
        InEscapeMenu = false;
        StartCoroutine(DisableEscapeMenu());
    }
    IEnumerator EnableEscapeMenu()
    {
        OnTransition(InEscapeMenu);
        yield return new WaitForSeconds(.25f);
        transitioner.SetBool("InGameMenuOn", InEscapeMenu);
        yield return new WaitForSeconds(.25f);
    }
    IEnumerator DisableEscapeMenu()
    {
        if (InVolumeMenu)
        {
            InVolumeMenu = false;
            AnimVolumeMenu();
        }
        transitioner.SetBool("InGameMenuOn", InEscapeMenu);
        yield return new WaitForSeconds(.25f);
        OnTransition(InEscapeMenu);
        yield return new WaitForSeconds(.25f);
    }
    public void TriggerMainMenu()
    {
        StartCoroutine(MoveToMainMenu());
    }
    public void TriggerGameQuit()
    {
        StartCoroutine(CloseGame());
    }
    IEnumerator MoveToMainMenu()
    {
        transitioner.SetBool("InGameMenuOn", false);
        yield return new WaitForSeconds(.25f);
        OnBackToMenu();
    }
    IEnumerator CloseGame()
    {
        transitioner.SetBool("InGameMenuOn", false);
        yield return new WaitForSeconds(.25f);
        OnQuit();
    }
    public void OpenVolumeMenu()
    {
        InVolumeMenu = true;
        StartCoroutine(AnimVolumeMenu());
    }

    IEnumerator AnimVolumeMenu()
    {
        transitioner.SetBool("VolumeMenuOn", InVolumeMenu);
        yield return new WaitForSeconds(1f);
    }
    public void CloseVolumeMenu()
    {
        InVolumeMenu = false;
        StartCoroutine(AnimVolumeMenu());
    }
}
