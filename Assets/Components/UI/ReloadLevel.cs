using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
				[SerializeField]
				int levelIndexToLoad;

				public int LevelIndexToLoad
				{
								get { return levelIndexToLoad; }
								set { levelIndexToLoad = value; }
				}

				public void Reload()
				{
								SceneManager.LoadScene(levelIndexToLoad);
				}
}
