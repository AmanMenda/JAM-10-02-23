using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe_detector : MonoBehaviour
{
    //public CharacterController player;
    public static Swipe_detector swiper;
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;
    public bool up, right;
    public bool down, left;
    //public GameObject[] image;
    //int index;

    void Awake()
    {
        swiper = this;
    }

    void Start() {
        //index = 0;
        //player = gameObject.AddComponent<Player>();        
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
                //Debug.Log("SwipeLeft");
                //player.Move(Vector3.left);
                left = true;
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeRight");
                //player.Move(Vector3.right);
                right = true;
            }
            else if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeUp");
                //player.Move(Vector3.up);
                up = true;
            }
            else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeDown");
                //player.Move(Vector3.down);
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
                //Debug.Log("SwipeUp");
                //player.Move(Vector3.up);
                up = true;
            }
            else if (Input.mousePosition.y <= startPos.y - pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeDown");
                //player.Move(Vector3.down);
                down = true;
            }
            else if (Input.mousePosition.x <= startPos.x - pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeLeft");
                //player.Move(Vector3.left);
                left = true;
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect) {
                fingerDown = false;
                //Debug.Log("SwipeRight");
                //player.Move(Vector3.right);
                right = true;
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0)) {
            fingerDown = false;
        }

    }
}
