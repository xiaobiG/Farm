using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    // 空地
    private GameObject prefab_Crop_Empty;

    // 临时持有的建筑物 脚本
    private BaseBuild tempBuild;
    private GameObject tempBuildPrefab;

    // 全部建筑物
    private List<BaseBuild> buildList = new List<BaseBuild>();

    void Start()
    {
        prefab_Crop_Empty = Resources.Load<GameObject>("Crop_Empty");
        tempBuild = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Build_Shop")).GetComponent<BaseBuild>();
        Build(Resources.Load<GameObject>("Build_Shop"));
    }

    void Update()
    {
        if (tempBuild != null)
        {
            BuildForUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Build(Resources.Load<GameObject>("Build_Shop"));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Build(Resources.Load<GameObject>("Crop_Empty"));
        }

    }

    public void Build(GameObject prefab)
    {
        // 预制件
        tempBuildPrefab = prefab;
        // 预制件实例
        if (tempBuild != null)
        {
            // 销毁之前的临时实例
            Destroy(tempBuild.gameObject);
        }
        tempBuild = GameObject.Instantiate<GameObject>(prefab).GetComponent<BaseBuild>();
    }

    private void BuildForUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 碰撞点地面
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
            {
                // 可以吸附的植物
                BaseBuild build = null;
                // 查找 有没有很近的植物
                for (int i = 0; i < buildList.Count; i++)
                {
                    // 如果小于一定距离
                    if (Vector3.Distance(hit.point, buildList[i].transform.position) < (buildList[i].Size / 2) + 2 + (tempBuild.Size / 2))
                    {
                        build = buildList[i];
                        break;
                    }
                }
                if (build != null)
                {
                    // 吸附
                    float offset = build.Size / 2 + tempBuild.Size / 2;
                    Vector3 top = build.transform.position + new Vector3(0, 0, offset);
                    Vector3 bottom = build.transform.position + new Vector3(0, 0, -offset);
                    Vector3 left = build.transform.position + new Vector3(-offset, 0, 0);
                    Vector3 right = build.transform.position + new Vector3(offset, 0, 0);
                    Vector3[] points = new Vector3[] { top, bottom, left, right };

                    // 吸附的位置
                    Vector3 temp = Vector3.zero;
                    float dis = 10000;
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (Vector3.Distance(hit.point, points[i]) < dis)
                        {
                            dis = Vector3.Distance(hit.point, points[i]);
                            temp = points[i];
                        }
                    }
                    tempBuild.transform.position = temp;
                }
                else
                {
                    // 让鼠标处有一块空地跟着跑
                    tempBuild.transform.position = hit.point;
                }

            }
            //鼠标左键 建造
            if (Input.GetMouseButtonDown(0))
            {
                if (tempBuild.CanCreate)
                {
                    GameObject temp = GameObject.Instantiate<GameObject>(tempBuildPrefab, tempBuild.transform.position, Quaternion.identity, null);
                    buildList.Add(temp.GetComponent<BaseBuild>());
                }
                else
                {
                    Debug.Log("重叠，不能创建");
                }

            }
        }
    }
}
