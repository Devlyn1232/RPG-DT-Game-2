using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateE : MonoBehaviour
{
    public float degreesPerSecond = 120f;
    public bool frendly;

    private Transform targetTransform;
    private NavMeshAgent agent;
    private Transform self;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        self = transform;
        InitializeTarget();
    }

    void FixedUpdate()
    {
        if (targetTransform == null)
        {
            InitializeTarget();
            return;
        }

        Vector3 playersDirection = targetTransform.position - self.position;
        Quaternion lookRot = Quaternion.LookRotation(playersDirection);
        self.rotation = Quaternion.RotateTowards(self.rotation, lookRot, Time.deltaTime * degreesPerSecond);
    }

    void InitializeTarget()
    {
        if (frendly)
        {
            EnemieAttributes[] enemyAttributes = GameObject.FindObjectsOfType<EnemieAttributes>();
            targetTransform = GetNearestTarget(enemyAttributes);
        }
        else
        {
            PlayerManager[] playerLifeAttributes = GameObject.FindObjectsOfType<PlayerManager>();
            targetTransform = GetNearestTarget(playerLifeAttributes);
        }
    }

    Transform GetNearestTarget(Component[] targets)
    {
        Transform nearestTarget = null;
        float targetDist = Mathf.Infinity;

        foreach (Component target in targets)
        {
            float d = Vector3.Distance(self.position, target.transform.position);
            if (nearestTarget == null || d < targetDist)
            {
                nearestTarget = target.transform;
                targetDist = d;
            }
        }

        return nearestTarget;
    }
}
