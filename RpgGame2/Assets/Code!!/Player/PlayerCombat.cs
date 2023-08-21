using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackScriptableObjects> combo;
    float lastClickedTime;
    float lastComboEnd; //Time before player can start doing the next combo
    int comboCounter;
    float timeBetweenCombos;
    float timeBetweenAttacks;

    Animator animator;
    [SerializeField] Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        if(Time.time - lastComboEnd > timeBetweenCombos && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastComboEnd >= timeBetweenAttacks)
            {
                //animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                //animator.Play("Attack", 0, 0);
                weapon.damage = combo[comboCounter].damage;
                //u can add knockback here and stuff
                comboCounter++;
                lastClickedTime = Time.time;

                if(comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
            // check is animation further than 90% playing and if its tag is attack
        {
            Invoke("EndCombo",1); //Use invoke as u can invoke it after 1 sec
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
        //if combo ends with explosion, then put it here
    }
}
