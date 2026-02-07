using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopLeftUI : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI influenceText;
    public TextMeshProUGUI unitsText;
    public TextMeshProUGUI vehiclesText;

    FactionResources resources;

    void Start()
    {
        resources = FindObjectOfType<FactionResources>();
    }

    void Update()
    {
        //energyText.text = $"Energy: {resources.energy}";
        //influenceText.text = $"Influence: {resources.influence}";

        unitsText.text = $"Units: {CountUnits()}";
        vehiclesText.text = $"Vehicles: {CountVehicles()}";
    }

    int CountUnits()
    {
        return FindObjectsOfType<GroundUnitMovement>().Length;
    }

    int CountVehicles()
    {
        return FindObjectsOfType<TankMovement>().Length;
    }
}