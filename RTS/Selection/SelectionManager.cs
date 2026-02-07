using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public RectTransform selectionBox;
    public Image top, bottom, left, right;
    public float clickThreshold = 5f;

    bool isDragging;
    Vector2 startPos;
    List<Selectable> selected = new();

    public static SelectionManager Instance { get; private set; }
    public IReadOnlyList<Selectable> SelectedUnits => selected;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selectionBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;

            if (!isDragging && Vector2.Distance(startPos, currentPos) > clickThreshold)
            {
                isDragging = true;
                selectionBox.gameObject.SetActive(true);
            }

            if (isDragging)
            {
                Vector2 min = Vector2.Min(startPos, currentPos);
                Vector2 max = Vector2.Max(startPos, currentPos);

                selectionBox.position = min;
                selectionBox.sizeDelta = max - min;

                DrawFrame();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
                SelectUnits();
            else
                SelectSingleUnit();

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
        foreach (var s in selected)
            s.Deselect();
        selected.Clear();

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
        foreach (var s in selected)
            s.Deselect();
        selected.Clear();

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
}