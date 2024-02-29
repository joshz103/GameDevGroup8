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
    public GameObject offenseStr;
    public GameObject defenseStr;
    public GameObject speedStr;
    public GameObject luckStr;

    // Start is called before the first frame update
    void Start()
    {
        textOff = offenseStr.GetComponent<TextMeshProUGUI>();
        textDef = defenseStr.GetComponent<TextMeshProUGUI>();
        textSpeed = speedStr.GetComponent<TextMeshProUGUI>();
        textLuck = luckStr.GetComponent<TextMeshProUGUI>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    // Update is called once per frame
    void Update()
    {
        textOff.text = stats.getOffenseStr();
        textDef.text = stats.getDefenseStr();
        textSpeed.text = stats.getSpeedStr();
        textLuck.text = stats.getLuckStr();
    }
}
