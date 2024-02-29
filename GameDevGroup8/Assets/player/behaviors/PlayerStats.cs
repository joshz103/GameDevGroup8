using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstats : MonoBehaviour
{
    [SerializeField] private float offense;
    [SerializeField] private float defense;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float luck;
    [SerializeField] private float jumpHeight; //This stat will likely be hidden from the player and never change

    private float baseOffense = 1f;
    private float baseDefense = 1f;
    private float baseSpeed = 2.5f;
    private float basemaxHealth = 10f;
    private float baseLuck = 1f;
    private float baseJumpHeight = 8f;



    // Start is called before the first frame update
    void Start()
    {
        applyBaseStats();
    }

    private void applyBaseStats() //Run on start to apply base character stats. Having 0 speed is no fun
    {
        offense += baseOffense;
        defense += baseDefense;
        speed += baseSpeed;
        maxHealth += basemaxHealth;
        luck += baseLuck;
        jumpHeight += baseJumpHeight;
    }


    public float getOffense()
    {
        return offense;
    }

    public string getOffenseStr()
    {
        return string.Format("{0:N2}", offense);
    }

    public void addOffense(float value)
    {
        offense += value;
    }

    public float getDefense()
    {
        return defense;
    }

    public string getDefenseStr()
    {
        return string.Format("{0:N2}", defense);
    }

    public void addDefense(float value)
    {
        defense += value;
    }

    public float getSpeed()
    {
        return speed;
    }

    public string getSpeedStr()
    {
        return string.Format("{0:N2}", speed);
    }

    public void addSpeed(float value)
    {
        speed += value;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void addMaxHealth(float value)
    {
        maxHealth += value;
    }

    public float getLuck()
    {
        return luck;
    }

    public string getLuckStr()
    {
        return string.Format("{0:N2}", luck);
    }
    public void addLuck(float value)
    {
        luck += value;
    }

    public float getJumpHeight()
    {
        return jumpHeight;
    }
    public void addJumpHeight(float value)
    {
        jumpHeight += value;
    }

}
