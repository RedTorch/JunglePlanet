using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRise : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float DistanceToEnable = 26f;
    [Tooltip("Play speed of animation. Default assumes animation length of 1 second. \nReduce this number for a slower playback/ longer animation.")]
    private float speed = 0.6f;
    private float currTime = 1f;
    private GameObject target;

    [SerializeField] private string animationName = "BridgeRise";
    // [SerializeField] private
    // Start is called before the first frame update
    void Start()
    {
        if(anim == null) {
            anim = gameObject.GetComponent<Animator>();
        }
        // anim.speed = 0f;
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateByDistance();
        anim.Play (animationName, 0, currTime);
    }

    void UpdateByDistance() {
        if(Vector3.Distance(target.transform.position,transform.position) < DistanceToEnable) {
            currTime = Mathf.Clamp(currTime+(Time.deltaTime*speed),0f,0.9999f);
        }
        else {
            currTime = Mathf.Clamp(currTime-(Time.deltaTime*speed),0f,0.9999f);
        }
    }
}
