using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{
				[SerializeField]
				private new Transform camera = null;

				[SerializeField]
				private float mouseSensitivity = 150;

				private void Update()
				{
								float movement = Input.GetAxis("Mouse Y");
								movement *= mouseSensitivity;
								camera.transform.rotation *= Quaternion.AngleAxis(movement * Time.deltaTime, Vector3.left);
				}
}
