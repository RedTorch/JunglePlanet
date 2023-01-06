using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    [SerializeField] private Vector3 DegPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(DegPerSecond.x * Time.deltaTime, DegPerSecond.y * Time.deltaTime, DegPerSecond.z * Time.deltaTime);
    }
}
