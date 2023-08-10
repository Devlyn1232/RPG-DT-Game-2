using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementGround : MonoBehaviour
{
    public float Speed = 10f;
    private PlayerLifeAttributes targetPlayer;
    private EnemieAttributes targetEnemy;
    public LayerMask obstruction;
    public Transform self;
    public float degreesPerSecond = 120f;
    public bool frendly;

    public float stoppingDistance = 1f; 
    public float avoidDistance = 200f;
    public float avoidSpeed = 20f;
    private UnityEngine.AI.NavMeshAgent agent;
    
    private RaycastHit hitInfo;
    private RaycastHit hitInfoUp;
    private RaycastHit hitInfoDown;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Other initialization code here if needed
        if (frendly)
        {
            EnemieAttributes[] enemyAttributes = GameObject.FindObjectsOfType<EnemieAttributes>();
            EnemieAttributes nearestEnemieAttributes = null;
            float enemyDist = Mathf.Infinity;

            foreach (EnemieAttributes enemyAttr in enemyAttributes)
            {
                float d = Vector3.Distance(this.transform.position, enemyAttr.transform.position);
                if (nearestEnemieAttributes == null || d < enemyDist)
                {
                    nearestEnemieAttributes = enemyAttr;
                    enemyDist = d;
                }
            }
            targetEnemy = nearestEnemieAttributes;
        }
        else
        {
            PlayerLifeAttributes[] playerLifeAttributes = GameObject.FindObjectsOfType<PlayerLifeAttributes>();
            PlayerLifeAttributes nearestPlayerLifeAttributes = null;
            float playerDist = Mathf.Infinity;

            foreach (PlayerLifeAttributes playerAttr in playerLifeAttributes)
            {
                float d = Vector3.Distance(this.transform.position, playerAttr.transform.position);
                if (nearestPlayerLifeAttributes == null || d < playerDist)
                {
                    nearestPlayerLifeAttributes = playerAttr;
                    playerDist = d;
                }
            }
            targetPlayer = nearestPlayerLifeAttributes;
        }
    }

    void FixedUpdate()
    {
        
        if(!Physics.Linecast(transform.position, GetTargetPosition(), obstruction))
        {
            
            
            Vector3 playersDirection = GetTargetPosition() - this.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(playersDirection);
            self.rotation = Quaternion.RotateTowards(self.rotation, lookRot, Time.deltaTime * degreesPerSecond);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
             if (Physics.Raycast(transform.position, transform.forward, out hitInfo, avoidDistance) ||
            Physics.Raycast(transform.position, transform.up, out hitInfoUp, avoidDistance) ||
            Physics.Raycast(transform.position, -transform.up, out hitInfoDown, avoidDistance))
            {
                // Move the agent around the obstacle
                agent.speed = avoidSpeed;
                agent.SetDestination(transform.position + transform.right * hitInfo.normal.x + transform.forward * avoidDistance);
            }
            else
            {
                agent.speed = Speed;
                agent.SetDestination(GetTargetPosition());
                
                
                // Move the agent towards the target
                
                
            

            // Stop the agent when it is close to the target
            
            if (Vector3.Distance(transform.position, GetTargetPosition()) <= stoppingDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        

    }
    Vector3 GetTargetPosition()
    {
        if (frendly)
        {
            return targetEnemy.transform.position;
        }
        else
        {
            return targetPlayer.transform.position;
        }
    }

    /*
    GameObject GetTarget()
    {
        if (frendly)
        {
            return targetEnemy.gameObject;
        }
        else
        {
            return targetPlayer.gameObject;
        }
    }
    */
}
}