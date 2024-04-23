using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
				[SerializeField]
				private float delayInSeconds = 1f;

				private IEnumerator Start()
				{
								yield return new WaitForSeconds(delayInSeconds);
								Destroy(gameObject);
				}
}
