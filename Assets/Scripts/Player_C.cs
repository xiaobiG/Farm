using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    // 空地
    private GameObject prefab_Crop_Empty;

    // 向日葵
    public GameObject Prefab_Crop_Sunflower;

    // 临时持有的空地 脚本
    private Crop_Empty crop_Empty;

    // 全部植物
    private List<GameObject> crops = new List<GameObject>();

    void Start()
    {
        prefab_Crop_Empty = Resources.Load<GameObject>("Crop_Empty");
        crop_Empty = GameObject.Instantiate<GameObject>(prefab_Crop_Empty).GetComponent<Crop_Empty>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 碰撞点地面
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
            {
                // 可以吸附的植物
                GameObject crop = null;
                // 查找 有没有很近的植物
                for (int i = 0; i < crops.Count; i++)
                {
                    // 如果小于12米
                    if (Vector3.Distance(hit.point, crops[i].transform.position) < 12)
                    {
                        crop = crops[i];
                        break;
                    }
                }
                if (crop != null)
                {
                    // 吸附
                    Vector3 top = crop.transform.position + new Vector3(0, 0, 10);
                    Vector3 bottom = crop.transform.position + new Vector3(0, 0, -10);
                    Vector3 left = crop.transform.position + new Vector3(-10, 0, 0);
                    Vector3 right = crop.transform.position + new Vector3(10, 0, 0);
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
                    crop_Empty.transform.position = temp;
                }
                else
                {
                    // 让鼠标处有一块空地跟着跑
                    crop_Empty.transform.position = hit.point;
                }

            }
            //鼠标左键 建造
            if (Input.GetMouseButtonDown(0))
            {
                if (crop_Empty.CanCreate)
                {
                    GameObject temp = GameObject.Instantiate<GameObject>(Prefab_Crop_Sunflower, crop_Empty.transform.position, Quaternion.identity, null);
                    crops.Add(temp);
                }
                else
                {
                    Debug.Log("重叠，不能创建");
                }

            }
        }
    }
}
