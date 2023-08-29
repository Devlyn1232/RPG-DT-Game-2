using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBall : MonoBehaviour
{
    [SerializeField] private int _MagicDamage;
    [SerializeField] private float _BallLifespan;
    [SerializeField] private GameObject _particleDeath;
    [SerializeField] private float _BallSpeed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * _BallSpeed;
        Invoke("DestroyProjectile", _BallLifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(_MagicDamage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Instantiate(_particleDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
