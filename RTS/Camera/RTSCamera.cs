using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float edgeSize = 10f;

    [Header("Zoom")]
    public float zoomSpeed = 200f;
    public float minY = 10f;
    public float maxY = 40f;

    [Header("Map Bounds")]
    public float minX = 15f;
    public float maxX = 285f;
    public float minZ = 15f;
    public float maxZ = 285f;

    void Update()
    {
        Vector3 move = Vector3.zero;

        // направления камеры, проецированные на землю
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Движение по краям экрана
        if (Input.mousePosition.y > Screen.height - edgeSize)
            move += forward;
        if (Input.mousePosition.y < edgeSize)
            move -= forward;
        if (Input.mousePosition.x > Screen.width - edgeSize)
            move += right;
        if (Input.mousePosition.x < edgeSize)
            move -= right;

        Vector3 pos = transform.position;
        pos += move * moveSpeed * Time.deltaTime;

        // Зум
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * zoomSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Ограничение по карте
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}