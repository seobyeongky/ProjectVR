using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VRInputControls : MonoBehaviour
{
    public enum EInteractionMode
    {
        Standard = 0,
        Teleport,
        ArmSwing,
        GrabPull,
    }

    public EInteractionMode mode = EInteractionMode.Standard;

    public Player player;
    
    public List<ModeSetup> modeParents = new List<ModeSetup>();

    private void Awake()
    {
    }

    private void Start()
    {
        player = Player.instance;
        if (player == null)
        {
            Debug.LogError("No player instance found!");
            gameObject.SetActive(false);
        }
        SetMode(mode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetMode(EInteractionMode type)
    {
        foreach (var item in modeParents)
        {
            if (item.type == mode)
            {
                if (item.modeRoot != null)
                {
                    item.modeRoot.SetActive(true);
                }
            }
            else
            {
                if (item.modeRoot != null)
                {
                    item.modeRoot.SetActive(false);
                }
            }
        }
    }

    [System.Serializable]
    public class ModeSetup
    {
        public EInteractionMode type;
        public GameObject modeRoot;

    }
}