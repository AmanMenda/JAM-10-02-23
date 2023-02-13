using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGuard : MonoBehaviour
{
    public Animator guardAnim;
    public Transform Guard;
    public float curDis;

    void Start()
    {
        
    }

    public void Follow(Vector3 pos, float speed)
    {
        Vector3 position = pos - Vector3.forward * curDis;
        Guard.position = Vector3.Lerp(Guard.position, position, Time.deltaTime * speed / curDis);
    }
}
