using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 30f;
    public float edgeSize = 10f;

    [Header("Limits")]
    public Vector2 xLimits = new(-100, 100);
    public Vector2 zLimits = new(-100, 100);

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // ❌ Камера НЕ двигается во время drag-выделения
        if (SelectionManager.Instance != null &&
            SelectionManager.Instance.IsSelecting)
            return;

        Vector3 dir = Vector3.zero;

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // ⌨ Стрелки
        if (Input.GetKey(KeyCode.UpArrow)) dir += forward;
        if (Input.GetKey(KeyCode.DownArrow)) dir -= forward;
        if (Input.GetKey(KeyCode.LeftArrow)) dir -= right;
        if (Input.GetKey(KeyCode.RightArrow)) dir += right;

        // 🖱 Edge scroll
        Vector3 mouse = Input.mousePosition;

        if (mouse.x <= edgeSize) dir -= right;
        else if (mouse.x >= Screen.width - edgeSize) dir += right;

        if (mouse.y <= edgeSize) dir -= forward;
        else if (mouse.y >= Screen.height - edgeSize) dir += forward;

        if (dir.sqrMagnitude > 1f)
            dir.Normalize();

        Vector3 newPos = transform.position + dir * moveSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, xLimits.x, xLimits.y);
        newPos.z = Mathf.Clamp(newPos.z, zLimits.x, zLimits.y);

        transform.position = newPos;
    }
}