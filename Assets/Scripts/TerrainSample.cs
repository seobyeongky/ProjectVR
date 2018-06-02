using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainSample : MonoBehaviour
{
	public Terrain terrain;

	public bool snap = false;

	// Use this for initialization
	void Start()
	{
		terrain = GameObject.FindObjectOfType<Terrain>();
	}

	// Update is called once per frame
	void Update()
	{
		if (snap)
		{
			snap = false;
			if(terrain != null)
			{
				float height = terrain.terrainData.GetInterpolatedHeight(transform.position.x / terrain.terrainData.size.x, transform.position.y / terrain.terrainData.size.z);
				Vector3 norm = terrain.terrainData.GetInterpolatedNormal(transform.position.x / terrain.terrainData.size.x, transform.position.y / terrain.terrainData.size.z);

				transform.position = new Vector3(transform.position.x, height, transform.position.z);
				transform.forward = norm;
			}

		}
	}
}