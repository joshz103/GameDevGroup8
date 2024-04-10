using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerstats : MonoBehaviour
{
    public GameObject damageFlash;
    private DamageVisual damageVisual;
    private PlayerSounds playerSounds;

    [SerializeField] private float offense;
    [SerializeField] private float defense; //unused
    [SerializeField] private float speed;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float luck;
    [SerializeField] private float jumpHeight; //This stat will likely be hidden from the player and never change

    [SerializeField] private float mana;
    [SerializeField] private float maxMana;

    [SerializeField] private int money;

    [SerializeField] private int enemiesKilled;

    private float baseOffense = 1f;
    private float baseDefense = 1f;
    private float baseSpeed = 1f;
    private float basemaxHealth = 10f;
    private float baseLuck = 1f;
    private float baseJumpHeight = 8f;

    // Start is called before the first frame update
    void Start()
    {
        applyBaseStats();
        damageVisual = damageFlash.GetComponent<DamageVisual>();
        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>();
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (mana < maxMana)
        {
            manaRegen();
        }

        if (mana > maxMana)
        {
            mana = maxMana;
        }

    }

    private void applyBaseStats() //Run on start to apply base character stats. Having 0 speed is no fun
    {
        offense += baseOffense;
        defense += baseDefense;
        speed += baseSpeed;
        maxHealth += basemaxHealth;
        currentHealth += basemaxHealth;
        luck += baseLuck;
        jumpHeight += baseJumpHeight;
        money = 0;
        maxMana = 100;
        mana = 50;
        enemiesKilled=0;
    }

    //Mana
    public void manaRegen()
    {
        mana += 2 * Time.deltaTime;
    }



    //Stats


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

    public void addHealth(float value){
        currentHealth += value;
    }

    public string getMaxHealthStr()
    {
        return string.Format("{0:N0}", maxHealth);
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

    public float getCurrentHealth()
    {
        return currentHealth;
    }
    public void addCurrentHealth(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float getMoney()
    {
        return money;
    }

    public string getMoneyStr()
    {
        return string.Format("{0:N0}", money);
    }

    public void addMoney(int value)
    {
        money += value;
    }

    public void subtractMoney(int value)
    {
        money -= value;
    }

    public float getEnemiesKilled()
    {
        return enemiesKilled;
    }
    public void addEnemyKilled()
    {
        enemiesKilled++;
    }

    public void damage(float value)
    {
        currentHealth -= value;
        damageVisual.damageFlash();
        playerSounds.playDamageSound();
    }

    public string getCurrentHealthStr()
    {
        return string.Format("{0:N0}", currentHealth);
    }

    public float getMana()
    {
        return mana;
    }

    public string getManaStr()
    {
        return string.Format("{0:N2}", mana);
    }

    public void addMana(float value)
    {
        mana += value;
    }

    public float getMaxMana()
    {
        return maxMana;
    }

    public string getMaxManaStr()
    {
        return string.Format("{0:N2}", mana);
    }

    public void addMaxMana(float value)
    {
        maxMana += value;
    }

}
