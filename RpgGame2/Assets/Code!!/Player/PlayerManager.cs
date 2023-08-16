using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float playerBaseMaxHealth;
    [SerializeField] private float playerBaseDamage;

    public Game.EntityStats playerStats;

    [Header("Level Stat Upgrades")]
    [SerializeField] private float levelPlayerHealth;
    [SerializeField] private float levelPlayerBaseDamage;

    [Header("Current Stats")]
    [SerializeField] private float totalLevelXp;
    public float currentXp;
    public float currentHealth;


    void Awake() {
        playerStats = new Game.EntityStats(playerBaseMaxHealth, playerBaseDamage);
        currentHealth = playerStats.baseHealth;
    }

    public void LevelUp() {
        totalLevelXp = totalLevelXp * Mathf.Pow((1f + 0.01f), (float)playerStats.levelCount);
        playerStats.LevelUp(levelPlayerHealth, levelPlayerBaseDamage);
        currentXp = 0;
        currentHealth = playerStats.baseHealth;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            AddXp(10f);
        }
    }
    void AddXp(float xpToAdd) {
        currentXp += xpToAdd;
        if (currentXp >= totalLevelXp) {
            LevelUp();
        }
    }
}
