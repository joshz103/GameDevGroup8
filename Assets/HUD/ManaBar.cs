using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider1;
    private playerstats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerstats>();
    }

    // Update is called once per frame
    void Update()
    {
        slider1.value = stats.getMana();
        slider1.maxValue = stats.getMaxMana();
    }
}
