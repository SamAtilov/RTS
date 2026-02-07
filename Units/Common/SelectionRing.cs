using UnityEngine;

public class SelectionRing : MonoBehaviour
{
    public GameObject ring;

    public void Show(Color c)
    {
        ring.SetActive(true);
        ring.GetComponent<Renderer>().material.color = c;
    }

    public void Hide()
    {
        ring.SetActive(false);
    }
}