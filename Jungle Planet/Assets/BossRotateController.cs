using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotateController : MonoBehaviour
{
    private float currAngle = 0f;
    private float targetAngle = 0f;
    private float lerpSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currAngle = Mathf.LerpAngle(currAngle, targetAngle, lerpSpeed*Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f,currAngle,0f);
    }

    public void SetTargetAngle(float newta) {
        targetAngle = Mathf.Clamp(newta,0f,360f);
    }

    public void Reset() {
        currAngle = 0f;
        targetAngle = 0f;
    }
}
