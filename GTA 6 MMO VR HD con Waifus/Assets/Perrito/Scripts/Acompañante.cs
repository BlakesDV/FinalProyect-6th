using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class Acompañante : BasicAgent
{
    [SerializeField] float eyesPerceptRadious, earsPerceptRadious;
    [SerializeField] Transform eyesPercept, earsPercept;
    [SerializeField] Animator animator;
    [SerializeField] AgressiveAgentStates agentState;
    Rigidbody rb;
    Collider[] perceibed, perceibed2;
    string currentAnimationStateName;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = AgressiveAgentStates.None;
        currentAnimationStateName = "";
    }

    void Update()
    {
        perceptionManager();
        decisionManager();
    }

    private void FixedUpdate()
    {
        perceibed = Physics.OverlapSphere(eyesPercept.position, eyesPerceptRadious);
        perceibed2 = Physics.OverlapSphere(earsPercept.position, earsPerceptRadious);
    }

    void perceptionManager()
    {
        target = null;
        if (perceibed != null)
        {
            foreach (Collider tmp in perceibed)
            {
                if (tmp.CompareTag("Enemy"))
                {
                    target = tmp.transform;
                }
            }
        }
        if (perceibed2 != null)
        {
            foreach (Collider tmp in perceibed2)
            {
                if (tmp.CompareTag("Enemy"))
                {
                    target = tmp.transform;
                }
            }
        }
    }

    void decisionManager()
    {
        AgressiveAgentStates newState;
        if (target == null)
        {
            newState = AgressiveAgentStates.Wander;
        }
        else if (target.GetComponent<Rigidbody>().mass < rb.mass)
        {
            newState = AgressiveAgentStates.Pursuit;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold)
            {
                newState = AgressiveAgentStates.Attack;
            }
        }
        else
        {
            newState = AgressiveAgentStates.Escape;
        }
        changeAgentState(newState);
        actionManager();
        movementManager();
    }

    /// <param name="t_newState">The new state of the agent.</param>
    void changeAgentState(AgressiveAgentStates t_newState)
    {
        if (agentState == t_newState)
        {
            return;
        }
        agentState = t_newState;
        if (agentState != AgressiveAgentStates.Wander)
        {
            wanderNextPosition = null;
        }
    }

    void actionManager()
    {
        switch (agentState)
        {
            case AgressiveAgentStates.None:
                break;
            case AgressiveAgentStates.Attack:
                // biting();
                break;
            case AgressiveAgentStates.Escape:
                // screaming();
                break;
        }
    }

    void movementManager()
    {
        switch (agentState)
        {
            case AgressiveAgentStates.None:
                rb.velocity = Vector3.zero;
                break;
            case AgressiveAgentStates.Pursuit:
                pursuiting();
                break;
            case AgressiveAgentStates.Attack:
                attacking();
                break;
            case AgressiveAgentStates.Escape:
                escaping();
                break;
            case AgressiveAgentStates.Wander:
                wandering();
                break;
        }
    }

    private void wandering()
    {
        if (!currentAnimationStateName.Equals("RaptorArmature|Raptor_SniffGround_Anim"))
        {
            Debug.Log(currentAnimationStateName);
            animator.Play("RaptorArmature|Raptor_SniffGround_Anim", 0);
            currentAnimationStateName = "RaptorArmature|Raptor_SniffGround_Anim";
        }
        if ((wanderNextPosition == null) ||
            (Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f))
        {
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    private void pursuiting()
    {
        if (!currentAnimationStateName.Equals("RaptorArmature|Raptor_Run1_Anim") && !currentAnimationStateName.Equals("RaptorArmature|Raptor_Walk_Anim"))
        {
            animator.Play("RaptorArmature|Raptor_Run1_Anim", 0);
            currentAnimationStateName = "RaptorArmature|Raptor_Run1_Anim";
        }
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius)
        {
            if (!currentAnimationStateName.Equals("RaptorArmature|Raptor_Walk_Anim"))
            {
                animator.Play("RaptorArmature|Raptor_Walk_Anim", 0);
                currentAnimationStateName = "RaptorArmature|Raptor_Walk_Anim";
            }
        }
        maxVel /= 2;
    }

    private void attacking()
    {
        if (!currentAnimationStateName.Equals("RaptorArmature|Raptor_Bite1_Anim"))
        {
            animator.Play("RaptorArmature|Raptor_Bite1_Anim", 0);
            currentAnimationStateName = "RaptorArmature|Raptor_Bite1_Anim";
        }
    }

    private void escaping()
    {
        if (!currentAnimationStateName.Equals("RaptorArmature|Raptor_Run1_Anim"))
        {
            animator.Play("RaptorArmature|Raptor_Run1_Anim", 0);
            currentAnimationStateName = "RaptorArmature|Raptor_Run1_Anim";
        }
        rb.velocity = SteeringBehaviours.flee(this, target.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyesPercept.position, eyesPerceptRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(earsPercept.position, earsPerceptRadious);
    }

    private enum AgressiveAgentStates
    {
        None,
        Pursuit,
        Attack,
        Escape,
        Wander
    }
}
