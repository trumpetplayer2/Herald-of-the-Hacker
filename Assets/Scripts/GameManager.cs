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
    public GameObject[] levelTwo;
    public GameObject WinMenu;
    public GameObject DeathMenu;
    private bool isPaused = false;
    public AudioSource Music;
    public AudioClip VictoryJingle;
    private bool isFinished = false;
    public GameObject[] enemyTargets;
    public float roundTime = 120;
    public float roundTwoTime = 60f;
    private float curTime = 0;
    public TextMeshProUGUI timer;
    int round = 1;

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
        if(levelTwo.Length > 0)
        {
            foreach(GameObject obj in levelTwo)
            {
                obj.SetActive(false);
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
        curTime += Time.deltaTime;
        if (curTime >= roundTime)
        {
            //If time is up, congrats, you win
            finishLevel();
        }
        if (curTime >= roundTwoTime && round < 2)
        {
            foreach(GameObject obj in levelTwo)
            {
                obj.SetActive(true);
            }
            round += 1;
        }
        //Update text element
        timer.text = "Time Survived: " + Mathf.FloorToInt(curTime);
    }

    public void Death()
    {
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DeathMenu.SetActive(true);
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
        Time.timeScale = 1f;
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
