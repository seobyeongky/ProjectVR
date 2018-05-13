
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportTerrain : TeleportMarkerBase
{
    public Bounds meshBounds { get; private set; }

    public void Awake()
    {
        CalculateBounds();
    }

    public override bool ShouldActivate(Vector3 playerPosition)
    {
        return true;
    }
    
    public override bool ShouldMovePlayer()
    {
        return true;
    }
    
    public override void Highlight(bool highlight)
    {
    }
    
    public override void SetAlpha(float tintAlpha, float alphaPercent)
    {
    }
    
    public override void UpdateVisuals()
    {
    }

    private bool CalculateBounds()
    {
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        if (terrainCollider == null)
        {
            return false;
        }

        meshBounds = terrainCollider.bounds;
        return true;
    }

}
