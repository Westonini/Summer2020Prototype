using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Simple patrol behavior.
	/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="targets"/> array.
	/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
	///
	/// See: <see cref="Pathfinding.AIDestinationSetter"/>
	/// See: <see cref="Pathfinding.AIPath"/>
	/// See: <see cref="Pathfinding.RichAI"/>
	/// See: <see cref="Pathfinding.AILerp"/>
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
	public class Patrol : VersionedMonoBehaviour {
        /// <summary>Target points to move to in order</summary>
        //public Transform[] targets;

        /// <summary>Time in seconds to wait at each target</summary>
        private float delay;
        private float x1;
        private float y1;
        private Vector3 newPos;
        private Vector3 startingPos;
        public float maxDistancePerMove = 3;

		/// <summary>Current target index</summary>
		int index;

		IAstarAI agent;
		float switchTime = float.PositiveInfinity;

		protected override void Awake () {
			base.Awake();
			agent = GetComponent<IAstarAI>();
            switchTime = Time.time + delay;
            startingPos = transform.position;
        }

		/// <summary>Update is called once per frame</summary>
		void Update () {
			//if (targets.Length == 0) return;

			bool search = false;

			// Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
			// if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
			if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)) {
				switchTime = Time.time + delay;
			}

			if (Time.time >= switchTime) {
				index = index + 1;
				search = true;
				switchTime = float.PositiveInfinity;

                x1 = Random.Range(-maxDistancePerMove, maxDistancePerMove);
                y1 = Random.Range(-maxDistancePerMove, maxDistancePerMove);

                newPos = new Vector3(transform.position.x + x1, transform.position.y + y1, transform.position.z);

                delay = Random.Range(5, 10);

                if (newPos.x >= startingPos.x + 6 || newPos.x <= startingPos.x - 6 || newPos.y >= startingPos.y + 6 || newPos.y <= startingPos.y - 6)
                {
                    newPos = startingPos;
                    delay = 10;
                }

                switchTime = Time.time + delay;
            }

            agent.destination = newPos;

            //index = index % targets.Length;
            //agent.destination = targets[index].position;

            if (search) agent.SearchPath();
		}
	}
}
