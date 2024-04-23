using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUI : MonoBehaviour
{
				[SerializeField]
				int sceneIndex;
				[SerializeField]
				int gameOverSceneIndex;

				static int gameOverScene;

				// Start is called before the first frame update
				void Start()
				{
								if (!SceneManager.GetSceneByBuildIndex(sceneIndex).isLoaded)
												SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);

								gameOverScene = gameOverSceneIndex;
				}

				private void OnDisable()
				{
								SceneManager.UnloadSceneAsync(sceneIndex);
				}

				public static void GameOver()
				{
								SceneManager.LoadScene(gameOverScene, LoadSceneMode.Single);
				}
}
