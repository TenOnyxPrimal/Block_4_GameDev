using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
				private Vector3 movement;
				private bool jump;

				public Vector3 Movement
				{
								get
								{
												Vector3 result = movement;
												movement = Vector3.zero;
												return result;
								}
				}
				public bool IsJumping
				{
								get
								{
												bool result = jump;
												jump = false;
												return result;
								}
				}

				public void AddMovement(Vector3 delta)
				{
								this.movement += delta;
				}

				public void Jump()
				{
								jump = true;
				}
}
