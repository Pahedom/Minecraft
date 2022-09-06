using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite icon;

    public GameObject selectionMarker;

    private bool _selected = false;

    public void SetSelected(bool value)
    {
        selectionMarker.SetActive(value);
        _selected = value;
    }
}