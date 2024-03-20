using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private BossFight1 myBf;
    private GameObject player;
    private Transform myPlayerTransform;
    private DoomCC myPlayerDcc;
    private Vector3 lastCheckpoint;
    [SerializeField] private GameObject myDeathScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myPlayerTransform = player.transform;
        myPlayerDcc = player.GetComponent<DoomCC>();
        lastCheckpoint = myPlayerTransform.position;
        showDeathScreen(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetToLastCheckpoint()
    {
        // myBf.Reset();
        // myPlayerTransform.position = lastCheckpoint;
        // myPlayerDcc.Reset();
        // showDeathScreen(false);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void SetCheckpointToCurrent()
    {
        lastCheckpoint = myPlayerTransform.position;
    }

    public void showDeathScreen(bool iss)
    {
        print("setactive has been set: " + iss);
        myDeathScreen.SetActive(iss);
        if(iss)
        {
            Invoke("ResetToLastCheckpoint",5f);
        }
    }
}
