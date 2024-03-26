using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int t;
    private AudioSource targetClip;
    private Vector3 startPos;
    private bool soundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        targetClip = GetComponent<AudioSource>();
        soundPlayed = false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t++;
        if (t == 1000) {
            transform.position += new Vector3(0f, 1f, 0f);
            if (soundPlayed == false) {
                targetClip.Play();
                soundPlayed = true;
            }
        }
        if (t == 2000) {
            transform.position = startPos;
            soundPlayed = false;
            t = 0;
        }
    }

    public void Bump() {
        t = 0;
        transform.position = startPos;
        soundPlayed = false;
        targetClip.Play();
    }
}
