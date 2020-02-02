using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public void PlayUIHover()
    {
        if(!SFXManager.instance.IsPlaying("UI_Hover"))
            SFXManager.instance.PlayRandomWithMod("UI_Hover");
    }
    public void PlayUIClick()
    {
        if (!SFXManager.instance.IsPlaying("UI_Click"))
            SFXManager.instance.PlayRandomWithMod("UI_Click");
    }
}
