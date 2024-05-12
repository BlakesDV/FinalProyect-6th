using UnityEngine;

/// <summary>
/// Class containing static methods for implementing steering behaviors for agents.
/// </summary>
public class SteeringBehaviours {
    /// <summary>
    /// Moves the agent towards the target position.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move towards.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    public static Vector3 seek (BasicAgent agent, Vector3 targetPosition) {
        Vector3 desiredVel = targetPosition - agent.transform.position;
        return calculateSteer(agent, desiredVel);
    }

    /// <summary>
    /// Moves the agent away from the target position.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move away from.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    public static Vector3 flee (BasicAgent agent, Vector3 targetPosition) {
        Vector3 desiredVel = agent.transform.position - targetPosition;
        return calculateSteer(agent, desiredVel);
    }

    /// <summary>
    /// Moves the agent towards the target position, slowing down as it approaches.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="targetPosition">The position to move towards.</param>
    /// <param name="slowingRadious">The radius at which the agent starts slowing down.</param>
    /// <param name="threshold">The threshold distance for applying slowing down.</param>
    /// <returns>The velocity to apply to the agent.</returns>
    public static Vector3 arrival (BasicAgent agent, Vector3 targetPosition, float slowingRadious, float threshold) {
        float distance = Vector3.Distance(agent.transform.position, targetPosition);
        if (distance < slowingRadious) {
            if (distance < threshold) {
                //return Vector3.zero;
            }
            return agent.GetComponent<Rigidbody>().velocity * ( distance / slowingRadious );
        } else {
            return agent.GetComponent<Rigidbody>().velocity;
        }
    }

    /// <summary>
    /// Calculates a random wandering direction for the agent.
    /// </summary>
    /// <param name="agent">The agent to wander.</param>
    /// <returns>The random wandering direction.</returns>
    public static Vector3 wanderNextPos (BasicAgent agent) {
        Vector3 velCopy = agent.transform.GetComponent<Rigidbody>().velocity;
        velCopy.Normalize();
        velCopy *= agent.getWanderDisplacement();
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        randomDirection.Normalize();
        randomDirection *= agent.getWanderRadius();
        randomDirection += velCopy;
        randomDirection += agent.transform.position;
        return randomDirection;
    }

    /// <summary>
    /// Calculates the steering force to apply to the agent.
    /// </summary>
    /// <param name="agent">The agent to be moved.</param>
    /// <param name="desiredVel">The desired velocity of the agent.</param>
    /// <returns>The steering force to apply to the agent.</returns>
    private static Vector3 calculateSteer (BasicAgent agent, Vector3 desiredVel) {
        Rigidbody agentRB = agent.GetComponent<Rigidbody>();
        desiredVel.Normalize();
        desiredVel *= agent.getMaxVel();
        Vector3 steering = desiredVel - agentRB.velocity;
        steering = truncate(steering, agent.getMaxSteerForce());
        steering /= agentRB.mass;
        steering += agentRB.velocity;
        steering = truncate(steering, agent.getSpeed());
        steering.y = agentRB.velocity.y;
        lookAt(agent.transform, steering);
        return steering;
    }

    /// <summary>
    /// Makes the agent look at the given direction.
    /// </summary>
    /// <param name="agent">The agent to rotate.</param>
    /// <param name="currentVel">The direction to look at.</param>
    public static void lookAt (Transform agent, Vector3 currentVel) {
        agent.transform.LookAt(agent.position + currentVel);
    }

    /// <summary>
    /// Truncates the vector if its magnitude exceeds the specified maximum value.
    /// </summary>
    /// <param name="vector">The vector to truncate.</param>
    /// <param name="maxValue">The maximum value for the vector's magnitude.</param>
    /// <returns>The truncated vector.</returns>
    private static Vector3 truncate (Vector3 vector, float maxValue) {
        if (vector.magnitude <= maxValue) {
            return vector;
        }
        vector.Normalize();
        return vector *= maxValue;
    }
}