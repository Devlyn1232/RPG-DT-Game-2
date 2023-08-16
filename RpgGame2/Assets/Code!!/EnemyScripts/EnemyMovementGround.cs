using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Make sure to include the NavMeshAgent namespace

public class EnemyMovementGround : MonoBehaviour
{
    public float Speed = 10f;
    private PlayerManager targetPlayer;
    private EnemieAttributes targetEnemy;
    public LayerMask obstruction;
    public Transform self;
    public float degreesPerSecond = 120f;
    public bool frendly;

    public float stoppingDistance = 1f; 
    public float avoidDistance = 200f;
    public float avoidSpeed = 20f;
    private NavMeshAgent agent; // Use NavMeshAgent directly
    private RaycastHit hitInfo;
    private RaycastHit hitInfoUp;
    private RaycastHit hitInfoDown;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Initialize the NavMeshAgent
        
        // Initialize targetPlayer and targetEnemy
        if (!frendly) // Non-friendly case
        {
            PlayerManager[] playerLifeAttributes = GameObject.FindObjectsOfType<PlayerManager>();
            float closestDistance = Mathf.Infinity;

            foreach (PlayerManager playerAttr in playerLifeAttributes)
            {
                float distance = Vector3.Distance(transform.position, playerAttr.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetPlayer = playerAttr;
                }
            }
        }
        else // Friendly case
        {
            EnemieAttributes[] enemyAttributes = GameObject.FindObjectsOfType<EnemieAttributes>();
            float closestDistance = Mathf.Infinity;

            foreach (EnemieAttributes enemyAttr in enemyAttributes)
            {
                float distance = Vector3.Distance(transform.position, enemyAttr.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = enemyAttr;
                }
            }
        }
    }

    void Update()
    {
        if (!Physics.Linecast(transform.position, GetTargetPosition(), obstruction))
        {
            Vector3 playersDirection = GetTargetPosition() - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(playersDirection);
            self.rotation = Quaternion.RotateTowards(self.rotation, lookRot, Time.deltaTime * degreesPerSecond);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, avoidDistance) ||
            Physics.Raycast(transform.position, transform.up, out hitInfoUp, avoidDistance) ||
            Physics.Raycast(transform.position, -transform.up, out hitInfoDown, avoidDistance))
        {
            agent.speed = avoidSpeed;
            agent.SetDestination(transform.position + transform.right * hitInfo.normal.x + transform.forward * avoidDistance);
        }
        else
        {
            agent.speed = Speed;
            agent.SetDestination(GetTargetPosition());

            if (Vector3.Distance(transform.position, GetTargetPosition()) <= stoppingDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    Vector3 GetTargetPosition()
    {
        if (frendly)
        {
            if (targetEnemy != null)
            {
                return targetEnemy.transform.position;
            }
        }
        else
        {
            if (targetPlayer != null)
            {
                return targetPlayer.transform.position;
            }
        }

        // Return a default position if target is null
        return Vector3.zero; // You can change this to an appropriate default position
    }
}