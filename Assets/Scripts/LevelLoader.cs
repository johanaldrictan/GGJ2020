using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        //trigger animation
        shatterer.RepairScreen();
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
