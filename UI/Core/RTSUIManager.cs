using UnityEngine;

public class RTSUIManager : MonoBehaviour
{
    public GameObject unitPanel;
    public GameObject buildingPanel;

    void Update()
    {
        var selected = SelectionManager.Instance.SelectedUnits;

        if (selected.Count == 0)
        {
            unitPanel.SetActive(false);
            buildingPanel.SetActive(false);
        }
        else if (selected.Count == 1)
        {
            if (selected[0].GetComponent<BuildingCore>())
            {
                buildingPanel.SetActive(true);
                unitPanel.SetActive(false);
            }
            else
            {
                unitPanel.SetActive(true);
                buildingPanel.SetActive(false);
            }
        }
        else
        {
            // мультивыделение
            unitPanel.SetActive(true);
            buildingPanel.SetActive(false);
        }
    }
}