using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectBase))]
public class MuzzleFlashEffect : MonoBehaviour
{
				[SerializeField]
				private Texture2D[] frontMuzzleTextures;
				[SerializeField]
				private Texture2D[] sizeMuzzleTextures;

				[SerializeField]
				private MeshRenderer[] frontFlareMaterials;
				[SerializeField]
				private bool randomizeFrontMaterials;
				[SerializeField]
				private MeshRenderer[] sideFlareMaterials;
				[SerializeField]
				private bool randomizeSideMaterials;
				EffectBase effectbase;

				private void Start()
				{
								effectbase = GetComponent<EffectBase>();
								effectbase.onUpdate += UpdateMuzzleFlash;
				}

				// Update is called once per frame
				private void UpdateMuzzleFlash()
				{
								// Rotate entire object in it's forward to add randomness
								Vector3 euler = transform.rotation.eulerAngles;
								transform.rotation = Quaternion.Euler(euler.x, euler.y, Random.Range(0.0F, 360.0F));

								// Replace textures
								Randomize(frontFlareMaterials, frontMuzzleTextures, true);
								Randomize(sideFlareMaterials, sizeMuzzleTextures, true);
				}

				private void Randomize(MeshRenderer[] renderers, Texture2D[] textures, bool randomize)
				{
								int textureID = Random.Range(0, textures.Length);

								for (int i = 0; i < renderers.Length; i++)
								{
												if (randomize)
																textureID = Random.Range(0, textures.Length);

												renderers[i].material.mainTexture = textures[textureID];
								}
				}
}
