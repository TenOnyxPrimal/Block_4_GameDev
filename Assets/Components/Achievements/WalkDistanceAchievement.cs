using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkDistanceAchievement : MonoBehaviour
{
				[SerializeField]
				private Achievement achievement;

				private Vector3 position;

				private void Start()
				{
								position = transform.position;
				}

				private void Update()
				{
								Vector3 newPosition = transform.position;
								Vector3 delta = newPosition - position;
								delta.y = 0;
								achievement.Increase(delta.magnitude);
								position = newPosition;
				}
}
