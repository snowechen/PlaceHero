using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

    public static StageController instance;

    [SerializeField]
    private GameObject[] stage;
    private List<GameObject> currMap = new List<GameObject>();

	void Awake () {
        if (instance) Destroy(gameObject);
        else instance = this;
        
        for(int i = 0; i < stage.Length; i++)
        {
            currMap.Add(Instantiate(stage[i], new Vector3(i * 100, 0, 0), Quaternion.identity) as GameObject);
        }
	}
	
    public void mapMoveStart()
    {
        StartCoroutine(mapMove());
    }

	IEnumerator mapMove()
    {
        while (true)
        {
            currMap.ForEach(map => map.transform.position += new Vector3(-0.01f, 0, 0));
            yield return new WaitForFixedUpdate();
        }
    }

    public void MapReset()
    {
        for(int i = 0; i < currMap.Count; i++)
        {
            Destroy(currMap[i]);
        }
        currMap.Clear();
        for (int i = 0; i < stage.Length; i++)
        {
            currMap.Add(Instantiate(stage[i], new Vector3(i * 100, 0, 0), Quaternion.identity) as GameObject);
        }
    }

    public void mapStop()
    {
        StopAllCoroutines();
    }

    public void mapUpdate()
    {
        if(currMap.Count < 3)
        {
            currMap.Add(Instantiate(stage[1], currMap[currMap.Count-1].transform.position + new Vector3(100, 0, 0), Quaternion.identity) as GameObject);
            Debug.LogFormat("生成新的地图：{0}, 地图数量：{1}", stage[1].name, currMap.Count);
        }
        else
        {
            if(currMap[0].tag != stage[1].tag)
            {
                string mapName = currMap[0].name;
                Destroy(currMap[0]);
                currMap.RemoveAt(0);
                Debug.LogFormat("删除旧版地图:{0}，地图长度:{1} " , mapName, currMap.Count);
                mapUpdate();
            }
            else
            {
                currMap[0].transform.position = currMap[currMap.Count - 1].transform.position + new Vector3(100, 0, 0);
                var temp = currMap[0];
                currMap.RemoveAt(0);
                currMap.Add(temp);
            }
        }
     
        
        

    }
}
