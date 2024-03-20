using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private BossFight1 myBf;
    private GameObject player;
    private Transform myPlayerTransform;
    private DamageReceiver myPlayerDr;
    private Vector3 lastCheckpoint;
    [SerializeField] private GameObject myDeathScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myPlayerTransform = player.transform;
        myPlayerDr = player.GetComponent<DamageReceiver>();
        lastCheckpoint = myPlayerTransform.position;
        showDeathScreen(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetToLastCheckpoint()
    {
        myBf.Reset();
        myPlayerTransform.position = lastCheckpoint;
        myPlayerDr.Reset();
        showDeathScreen(false);
    }

    public void SetCheckpointToCurrent()
    {
        lastCheckpoint = myPlayerTransform.position;
    }

    public void showDeathScreen(bool iss)
    {
        print("setactive has been set: " + iss);
        myDeathScreen.SetActive(iss);
    }
}
