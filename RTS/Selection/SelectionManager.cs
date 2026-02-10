using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    [Header("UI")]
    public RectTransform selectionBox;
    public Image top, bottom, left, right;

    [Header("Settings")]
    public float clickThreshold = 5f;

    bool isDragging;
    Vector2 startMouse;
    List<Selectable> selected = new();

    public bool IsSelecting => isDragging;
    public IReadOnlyList<Selectable> SelectedUnits => selected;

    public bool IsMouseHeld { get; internal set; }

    void Awake()
    {
        Instance = this;
        selectionBox.gameObject.SetActive(false);
    }

    void UpdateSelectionBox(Vector2 start, Vector2 current)
    {
        Vector2 min = Vector2.Min(start, current);
        Vector2 max = Vector2.Max(start, current);

        selectionBox.position = min;
        selectionBox.sizeDelta = max - min;

        DrawFrame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMouse = Input.mousePosition;
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 current = Input.mousePosition;

            if (!isDragging &&
                Vector2.Distance(startMouse, current) > clickThreshold)
            {
                isDragging = true;
                selectionBox.gameObject.SetActive(true);
            }

            if (isDragging)
            {
                UpdateSelectionBox(startMouse, current);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
                SelectUnits();
            else
                SelectSingleUnit();

            isDragging = false;
            selectionBox.gameObject.SetActive(false);
        }
    }

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

    void SelectUnits()
    {
        ClearSelection();

        Vector2 min = selectionBox.position;
        Vector2 max = min + selectionBox.sizeDelta;

        foreach (Selectable unit in FindObjectsOfType<Selectable>())
        {
            Vector3 sp = Camera.main.WorldToScreenPoint(unit.transform.position);
            if (sp.z < 0) continue;

            if (sp.x >= min.x && sp.x <= max.x &&
                sp.y >= min.y && sp.y <= max.y)
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
}