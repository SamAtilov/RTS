using UnityEngine;
using UnityEngine.UI;

public class ProductionButton : MonoBehaviour
{
    public UnitBlueprint blueprint;
    public Button button;

    public void Init(BuildingProduction production)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            //production.Enqueue(blueprint);
        });
    }
}