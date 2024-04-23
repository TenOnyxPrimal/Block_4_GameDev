using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Achievement", menuName = "Game Data/Achievement")]
public class Achievement : ScriptableObject
{
				[SerializeField]
				private float targetValue = 1;

				[SerializeField]
				private float defaultIncrement = 1;

				[SerializeField]
				private string formatting = "{0}: {1:0.00}/{2:0.00}";

				private float value = 0;
				private bool achieved = false;

				public string Description => String.Format(formatting, name, value, targetValue);

				public UnityAction<Achievement> Achieved;

				public bool Increase()
				{
								return Increase(defaultIncrement);
				}

				public bool Increase(float increase)
				{
								if (achieved)
												return false;

								value = Mathf.Min(value + increase, targetValue);
								if (value == targetValue)
								{
												achieved = true;
												Achieved?.Invoke(this);
								}

								return achieved;
				}

				public void Reset()
				{
								value = 0;
								achieved = false;
				}
}
