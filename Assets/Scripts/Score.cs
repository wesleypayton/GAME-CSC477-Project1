using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {
    private TextMeshProUGUI txt;
    public int currentScore;
    public HighScore highScore;
    public TextMeshProUGUI gameoverScoreCounter;

    void Start() {
        txt = GetComponent<TextMeshProUGUI>();
        currentScore = 0;
    }

    void Update() {
        txt.text = currentScore.ToString();
        gameoverScoreCounter.text = currentScore.ToString();
    }

    public void AddScore(int amount) {
        currentScore += amount;
        highScore.SubmitScore(currentScore);
    }


}
