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
    public void LoadNextLevel()
    {
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
