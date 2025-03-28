using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{
    public List<BlockType> allBockTypes = new List<BlockType>();

    public void Start()
    {
        TestFunction();
    }

    public void TestFunction()
    {
        
        print("TestFunction");
        SlotManager.Instance.TestAddFunction();
        
    }
    [System.Serializable]
    public class BlockType
    {
         public string blockName;
         public GameObject blockPrefab;
         public int typeId;
         public Sprite icon;
         [Header("Texture Values")]
         public int backFaceTexture;
         public int frontFaceTexture;
         public int topFaceTexture;
         public int bottomFaceTexture;
         public int leftFaceTexture;
         public int rightFaceTexture;

         // Back, Front, Top, Bottom, Left, Right

         public int GetTextureID (int faceIndex) {

             switch (faceIndex) {

                 case 0:
                     return backFaceTexture;
                 case 1:
                     return frontFaceTexture;
                 case 2:
                     return topFaceTexture;
                 case 3:
                     return bottomFaceTexture;
                 case 4:
                     return leftFaceTexture;
                 case 5:
                     return rightFaceTexture;
                 default:
                     Debug.Log("Error in GetTextureID; invalid face index");
                     return 0;


             }

         }
    }
}

