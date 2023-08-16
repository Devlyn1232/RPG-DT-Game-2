using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Player {
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

        public float totalDefense;

        public Object.ObjectData[] equipment;


        void Awake() {
            playerStats = new Game.EntityStats(playerBaseMaxHealth, playerBaseDamage);
            currentHealth = playerStats.baseHealth;

            equipment = new Object.ObjectData[5];
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Y)) {
                AddXp(10f);
            }
        }
        public void AddXp(float xpToAdd) {
            currentXp += xpToAdd;
            if (currentXp >= totalLevelXp) {
                LevelUp();
            }
        }

        public void LevelUp() {
            totalLevelXp = totalLevelXp * Mathf.Pow((1f + 0.01f), (float)playerStats.levelCount); // complex interest for xp needed to level up (max is 133, after is infinite xp required)
            playerStats.LevelUp(levelPlayerHealth, levelPlayerBaseDamage); // apply stat changes
            currentXp = 0; // reset xp
            currentHealth = playerStats.baseHealth; // fill health
        }

        void TakeDamage(float damageInflicted) {
            float finalDamage;
            finalDamage = damageInflicted - totalDefense;
            currentHealth -= finalDamage;
        }
    }
}
