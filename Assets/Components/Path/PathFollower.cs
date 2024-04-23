using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathFollower : MonoBehaviour
{
				[SerializeField]
				private PathNode startNode;

				[SerializeField]
				private float nearDistance = 0.1f;

				[SerializeField]
				private bool startFollowingPath = true;

				private bool isFollowing;

				private NavMeshAgent agent;
				private NavMeshPath path;
				private PathNode currentNode;

				private void Awake()
				{
								path = new NavMeshPath();
								isFollowing = startFollowingPath;
								agent = GetComponent<NavMeshAgent>();
								SwitchNode(startNode);
				}

				private void Update()
				{
								if (!isFollowing)
												return;

								if (agent.remainingDistance - agent.stoppingDistance < nearDistance)
												SwitchNode(currentNode.GetRandom());

				}

				private void SwitchNode(PathNode node)
				{
								currentNode = node;
								agent.CalculatePath(node.transform.position, path);
								agent.SetPath(path);
								agent.isStopped = false;
				}

				public void StopFollowing()
				{
								isFollowing = false;
								agent.isStopped = true;
				}

				public void Continue()
				{
								if (isFollowing || !currentNode)
												return;

								isFollowing = true;
								SwitchNode(currentNode);
				}
}
