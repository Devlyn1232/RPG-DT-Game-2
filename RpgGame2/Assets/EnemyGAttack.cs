using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGAttack : MonoBehaviour
{
    public bool frendly;
    public Transform self;
    public GameObject Sword;
    private PlayerLifeAttributes targetPlayer;
    private bool CanAttack = true;
    public float AttackCooldown = 1f; // Adjust this value as needed

    void Start()
    {
        // Find the nearest player
        PlayerLifeAttributes[] playerLifeAttributes = GameObject.FindObjectsOfType<PlayerLifeAttributes>();
        float closestDistance = Mathf.Infinity;

        foreach (PlayerLifeAttributes playerAttr in playerLifeAttributes)
        {
            float distance = Vector3.Distance(transform.position, playerAttr.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPlayer = playerAttr;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(frendly)
        {
            if (CanAttack && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                StartCoroutine(AttackWithCooldown());
            }
            
        }
        else
        {
            if (CanAttack && other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                StartCoroutine(AttackWithCooldown());
            }
        }
    }

    IEnumerator AttackWithCooldown()
    {
        CanAttack = false;

        print("ASd");
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        //GameObject swordInstance = Instantiate(Sword, transform.position, Quaternion.identity);
        //swordInstance.transform.SetParent(self);

        yield return new WaitForSeconds(AttackCooldown);

        CanAttack = true;
    }
}
