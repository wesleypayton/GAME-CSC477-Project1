using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Flipper : MonoBehaviour {
    private Rigidbody rb;
    public float force;
    public bool isFlipping;
    private AudioSource flipClip;

    void Start() {
        rb = GetComponent<Rigidbody>();
        flipClip = GetComponent<AudioSource>();
    }

    public void Flip() {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        flipClip.Play();
    }
}
