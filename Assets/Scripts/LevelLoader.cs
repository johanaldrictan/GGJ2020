using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlaneShatterer))]
public class LevelLoader : MonoBehaviour
{
    /*
    [SerializeField]
    private Animator transition;
    [SerializeField]
    */
    [SerializeField]
    private float transitionTime = 2f;
    [SerializeField]
    private PlaneShatterer shatterer;

    private void OnEnable()
    {
        //Only init events if there is an InGameMenu object
        if (GameObject.FindGameObjectWithTag("UI") != null)
        {
            InGameMenu.OnBackToMenu += BackToTitleScreen;
            InGameMenu.OnQuit += QuitGame;
        }
    }
    private void OnDisable()
    {
        if (GameObject.FindGameObjectWithTag("UI") != null)
        {
            InGameMenu.OnBackToMenu -= BackToTitleScreen;
            InGameMenu.OnQuit -= QuitGame;
        }
    }
    private void Awake()
    {
        shatterer = GetComponentInChildren<PlaneShatterer>();
        
    }
    private void Start()
    {
        if (SFXManager.instance != null)
        {
            AudioSource s = SFXManager.instance.GetSFX("Music").source;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                s.clip = SFXManager.instance.GetSFX("Music").clips[0];
                s.loop = true;
                s.Play();
            }
            else
            {
                s.clip = SFXManager.instance.GetSFX("Music").clips[1];
                s.loop = true;
                s.Play();
            }
        }
    }
    public void LoadNextLevel()
    {
        AudioSource s = SFXManager.instance.GetSFX("Music").source;
        if(s.isPlaying)
        {
            s.Stop();
        }
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        //trigger animation
        if (shatterer != null)
        {
            shatterer.RepairScreen();
        }
        else
        {
            Debug.Log("Shatterer nulled");
        }
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    public void QuitGame()
    {
        StartCoroutine(CloseGame());
    }
    IEnumerator CloseGame()
    {
        if (shatterer != null)
            shatterer.RepairScreen();
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
    public void BackToTitleScreen()
    {
        StartCoroutine(LoadLevel(0));
    }
}
