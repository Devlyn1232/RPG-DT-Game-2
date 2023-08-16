using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [System.Serializable] public class EntityStats
    {
        public float baseHealth;
        public float baseDamage;

        public int levelCount;

        public EntityStats(float newBaseHealth, float newBaseDamage) {
            baseHealth = newBaseHealth;
            baseDamage = newBaseDamage;
            
            levelCount = 1;
        }

        public void LevelUp(float healthAdd, float damageAdd) {
            baseHealth += healthAdd;
            baseDamage += damageAdd;

            levelCount ++;
        }
    }
}
