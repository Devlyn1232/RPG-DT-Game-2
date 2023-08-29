using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicSystem : MonoBehaviour
{
    [Header("Cooldowns")]
    [SerializeField] bool _CanAttack = true;
    [SerializeField] float _ManaBallCooldown = 1f;
    [SerializeField] float _FireBallCooldown = 0.5f;
    [SerializeField] float _IceBoltCooldown = 2f;

    [Header("Equipped Spell")]
    [SerializeField] int _CurrentSpell = 1;

    [Header("References")]
    public Transform ShootPoint;
    public GameObject ManaBall;
    public GameObject FireBall;
    public GameObject IceBolt;
    public TMP_Text magicEquip;
    public GameObject purpCirc;
    public GameObject orangeCirc;
    public GameObject blueCirc;

    private void Start()
    {
        _CurrentSpell = 0;
        magicEquip.text = "Current Spell: Nothing!";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _CurrentSpell = 1;
            magicEquip.text = "Current Spell: Manaball";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _CurrentSpell = 2;
            magicEquip.text = "Current Spell: Fireball";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _CurrentSpell = 3;
            magicEquip.text = "Current Spell: Icebolt";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_CanAttack)
            {
                switch (_CurrentSpell)
                {
                    case 1:
                        StartCoroutine(ManaBallShoot());
                        _CanAttack = false;
                        break;
                    case 2:
                        StartCoroutine(FireBallShoot());
                        _CanAttack = false;
                        break;
                    case 3:
                        StartCoroutine(IceBoltShoot());
                        _CanAttack = false;
                        break;
                }
            }
        }
    }

    IEnumerator ManaBallShoot()
    {
        GameObject projectile = Instantiate(ManaBall,ShootPoint.position,ShootPoint.rotation);
        yield return new WaitForSeconds(_ManaBallCooldown);
        _CanAttack = true;
    }

    IEnumerator FireBallShoot()
    {
        GameObject projectile = Instantiate(FireBall, ShootPoint.position, ShootPoint.rotation);
        yield return new WaitForSeconds(_FireBallCooldown);
        _CanAttack = true;
    }

    IEnumerator IceBoltShoot()
    {
        GameObject projectile = Instantiate(IceBolt, ShootPoint.position, ShootPoint.rotation);
        yield return new WaitForSeconds(_IceBoltCooldown);
        _CanAttack = true;
    }
}
