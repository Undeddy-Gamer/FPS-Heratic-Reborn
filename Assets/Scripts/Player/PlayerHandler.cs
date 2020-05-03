using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlayerHandler : MonoBehaviour
{
    [Header("Value Variables")]
    public float curHealth;
    public float curMana, curStamina;
    public float maxHealth, maxMana, maxStamina, healRate;


    private float prevHealth, prevMana, prevStamina;
    private float healTimer;

    //[SerializeField]
    //public Stats[] stats;

    [Header("Refrence Variables")]
    public Slider healthBar;
    public Slider manaBar, staminaBar;
    public Text healthText;
    public Text manaText;
    

    [Header("Damage Effect Variables")] 
    static public bool isDead;
    bool damaged;
    bool canHeal = false;

    [Header("Check Point")]
    public Transform curCheckPoint;
        

    // Start is called before the first frame update
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        //maxHealth = maxHealth * stats[2].statValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar.value != curHealth/maxHealth)
        {
            curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
            healthBar.value = Mathf.Clamp01(curHealth / maxHealth);
        }

        if (manaBar.value != curMana / maxMana)
        {
            curMana = Mathf.Clamp(curMana, 0, maxMana);
            manaBar.value = Mathf.Clamp01(curMana / maxMana);
        }

        /* if (staminaBar.value != curStamina / maxStamina)
        {
            curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
            staminaBar.value = Mathf.Clamp01(curStamina / maxStamina);
        }*/


        //Player is Dead
        if (curHealth <= 0 && !isDead)
        {
            Death();
        }



#if UNITY_EDITOR
        //function to test damage
        //Test Damage
        if (Input.GetKeyDown(KeyCode.X))
                {
                    curHealth -= 5;
                    damaged = true;
                }
        #endif

       
        if (!canHeal && curHealth < maxHealth && curHealth > 0)
        {
            healTimer += Time.deltaTime;
            if(healTimer >= 7)      
            {
                canHeal = true;
            }
        }

        
        // Set the 
        healthText.text = System.Math.Round(curHealth) + " / " + maxHealth;
        manaText.text = System.Math.Round(curMana) + " / " + maxMana;
    }

    /// <summary>
    /// 
    /// </summary>
    private void LateUpdate()
    {
        if (curHealth < maxHealth && curHealth > 0 && canHeal)
        {
            HealOverTime();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void Death()
    {
        // set the death flag to this funciton int's called again
        isDead = true;        
        
    }


   /// <summary>
   /// 
   /// </summary>
    void Revive()
    {
        isDead = false;
        curHealth = maxHealth;
        curMana = maxMana;
        curStamina = maxStamina;

        //move and rotate spawn location
        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;
       

    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void DamagePlayer(float damage)
    {
        damaged = true;
        canHeal = false;
        curHealth -= damage;
        healTimer = 0;
        //Invoke("Healable", 10f);
    }

    /// <summary>
    /// 
    /// </summary>
    public void HealOverTime()
    {
        //curHealth += Time.deltaTime * (healRate + stats[2].statValue);
    }

   


}
