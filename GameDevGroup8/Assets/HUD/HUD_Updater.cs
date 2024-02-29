using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Offense : MonoBehaviour
{
    private playerstats stats;
    private TextMeshProUGUI textOff;
    private TextMeshProUGUI textDef;
    private TextMeshProUGUI textSpeed;
    private TextMeshProUGUI textLuck;
    private TextMeshProUGUI textMaxHealth;
    private TextMeshProUGUI textCurrentHealth;
    public GameObject offenseStr;
    public GameObject defenseStr;
    public GameObject speedStr;
    public GameObject luckStr;
    public GameObject maxHealthStr;
    public GameObject currentHealthStr;

    // Start is called before the first frame update
    void Start()
    {
        textOff = offenseStr.GetComponent<TextMeshProUGUI>();
        textDef = defenseStr.GetComponent<TextMeshProUGUI>();
        textSpeed = speedStr.GetComponent<TextMeshProUGUI>();
        textLuck = luckStr.GetComponent<TextMeshProUGUI>();
        textMaxHealth = maxHealthStr.GetComponent<TextMeshProUGUI>();
        textCurrentHealth = currentHealthStr.GetComponent<TextMeshProUGUI>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    // Update is called once per frame
    void Update()
    {
        textOff.text = stats.getOffenseStr();
        textDef.text = stats.getDefenseStr();
        textSpeed.text = stats.getSpeedStr();
        textLuck.text = stats.getLuckStr();
        textMaxHealth.text = stats.getMaxHealthStr();
        textCurrentHealth.text = stats.getCurrentHealthStr();
    }
}
