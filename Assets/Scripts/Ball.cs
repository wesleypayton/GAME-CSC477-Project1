using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {
    // set in inspector
    public float launchForce;
    public Menu menu;
    public GameObject gameHUD;
    public ParticleSystem sparks_PS;
    public bool launched = false;

    // private fields
    private Rigidbody rb;
    private int lives;
    private const int MAX_LIVES = 4;

    void Start() {
        lives = MAX_LIVES;
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(float forceApplied) {
        if (launched == false) {
            float actualLaunchForce = launchForce * forceApplied;
            rb.AddForce(Vector3.forward * actualLaunchForce, ForceMode.Impulse);
            launched = true;
        }
    }
    public void Restart() {
        transform.position = GameObject.FindGameObjectWithTag("BallStart").transform.position;
        rb.velocity = Vector3.zero;
        lives = MAX_LIVES;
        Game.Instance.ResetScore();
        for (int i = 1; i < MAX_LIVES+1; i++) {
            gameHUD.transform.Find("Ball" + i.ToString()).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BallEnd")) {
            transform.position = GameObject.FindGameObjectWithTag("BallStart").transform.position;
            rb.velocity = Vector3.zero;
            launched = false;
            // Disable corresponding life graphic with each life lost
            if (lives > 0) {
                gameHUD.transform.Find("Ball" + lives.ToString()).gameObject.SetActive(false);
            }
            lives--;
            print($"number of balls left: {lives}");
            if (lives < 0) {
                menu.GameOver();
            }
        } else if (other.CompareTag("BallStart")) {
            launched = false;
        }

    }
    private void OnCollisionEnter(Collision collision) {
        var bumper = collision.gameObject.GetComponent<Bumper>();
        var target = collision.gameObject.GetComponent<Target>();
        if (bumper != null) {
            bumper.Bump();
            Game.Instance.AddScore(100);
        }
        if (target != null) {
            target.Bump();
            Game.Instance.AddScore(1000);
        }
        else {
            if (collision.gameObject.tag.StartsWith("Flipper")) {
                sparks_PS.Play();
                Game.Instance.AddScore(10);
            }
            else if (collision.gameObject.CompareTag("ScoreCircle")) {
                print("here");
                int score = collision.gameObject.GetComponent<ScoreCircle>().isActive ? 100_000 : 5;
                Game.Instance.AddScore(score);
            }
        }
    }
}
