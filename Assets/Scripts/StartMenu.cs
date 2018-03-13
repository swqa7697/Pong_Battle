using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu: MonoBehaviour
{
    public AudioClip[] BGM;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        int i = (int)Mathf.Floor(Random.Range(0, 4.99f));
        source.clip = BGM[i];
        source.Play();
    }

    public void StartGame()
    {
        Player1.isAI = false;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void StartWithAI()
    {
        Player1.isAI = true;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
