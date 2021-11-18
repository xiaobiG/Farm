using UnityEngine;

public class Camera_C : MonoBehaviour
{
    private Camera camera;
    // 移动速度
    private float moveSpeed = 50;
    // 相机边界
    private Vector2 borderX = new Vector2(-100, 190);
    private Vector2 borderZ = new Vector2(147, 305);


    void Start()
    {
        camera = GetComponent<Camera>();

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // 左右
        float h = Input.GetAxis("Horizontal");
        // 前后
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * Time.deltaTime * moveSpeed;
        // Shift 冲刺
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dir *= 3;
        }

        // 纠正相机坐标，使其不能超出设定边界
        if (transform.position.x > borderX.y)
        {
            transform.position = new Vector3(borderX.y, transform.position.y, transform.position.z);

        }
        else if (transform.position.x < borderX.x)
        {
            transform.position = new Vector3(borderX.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z > borderZ.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, borderZ.y);
        }
        if (transform.position.z < borderZ.x)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, borderZ.x);
        }

        // 滚轮缩放，放大
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel > 0 && camera.fieldOfView < 60)
        {
            camera.fieldOfView += mouseScrollWheel * 10 * 5;
        }
        // 滚轮向下，缩小
        else if (mouseScrollWheel < 0 && camera.fieldOfView > 10)
        {
            camera.fieldOfView -= mouseScrollWheel * -10 * 5;
        }
    }
}
