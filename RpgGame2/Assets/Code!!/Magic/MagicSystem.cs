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

    [Header("Transparency")]
    public GameObject purpCirc;
    [SerializeField] private Color purpColour;
    public GameObject orangeCirc;
    [SerializeField] private Color orangeColour;
    public GameObject blueCirc;
    [SerializeField] private Color blueColour;

    private void Start()
    {
        _CurrentSpell = 0;
        magicEquip.text = "Current Spell: Nothing!";
    }

    void Update()
    {
        purpCirc.GetComponent<SpriteRenderer>().material.color = purpColour;
        orangeCirc.GetComponent<SpriteRenderer>().material.color = orangeColour;
        blueCirc.GetComponent<SpriteRenderer>().material.color = blueColour;

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
                        purpColour.a = Mathf.Lerp(purpColour.a, 1f, 1f);
                        StartCoroutine(ManaBallShoot());
                        Invoke("fadeOutPurp", 0.5f);
                        _CanAttack = false;
                        break;
                    case 2:
                        orangeColour.a = Mathf.Lerp(0f, 1f, 1f);
                        StartCoroutine(FireBallShoot());
                        Invoke("fadeOutOrange", 0.5f);
                        _CanAttack = false;
                        break;
                    case 3:
                        blueColour.a = Mathf.Lerp(0f, 1f, 1f);
                        StartCoroutine(IceBoltShoot());
                        Invoke("fadeOutBlue", 0.5f);
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

    private void fadeOutPurp()
    {
        purpColour.a = Mathf.Lerp(purpColour.a, 0f, 1f);
    }
    private void fadeOutOrange()
    {
        orangeColour.a = Mathf.Lerp(purpColour.a, 0f, 1f);
    }
    private void fadeOutBlue()
    {
        blueColour.a = Mathf.Lerp(purpColour.a, 0f, 1f);
    }
}
