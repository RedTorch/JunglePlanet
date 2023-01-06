using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverTextManager : MonoBehaviour
{
    [SerializeField] private TMP_Text t;
    private float lastSetTime;
    [SerializeField] private GameObject weaponTextContainer;
    [SerializeField] private TMP_Text weaponText;
    [SerializeField] private GameObject bossTextContainer;
    [SerializeField] private TMP_Text bossText;
    [SerializeField] private GameObject playerHpTextContainer;
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private Image i;
    [SerializeField] private GunController gc;
    [SerializeField] private BossFight1 bossfight;
    [SerializeField] private DamageReceiver playerhp;
    [SerializeField] private Image playerReceiveDamageOverlay;
    private float damageOpacityPercent = 0f;
    // Start is called before the first frame update
    void Start()
    {
        ClearText();
        lastSetTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > (lastSetTime + 0.02f)) {
            ClearText();
        }
        if(weaponText.enabled) {
            weaponText.text = gc.getDisplayText();
        }
        if(playerHpText.enabled) {
            playerHpText.text = playerhp.getDisplayText();
        }
        if(bossText.enabled) {
            int charwidth = 40;
            int hpcharsleft = (int)Mathf.Floor(bossfight.GetBossHPPercent() * (float)charwidth);
            bossText.text = "[" + new string('=', hpcharsleft) + new string(' ', charwidth-hpcharsleft) + "]";
        }
        damageOpacityPercent = Mathf.Clamp(damageOpacityPercent-(Time.deltaTime * 1f),0f,1f);
        Color nc = Color.red;
        nc.a = 0.4f * damageOpacityPercent;
        playerReceiveDamageOverlay.color = nc;
    }

    public void SetText(string newt) {
        lastSetTime = Time.time;
        t.text = newt;
        i.enabled = true; //(t.text != "");
    }

    public void ClearText() {
        t.text = "";
        i.enabled = false;
    }

    public void ShowWeaponText(bool i) {
        weaponTextContainer.SetActive(i);
        weaponText.enabled = i;
    }

    public void ShowPlayerHpText(bool i) {
        playerHpTextContainer.SetActive(i);
        playerHpText.enabled = i;
    }

    public void ShowBossText(bool i) {
        bossTextContainer.SetActive(i);
        bossText.enabled = i;
    }

    public void ShowDamagedOverlay() {
        damageOpacityPercent = 1f;
    }
}
