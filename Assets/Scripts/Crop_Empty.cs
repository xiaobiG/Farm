using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop_Empty : MonoBehaviour
{
    public bool CanCreate { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        // 如果触碰到植物 不能创建
        if (other.tag == "Crop")
        {
            CanCreate = false;
        }
        else
        {
            CanCreate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 如果从植物退出 能创建
        if (other.tag == "Crop")
        {
            CanCreate = true;
        }
    }



}
