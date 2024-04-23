using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PathNode : MonoBehaviour
{
				[SerializeField]
				private List<PathNode> next;

				[Header("Editor Settings")]
				[SerializeField]
				private Color color = Color.red;
				[SerializeField]
				private float radius = 1f;

				public ReadOnlyCollection<PathNode> Next => next.AsReadOnly();

				public PathNode GetRandom()
				{
								return next[Random.Range(0, next.Count)];
				}

				private void OnDrawGizmos()
				{
								Gizmos.color = color;
								Gizmos.DrawWireSphere(transform.position, radius);
				}
}
