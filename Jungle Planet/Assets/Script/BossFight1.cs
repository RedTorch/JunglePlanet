using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    public GameObject bridge;
    public GameObject arena;
    public GunController Player;
    public Interactable interactable;
    public HoverTextManager htman;
    public BossRotateController brcon;
    public GameObject bossRotationAxis;
    public Transform bossObject;

    public bool isTriggered = false;
    public bool isWon = false;

    private float BossHealthMax = 100f; // default 500f
    private float BossHealth;

    private int currSide = 0; // ranges from 0 to 3, 0 is base position and the rest are counterclockwise
    private float nextSpikeTime = 0f;
    private float currRootRot = 0f;

    [SerializeField] private GameObject chaserPrefab;
    private float chaserSpawnCooldown = 10f;
    private int chaserBaseSpawnAmt = 1;
    private int chaserAmountPerSpawn = 1;
    [SerializeField] private GameObject spikePrefab;
    private float nextTime = 0f;
    private float nextRotShuffleTime = 0f;
    private bool isActive = false; // TODO: use this as an actual condition (so the bossfight only is going on when it should...)

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }
        //
        // some pattern of spikeprefab and chasers
        shuffleDirection();
        spawnSpikeWave();
        brcon.SetTargetAngle(90f*currSide);
        spawnChaser();
    }

    public void Reset() { // e.g. if restarting battle
        bridge.SetActive(true);
        arena.SetActive(false);
        Player.SetGunEquipped(false);
        isTriggered = false;
        Camera.main.backgroundColor = Color.white;
        RenderSettings.fogColor = Color.white;
        interactable.SetDescription("[RMB] Use door");
        htman.ShowBossText(false);
        brcon.Reset();
        BossHealth = BossHealthMax;
    }

    void BossFight1_Triggered() {
        if(!isTriggered) {
            isActive = true;
            bridge.SetActive(false);
            arena.SetActive(true);
            Player.SetGunEquipped(true);
            isTriggered = true;
            Camera.main.backgroundColor = Color.black;
            RenderSettings.fogColor = Color.black;
            interactable.SetDescription("You cannot fast travel when enemies are nearby");
            htman.ShowBossText(true);
        }
        else if(isWon) {
            // TODO: move to next scene, i.e. next level
        }
    }

    void SetIsWon() {
        isWon = true;
        isActive = false;
        interactable.SetDescription("[RMB] Use door");
    }

    public void BossTakeDamage(float damage) {
        BossHealth -= damage;
        // TODO: update UI element;
        if(BossHealth <= 0f) {
            SetIsWon();
        }
    }

    public float GetBossHPPercent() {
        return (BossHealth / BossHealthMax);
    }

    // Out: instantiates chaserPrefab ==> add feature: with random position!
    void spawnChaser() {
        if(Time.time >= nextTime) {
            chaserAmountPerSpawn = chaserBaseSpawnAmt + Mathf.CeilToInt((1f-GetBossHPPercent())*2f*chaserBaseSpawnAmt);
            for(int i = 0; i < chaserAmountPerSpawn; i++) {
                Vector3 random = new Vector3(Random.Range(-20f,20f),Random.Range(-20f,20f),Random.Range(-20f,20f));
                Instantiate(chaserPrefab, bossObject.position + random, Quaternion.identity);
            }
            nextTime = Time.time + chaserSpawnCooldown + Random.Range(chaserSpawnCooldown*(-0.5f),chaserSpawnCooldown*0.5f);
        }
    }

    void spawnSpikeWave()
    {
        if(Time.time < nextSpikeTime)
        {
            return;
        }
        GameObject newSpike = Instantiate(spikePrefab, arena.transform.position, Quaternion.Euler(0, 90f*currSide, 0));
        newSpike.transform.Translate(Vector3.right * Random.Range(-29f, 29f));
        nextSpikeTime = Time.time + (2f * (1f + (1f*GetBossHPPercent())));
    }

    public void Activate()
    {
        isActive = true;
    }

    private void shuffleDirection()
    {
        if(Time.time < nextRotShuffleTime)
        {
            return;
        }
        currSide = Random.Range(0,4);
        nextRotShuffleTime = Time.time + (4f * (1f + (1f*GetBossHPPercent())));
    }
}