using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlide : MonoBehaviour
{
    public Slider staminaBar;
    
    private int maxStamina = 1000;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public Game.Player.Movement.PlayerMovementManager sp;

    // referenced at any time, but can only be changed in this script
    public static StaminaSlide instance;

    private void Awake()
    {
        // research
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if(regen != null)
            {
                StopCoroutine(regen);
                sp.outOfStam = false;
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not enough Stamina!");
            sp.outOfStam = true;
        }
    }

    // called over time
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            // originally += maxStamina / 100;
            currentStamina += maxStamina / 50;
            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(0.1f);
        }
        regen = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
