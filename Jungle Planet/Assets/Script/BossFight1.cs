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

    public bool isTriggered = false;
    public bool isWon = false;

    private float BossHealthMax = 100f;
    private float BossHealth;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    void Reset() { // e.g. if restarting battle
        bridge.SetActive(true);
        arena.SetActive(false);
        Player.SetGunEquipped(false);
        isTriggered = false;
        Camera.main.backgroundColor = Color.white;
        RenderSettings.fogColor = Color.white;
        interactable.SetDescription("[RMB] Use door");
        htman.ShowBossText(false);
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
            // move to next scene
        }
    }

    void SetIsWon() {
        isWon = true;
        interactable.SetDescription("[RMB] Use door");
    }

    void BossPhaseSet() {

    }

    void BossTakeDamage(float damage) {
        BossHealth -= damage;
        // update UI element;
        if(BossHealth <= 0f) {
            SetIsWon();
        }
    }

    public float GetBossHPPercent() {
        return (BossHealth / BossHealthMax);
    }
}
