using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementBase))]
public class ActorMovement : MonoBehaviour
{
				[SerializeField]
				private float mouseSensitivity = 150;

				private MovementBase movementBase;

				private void Awake()
				{
								movementBase = GetComponent<MovementBase>();
				}

				private void HandleMouse()
				{
								Vector2 movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
								movement *= mouseSensitivity;
								transform.rotation *= Quaternion.AngleAxis(movement.x * Time.deltaTime, Vector3.up);
				}

				private void Update()
				{
								HandleMouse();

								Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
								input = transform.rotation * input;
								movementBase.AddMovement(input);

								if (Input.GetButtonDown("Jump"))
												movementBase.Jump();
				}
}
