using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum SIDE {Left, Right, Mid}

public class Player_Controller : MonoBehaviour
{
    float time;
    float timeDelay;

    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;
    public bool up, right;
    public bool down, left;

    private CharacterController m_char;
    float NewXPos = -219.08f;
    public float XValue;

    private float speed = 7.0f;
    private int desiredLane = 1;
    private const float LANE_DISTANCE = 3.0f;
    public SIDE m_side = SIDE.Mid;

    void Start()
    {
        time = 0f;
        timeDelay = 5f;
        m_char = GetComponent<CharacterController>();
        //transform.position = Vector3.zero;
    }

    void checker()
    {
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

    // void moveLane(bool goingRight)
    // {
    //     if (!goingRight) {
    //         desiredLane--;
    //         if (desiredLane == -1) {
    //             desiredLane = 0;
    //         }
    //     } else {
    //         desiredLane++;
    //         if (desiredLane == 3) {
    //             desiredLane = 2;
    //         }
    //     }
    // }

    void inputTouch()
    {
        // if (up == true) {
        //     jump = true;
        // } else {
        //     jump = false;    
        // }
        
        // if (down == true) {
        //     slide = true;
        // } else {
        //     slide = false;
        // }

        if (left == true) {
            if (m_side == SIDE.Mid) {
                NewXPos = -XValue;
                m_side = SIDE.Left;
            } else if (m_side == SIDE.Left) {
                NewXPos = -219.08f;
                m_side = SIDE.Mid;
            }
        }
        else if (right == true) {
            if (m_side == SIDE.Mid) {
                NewXPos = XValue;
                m_side = SIDE.Right;
            } else if (m_side == SIDE.Right) {
                NewXPos = -219.08f;
                m_side = SIDE.Mid;
            }
        }
        m_char.Move((NewXPos - transform.position.x) * Vector3.right);

        // Vector3 targetPosition = transform.position.z *  Vector3.forward;
        // if (desiredLane == 0) {
        //     targetPosition += Vector3.left * LANE_DISTANCE;
        // } else if (desiredLane == 2)
        //     targetPosition += Vector3.right * LANE_DISTANCE;
        
        //Vector3 moveVector = Vector3.zero;
        //moveVector.x = ((targetPosition - transform.position).normalized.x * speed);
        // moveVector.y = -0.1f;
        // moveVector.z = speed;


    }

    void Update()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
            transform.Translate(0, 0, 0.1f);
        checker();
        inputTouch();
        //stayInLane();
    }
}
