using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private bool InCreditsMenu;
    private bool InVolumeMenu;
    private Animator transitioner;
    private void Awake()
    {
        transitioner = GetComponent<Animator>();
        InCreditsMenu = false;
        InVolumeMenu = false;
    }
    public void OpenCreditsMenu()
    {
        InCreditsMenu = true;
        StartCoroutine(AnimCreditsMenu());
    }
    public void CloseCreditsMenu()
    {
        InCreditsMenu = false;
        StartCoroutine(AnimCreditsMenu());
    }
    public void OpenVolumeMenu()
    {
        InVolumeMenu = true;
        StartCoroutine(AnimVolumeMenu());
    }
    public void CloseVolumeMenu()
    {
        InVolumeMenu = false;
        StartCoroutine(AnimVolumeMenu());
    }
    IEnumerator AnimCreditsMenu()
    {
        transitioner.SetBool("InCreditsMenu", InCreditsMenu);
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator AnimVolumeMenu()
    {
        transitioner.SetBool("InVolumeMenu", InVolumeMenu);
        yield return new WaitForSeconds(.5f);
    }


}
