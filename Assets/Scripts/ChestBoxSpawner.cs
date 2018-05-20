using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ChestBoxSpawner : MonoBehaviour
{
	public GameObject chestPrefab;

	public Terrain terrain;
	public Rect bounds;
	public float heightLimit = 20;

	public int chestSpawnLimit = 100;
	public bool spawn = false;
	public void Update()
	{
		if (spawn)
		{
			spawn = false;
			SpawnChests();
		}
	}

	public void SpawnChests()
	{
		List<GameObject> chestList = new List<GameObject>();
		for (int a = 0; chestList.Count < chestSpawnLimit; a++)
		{
			Vector2 randPos = Vector2.zero;
			randPos.x = Random.Range(bounds.x, bounds.width);
			randPos.y = Random.Range(bounds.y, bounds.height);

			float height = terrain.terrainData.GetInterpolatedHeight(randPos.x/bounds.x, randPos.y/bounds.y);
			Vector3 normals = terrain.terrainData.GetInterpolatedNormal(randPos.x / bounds.x, randPos.y / bounds.y);
			if (height <= heightLimit)
			{
				bool valid = true;
				foreach (var chest in chestList)
				{
					var pos = chest.transform.position;
					pos.y = pos.z;
					pos.z = 0;
					if (Vector3.Distance(randPos, pos) < 1)
					{
						valid = false;
						break;
					}
				}
				if (valid)
				{
					GameObject newChest = GameObject.Instantiate<GameObject>(chestPrefab,transform);
					newChest.transform.position = new Vector3(randPos.x, height, randPos.y);
					//rotate according to land normals... base is 0,1,0 so for other 
					newChest.transform.up = normals;
					chestList.Add(newChest);
				}
			}
		}
	}
}