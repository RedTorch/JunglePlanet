using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarHaphazardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    private float nextTime = 0f;
    private float spawnIncrement = 0.03f;
    private float spawnSpacing = 6f;
    private float elapsedDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        nextTime += Time.time;
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextTime)
        {
            return;
        }
        else if(elapsedDistance > 60f)
        {
            return;
        }
        elapsedDistance += spawnSpacing;
        Instantiate(spikePrefab, transform.position + (transform.forward * (35f - elapsedDistance)), Quaternion.identity);
        nextTime = Time.time + spawnIncrement;
    }
}
