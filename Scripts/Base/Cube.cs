using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[SerializeField]
public class Cube : MonoBehaviour
{
    public int cubeId;
    public WorldManager.BlockType cubeType { get; private set; }
    public void Initialize(int id)
    {
        cubeId = id;
    }
}