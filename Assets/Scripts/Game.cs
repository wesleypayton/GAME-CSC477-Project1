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

    // Private variables
    private float forceApplied;
    private float chargeMultiplier = 0f;
    private float chargeRate = 0.35f;
    private bool isCharging = false;
    private ParticleSystem chargeParticles;


    public static Game Instance 
        { get; private set; }

    void Awake() {
        chargeParticles = GameObject.FindGameObjectWithTag("BallStart").GetComponent<ParticleSystem>();
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
        else if (input.Default.LaunchBall.WasPressedThisFrame()) {
            if (ball.launched == false) {
                ball.chargeSound.Play();
                isCharging = true;
                StartCoroutine(ChargeLaunchForce());
            }
        }
        else if (input.Default.LaunchBall.WasReleasedThisFrame()) {
            if (ball.launched == false) {
                ball.chargeSound.Stop();
                isCharging = false;
                StopCoroutine(ChargeLaunchForce());
                float forceApplied = 0.4f + chargeMultiplier; // Apply the charge multiplier
                print(forceApplied);
                ball.Launch(forceApplied);
                chargeMultiplier = 0f; // Reset charge multiplier after launch
            }
        }
    }

    private IEnumerator ChargeLaunchForce() {
        while (isCharging) {
            chargeParticles.Emit(30);
            print(chargeMultiplier);
            chargeMultiplier += Time.deltaTime * chargeRate;
            if (chargeMultiplier >= 2.0f) {
                ball.chargeSound.Stop();
                isCharging = false;
                forceApplied = 0.4f + chargeMultiplier; // Apply the charge multiplier
                print(forceApplied);
                ball.Launch(forceApplied);
                chargeMultiplier = 0f; // Reset charge multiplier after launch
            }
            yield return null;
        }
    }

    public void AddScore(int amount) {
        score.AddScore(amount);
    }

    public void ResetScore() {
        score.currentScore = 0;
    }
}
