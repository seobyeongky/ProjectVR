using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerlinTerrain : MonoBehaviour
{

    public Terrain terrain;

    public bool refreshTerrain = false;

    // Use this for initialization
    void Start()
    {
        terrain = GetComponent<Terrain>();
    }

    public void Update()
    {
        if (refreshTerrain)
        {
            refreshTerrain = false;
            CreatePerlinTerrain();
        }
    }

    private void CreatePerlinTerrain()
    {

    }
}