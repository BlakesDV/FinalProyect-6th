using UnityEngine;

/// <summary>
/// Represents a basic agent with movement properties.
/// </summary>
public class BasicAgent : MonoBehaviour {
    [SerializeField] protected float speed, maxVel, maxSteerForce;
    [SerializeField] protected float slowingRadius, stopThreshold;
    [SerializeField] protected float wanderDisplacement, wanderRadius;
    protected Transform target;
    /// <summary>
    /// The next position for the agent to wander towards.
    /// This can be null, indicating that the next position is not yet determined.
    /// </summary>
    protected Vector3? wanderNextPosition;

    /// <summary>
    /// Gets the maximum speed of the agent.
    /// </summary>
    /// <returns>The maximum speed of the agent.</returns>
    public float getSpeed () {
        return speed;
    }

    /// <summary>
    /// Gets the maximum velocity of the agent.
    /// </summary>
    /// <returns>The maximum velocity of the agent.</returns>
    public float getMaxVel () {
        return maxVel;
    }

    /// <summary>
    /// Gets the maximum steering force of the agent.
    /// </summary>
    /// <returns>The maximum steering force of the agent.</returns>
    public float getMaxSteerForce () {
        return maxSteerForce;
    }

    /// <summary>
    /// Gets the displacement for wander behavior of the agent.
    /// </summary>
    /// <returns>The wander displacement of the agent.</returns>
    public float getWanderDisplacement () {
        return wanderDisplacement;
    }

    /// <summary>
    /// Gets the radius for wander behavior of the agent.
    /// </summary>
    /// <returns>The wander radius of the agent.</returns>
    public float getWanderRadius () {
        return wanderRadius;
    }

    /// <summary>
    /// Gets the slowing radius for arrival behavior of the agent.
    /// </summary>
    /// <returns>The slowing radius of the agent.</returns>
    public float getSlowingRadius () {
        return slowingRadius;
    }

    /// <summary>
    /// Gets the threshold for arrival behavior of the agent.
    /// </summary>
    /// <returns>The threshold for arrival of the agent.</returns>
    public float getThreshold () {
        return stopThreshold;
    }
}