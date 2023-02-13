using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public static SwipeDetector isSwap;
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;
    public bool up, right;
    public bool down, left;

    void Awake()
    {
        isSwap = this;
    }

    void Start()
    {
          
    }

    void Update() {
        up = false;
        down = false;
        left = false;
        right = false;

        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
        //check if finger touching the screen
        if (fingerDown) {
            if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect) {
                fingerDown = false;
                left = true;
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect) {
                fingerDown = false;
                right = true;
            }
            else if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect) {
                fingerDown = false;
                up = true;
            }
            else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect) {
                fingerDown = false;
                down = true;
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended) {
            fingerDown = false;
        }

        //testing PC
        if (fingerDown == false && Input.GetMouseButtonDown(0)) {
            startPos = Input.mousePosition;
            fingerDown = true;
        }
        if (fingerDown) {
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect) {
                fingerDown = false;
                up = true;
            }
            else if (Input.mousePosition.y <= startPos.y - pixelDistToDetect) {
                fingerDown = false;
                down = true;
            }
            else if (Input.mousePosition.x <= startPos.x - pixelDistToDetect) {
                fingerDown = false;
                left = true;
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect) {
                fingerDown = false;
                right = true;
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0)) {
            fingerDown = false;
        }
    }
}
