using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
				private void Awake()
				{
								UnityEngine.Cursor.lockState = CursorLockMode.Locked;
								UnityEngine.Cursor.visible = false;
				}

				public void Update()
				{
								//TODO: Move to other file
								if (Input.GetKeyDown(KeyCode.Tilde))
								{
												UnityEngine.Cursor.lockState = UnityEngine.Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
												UnityEngine.Cursor.visible = !UnityEngine.Cursor.visible;
								}
				}
}
