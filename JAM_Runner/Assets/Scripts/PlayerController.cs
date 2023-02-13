using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class PlayerController : MonoBehaviour
{
    public static PlayerController player_control;
    public bool jump = false;
    public bool slide = false;
    public bool go_left = false;
    public bool go_right = false;
    public bool fall = false;

    public GameObject trigger;
    public Animator anim;
    public Animator hero_anim;

    public float scoreo = 0;
    public Text multipliyer;

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1;
    public float laneDistance = 1;
    public CapsuleCollider myCollider;
    public AudioSource crashThud;

    public bool death;
    public float lastScore;
    public Text scoreText;
    public float scorer;
    public Text bestScoreText;
    public GameObject text;

    public GameObject resurect;
    public int test;
    public Transform respawnPoint;
    public bool resurected;

    float time;
    float timeDelay;

    public FollowGuard guard;
    private float curDistance;
    
    //public GameObject player;

    void Awake()
    {
        player_control = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timeDelay = 5f;
        anim = GetComponent<Animator>();
        hero_anim = GameObject.Find("Jess@Running").GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
        lastScore = PlayerPrefs.GetFloat("MyScore"); 
    }

    void incrementScore()
    {
        scoreText.text = scoreo.ToString();

        if (score.scorer.scoring >= 0 && score.scorer.scoring <= 800 && death != true && PauseMenu.pauser.click == false) {
            transform.Translate(0, 0, 0.2f);
            guard.Follow(transform.position, 0.2f);
            multipliyer.text = "x1";
        }
        else if (score.scorer.scoring >= 801 && score.scorer.scoring <= 1500 && death != true && PauseMenu.pauser.click == false) {
            transform.Translate(0, 0, 0.35f);
            guard.Follow(transform.position, 0.35f);
            multipliyer.text = "x2";
        }
        else if (score.scorer.scoring >= 1501 && death != true && PauseMenu.pauser.click == false) {
            transform.Translate(0, 0, 0.45f);
            guard.Follow(transform.position, 0.45f);
            multipliyer.text = "x4";
        }
    }

    void inputTouch()
    {
        if (Swipe_detector.swiper.up == true) {
            jump = true;
        } else {
            jump = false;
        }
        
        if (Swipe_detector.swiper.down == true) {
            slide = true;
        } else {
            slide = false;
        }

        if (Swipe_detector.swiper.left == true) {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        if (Swipe_detector.swiper.right == true) {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
    }

    void stayInLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0) {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2) {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 15 * Time.deltaTime);
    }

    public void Ressurection()
    {
        GameObject.Find("Player_2").GetComponent<Animator>().Play("Running");
        GameObject.Find("Player_2").GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
        Physics.SyncTransforms();
        death = false;
        DialogueBox.dialogue.dead = 0;
    }

    void persoMotion()
    {
        if (death == true) {
            fall = true;
            anim.Play("Falling Down");
            guard.curDis = 0f;
            GameObject.Find("Jess@Running").GetComponent<Transform>().position = transform.position;

            anim.SetBool("Fall", fall);
            hero_anim.SetBool("Intercepting", fall);
        }
        if (jump == true) {
            //anim.SetBool("isJump", jump);
            transform.Translate(0, 1.7f, 1.2f);
            myCollider.radius = 0;
            myCollider.center = new Vector3(0, 1.35f, 0);
            Invoke ("reset_value2", 1.5f);
            
        } else if (jump == false) {
            //anim.SetBool("isJump", jump);
        }

        if (slide == true) {
            //anim.SetBool("isSlide", slide);
            transform.Translate(0, 0, 0.1f);
            myCollider.height = 0.5f;
            myCollider.radius = 0.3f;
            myCollider.center = new Vector3(0, 0.38f, 0);
            Invoke ("reset_value", 1.5F);
            
        } else if (slide == false) {
            //anim.SetBool("isSlide", slide);
        }  
    }

    void Update()
    {
        //curDistance = Mathf.MoveTowards(curDistance, 5f, Time.deltaTime * 0.1f);
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay) {
            //guard.curDis = curDistance;
            incrementScore();   
            inputTouch();
            stayInLane();
            persoMotion();
            trigger = GameObject.FindGameObjectWithTag("Obstacle");
        }
        Debug.Log("isdead");
        Debug.Log(death);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject, 0.5f);
            scoreo += 5f;
        }

        if (other.gameObject.CompareTag("DeathPoint")) {
           crashThud.Play();
           death = true;
            if (scoreo > lastScore) {
                PlayerPrefs.SetFloat("MyScore", scoreo);
            }
        }

    }

    void reset_value() {
        myCollider.height = 1.8f;
        myCollider.center = new Vector3(0, 0.93f, 0);
        myCollider.radius = 0.5f;
        Debug.Log("changed");
    }

    void reset_value2() {
        myCollider.radius = 0.5f;
        myCollider.center = new Vector3(0, 0.93f, 0);
        Debug.Log("changed");
    }
}
