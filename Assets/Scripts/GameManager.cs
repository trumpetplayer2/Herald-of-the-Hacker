using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] pauseObjects;
    public GameObject[] resumeObjects;
    public GameObject WinMenu;
    private bool isPaused = false;
    public AudioSource Music;
    public AudioClip VictoryJingle;
    private bool isFinished = false;

    private void Start()
    {
        Time.timeScale = 1;
        //Null check
        if (instance == null)
        {
            //Define instance for easy access later
            instance = this;
        } else
        {
            if (instance.isPaused)
            {
                //Game last ended while paused, delete instance as they left the match to go to main menu
                Destroy(instance);
                instance = this;
            }
        }

        isPaused = false;
        //Toggle menu to correct states
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(true);
            }
        }

        WinMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel") && !isFinished)
        {
            togglePause();
        }
        //Dont fire anything after this
        if (isPaused) { return; }
        /*-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
             * Below this line is paused when pause script is ran *
         *-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-*/
    }

    public void finishLevel()
    {
        //Music.Stop();
        //Show win menu
        WinMenu.SetActive(true);
        isFinished = true;
        isPaused = false;
        togglePause();
        //Music.PlayOneShot(VictoryJingle);
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void switchScenes(string scene)
    {
        if (SceneManager.GetSceneByName(scene) == null) { Debug.LogError("Scene " + scene + "did not exist!"); return; }
        SceneManager.LoadScene(scene);
    }

    public void togglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Music.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Music.UnPause();
        }
        isPaused = !isPaused;
        //Update all pause/unpause objects
        if (pauseObjects.Length > 0)
        {
            foreach(GameObject obj in pauseObjects)
            {
                obj.SetActive(isPaused);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(!isPaused);
            }
        }
    }

    public bool getIsPaused()
    {
        return isPaused;
    }
}
