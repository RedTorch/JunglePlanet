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

    private float BossHealthMax = 100f;
    private float BossHealth;


    private int currPhaseIndex = 0;

    [SerializeField] private GameObject chaserPrefab;
    private float chaserSpawnCooldown = 10f;
    private int chaserAmountPerSpawn = 5;
    [SerializeField] private GameObject spikePrefab;
    private float nextTime = 0f;
    private bool isActive = true; // TODO: use this as an actual condition (so the bossfight only is going on when it should...)

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
        // some pattern of spikeprefab and chasers
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
        interactable.SetDescription("[RMB] Use door");
    }

    void BossTakeDamage(float damage) {
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
            for(int i = 0; i < chaserAmountPerSpawn; i++) {
                Vector3 random = new Vector3(Random.Range(-20f,20f),Random.Range(-20f,20f),Random.Range(-20f,20f));
                Instantiate(chaserPrefab, bossObject.position + random, Quaternion.identity);
            }
            nextTime = Time.time + chaserSpawnCooldown + Random.Range(chaserSpawnCooldown*(-0.5f),chaserSpawnCooldown*0.5f);
        }
    }

    public void Activate()
    {
        isActive = true;
    }
}
