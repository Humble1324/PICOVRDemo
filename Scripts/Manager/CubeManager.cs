using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : Singleton<CubeManager>
{
    public List<GameObject> cubes = new List<GameObject>();
    private Dictionary<int, GameObject> cubeDict = new Dictionary<int, GameObject>();
    private static int nextCubeId = 0;
    public int onRayDetectedCubeId;
    protected override void  Awake()
    {
        base.Awake();
        CreateCube(new Vector3(0,0,0));
    }
    private HashSet<Vector3> occupiedCells = new HashSet<Vector3>();

    public void CreateCube(Vector3 gridPos, int typeId =0)
    {


        // 新增网格位置检查
        if (occupiedCells.Contains(gridPos))
        {
            Debug.LogWarning($"格子位置 {gridPos} 已被占用");
            return;
        }

        GameObject cube = Instantiate(WorldManager.Instance.allBockTypes[typeId].blockPrefab, gridPos, Quaternion.identity);
        cube.transform.parent = transform;
        
        // 记录已占用的格子
        occupiedCells.Add(gridPos);
        
        // 分配唯一ID并记录
        int cubeId = nextCubeId++;
        cubeDict.Add(cubeId, cube);
        
        // 添加ID组件
        var cubeInfo = cube.GetComponent<Cube>();
        cubeInfo.Initialize(cubeId);
    }
    public void TryRemoveCube()
    {
        int cubeId = onRayDetectedCubeId;
        if(cubeId == -1)
        {
            print("没有选中方块");
            return;
        }
        if(cubeDict.TryGetValue(cubeId, out GameObject cube))
        {
            // 获取实际网格位置并移除
            Vector3Int gridPos = new Vector3Int(
                Mathf.RoundToInt(cube.transform.position.x),
                Mathf.RoundToInt(cube.transform.position.y),
                Mathf.RoundToInt(cube.transform.position.z)
            );
            occupiedCells.Remove(gridPos);
            
            Destroy(cube);
            cubeDict.Remove(cubeId);
        }
    }
    public void RemoveCube(int cubeId)
    {
        if(cubeDict.TryGetValue(cubeId, out GameObject cube))
        {
            // 获取实际网格位置并移除
            Vector3Int gridPos = new Vector3Int(
                Mathf.RoundToInt(cube.transform.position.x),
                Mathf.RoundToInt(cube.transform.position.y),
                Mathf.RoundToInt(cube.transform.position.z)
            );
            occupiedCells.Remove(gridPos);
            
            Destroy(cube);
            cubeDict.Remove(cubeId);
        }
    }
}
