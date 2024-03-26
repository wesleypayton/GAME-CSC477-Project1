using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject gameoverMenu;
    public AudioSource menuMusic;
    public AudioSource gameMusic;

    void Start() {
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
        gameMusic = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<AudioSource>();
        menuMusic.Play();
        mainMenu.SetActive(true);
        gameoverMenu.SetActive(false);
        Game.Instance.input.Disable();
    }

    public void StartGame() {
        menuMusic.Stop();
        gameMusic.Play();
        mainMenu.SetActive(false);
        Game.Instance.input.Enable();
    }

    public void GameOver() {
        gameMusic.Stop();
        menuMusic.Play();
        gameoverMenu.SetActive(true);
        Game.Instance.input.Disable();
    }

    public void Restart() {
        menuMusic.Stop();
        gameMusic.Play();
        gameoverMenu.SetActive(false);
        Game.Instance.input.Enable();
    }
}
