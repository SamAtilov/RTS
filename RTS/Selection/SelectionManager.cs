using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("UI")]
    public RectTransform selectionBox;
    public Image top;
    public Image bottom;
    public Image left;
    public Image right;
    public Canvas canvas;

    [Header("Settings")]
    public float clickThreshold = 5f;
    public LayerMask groundMask;

    bool isDragging;
    Vector3 worldStartPoint;

    readonly List<Selectable> selected = new();

    public static SelectionManager Instance { get; private set; }
    public IReadOnlyList<Selectable> SelectedUnits => selected;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selectionBox.gameObject.SetActive(false);
        selectionBox.sizeDelta = Vector2.zero;
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSelection();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishSelection();
        }
    }

    // -------------------- SELECTION FLOW --------------------

    void StartSelection()
    {
        isDragging = false;
        selectionBox.gameObject.SetActive(false);
        selectionBox.sizeDelta = Vector2.zero;

        // мировая точка под курсором (для движения камеры во время выделения)
        if (!TryGetWorldPointFromMouse(out worldStartPoint))
        {
            Plane p = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (p.Raycast(ray, out float enter))
                worldStartPoint = ray.GetPoint(enter);
        }
    }

    void UpdateSelectionBox()
    {
        Vector2 currentMouse = Input.mousePosition;
        Vector2 startScreen = Camera.main.WorldToScreenPoint(worldStartPoint);

        if (!isDragging && Vector2.Distance(startScreen, currentMouse) > clickThreshold)
        {
            isDragging = true;
            selectionBox.gameObject.SetActive(true);
        }

        if (!isDragging)
            return;

        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            startScreen,
            canvas.worldCamera,
            out Vector2 startLocal
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            currentMouse,
            canvas.worldCamera,
            out Vector2 currentLocal
        );

        Vector2 min = Vector2.Min(startLocal, currentLocal);
        Vector2 max = Vector2.Max(startLocal, currentLocal);

        selectionBox.anchoredPosition = min;
        selectionBox.sizeDelta = max - min;

        DrawFrame();
    }

    void FinishSelection()
    {
        if (isDragging)
            SelectUnits();
        else
            SelectSingleUnit();

        selectionBox.gameObject.SetActive(false);
        isDragging = false;
    }

    // -------------------- FRAME --------------------

    void DrawFrame()
    {
        float t = 2f;
        Vector2 size = selectionBox.sizeDelta;

        top.rectTransform.anchoredPosition = new Vector2(0, size.y - t);
        top.rectTransform.sizeDelta = new Vector2(size.x, t);

        bottom.rectTransform.anchoredPosition = Vector2.zero;
        bottom.rectTransform.sizeDelta = new Vector2(size.x, t);

        left.rectTransform.anchoredPosition = Vector2.zero;
        left.rectTransform.sizeDelta = new Vector2(t, size.y);

        right.rectTransform.anchoredPosition = new Vector2(size.x - t, 0);
        right.rectTransform.sizeDelta = new Vector2(t, size.y);
    }

    // -------------------- SELECTION LOGIC --------------------

    void SelectUnits()
    {
        ClearSelection();

        Vector2 min = selectionBox.position;
        Vector2 max = min + selectionBox.sizeDelta;

        foreach (Selectable unit in FindObjectsOfType<Selectable>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            if (screenPos.z < 0) continue;

            if (screenPos.x >= min.x && screenPos.x <= max.x &&
                screenPos.y >= min.y && screenPos.y <= max.y)
            {
                unit.Select();
                selected.Add(unit);
            }
        }
    }

    void SelectSingleUnit()
    {
        ClearSelection();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Selectable s = hit.collider.GetComponentInParent<Selectable>();
            if (s != null)
            {
                s.Select();
                selected.Add(s);
            }
        }
    }

    void ClearSelection()
    {
        foreach (var s in selected)
            s.Deselect();

        selected.Clear();
    }

    // -------------------- UTILS --------------------

    bool TryGetWorldPointFromMouse(out Vector3 worldPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
        {
            worldPoint = hit.point;
            return true;
        }

        worldPoint = Vector3.zero;
        return false;
    }
}