using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour {
    [HideInInspector] public PinballInput input;
    public Flipper flipperLeft;
    public Flipper flipperRight;
    public Ball ball;
    public Score score;


    public static Game Instance 
        { get; private set; }

    void Awake() {
        input = new PinballInput();
        input.Enable();
        Instance = this;
    }

    void Update() {
        if (input.Default.FlipperLeft.WasPressedThisFrame()) {
            flipperLeft.Flip();
        }
        else if (input.Default.FlipperRight.WasPressedThisFrame()) {
            flipperRight.Flip();
        }
        else if (input.Default.LaunchBall.WasReleasedThisFrame()) {
            ball.Launch();
        }
    }

    public void AddScore(int amount) {
        score.AddScore(amount);
    }

    public void ResetScore() {
        score.currentScore = 0;
    }
}
