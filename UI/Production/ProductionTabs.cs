using System.Collections.Generic;
using UnityEngine;

public class ProductionTabs : MonoBehaviour
{
    public List<GameObject> infantryButtons;
    public List<GameObject> vehicleButtons;
    public List<GameObject> eliteButtons;

    public void ShowInfantry()
    {
        Show(infantryButtons);
    }

    public void ShowVehicles()
    {
        Show(vehicleButtons);
    }

    public void ShowElite()
    {
        Show(eliteButtons);
    }

    void Show(List<GameObject> list)
    {
        infantryButtons.ForEach(b => b.SetActive(false));
        vehicleButtons.ForEach(b => b.SetActive(false));
        eliteButtons.ForEach(b => b.SetActive(false));

        list.ForEach(b => b.SetActive(true));
    }
}