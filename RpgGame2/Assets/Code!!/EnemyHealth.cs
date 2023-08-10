using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberPunk.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;
        public GameObject burningParticle;
        public GameObject DeathParticle;

        void Start()
        {
            currentHealth = maxHealth;
        }

        void Dead()
        {
            Instantiate(DeathParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        public void Burning()
        {
            StartCoroutine(OnFire());
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Dead();
            }
        }

        IEnumerator OnFire()
        {
            for (int i = 0; i < 5; i++)
            {
                currentHealth -= 1;
                Instantiate(burningParticle, transform.position, transform.rotation);
                yield return new WaitForSeconds(1f);
                if (currentHealth <= 0)
                {
                    Dead();
                }
            }
        }
    }
}