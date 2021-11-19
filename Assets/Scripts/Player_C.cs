using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    // 空地
    public GameObject Prefab_Crop_Empty;
    // 向日葵
    public GameObject Prefab_Crop_Sunflower;
    // 临时持有的空地
    private GameObject crop_Empty;

    void Start()
    {
        crop_Empty = GameObject.Instantiate<GameObject>(Prefab_Crop_Empty);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Ground"))) {
            // 碰撞点地面
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground") {
                // 让鼠标处有一块空地跟着跑
                crop_Empty.transform.position = hit.point;
            }
            if (Input.GetMouseButtonDown(0)) {
                GameObject.Instantiate<GameObject>(Prefab_Crop_Sunflower, hit.point, Quaternion.identity, null);
            }
        }
    }
}
