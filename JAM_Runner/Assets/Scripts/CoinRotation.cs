using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public bool touch;
    public AudioSource myAudio;
    public AudioClip coinCollection;
    public float rotateSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        //gameObject.tag = "Coin";
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 0f);
        if (touch == true) {
            transform.Rotate(0, 0, 0);
            transform.Translate(0, 0.04f, 0);
        } else {
            //transform.Rotate(0, 0.2f, 0);
            transform.Rotate(0, 5, 0);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // /Debug.Log("hit");
            touch = true;
            myAudio.PlayOneShot(coinCollection, 1);
        }
    }
}
