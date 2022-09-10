using UnityEngine;

public class Block : Debuggable
{
    public string displayName;

    public Sprite icon;

    public GameObject selectionMarker;

    public BlockPlacementRotation placementRotation;

    internal Transform player;

    private bool _selected = false;

    public void SetSelected(bool value)
    {
        selectionMarker.SetActive(value);
        _selected = value;
        if (value)
        {
            DebugLog("Selectd: " + displayName);
        }
        else
        {
            DebugLog("Deselectd: " + displayName);
        }
    }

    public void Place(Vector3 position, bool invertRotation)
    {
        transform.position = position;

        Vector3 rotation = GetRotation();
        if (invertRotation)
        {
            rotation.x += 180f;
            rotation.y += 180f;
        }

        transform.rotation = Quaternion.Euler(rotation);
        DebugLog($"{displayName} placed: \nPosition: {position.ToString("F1")}, Rotation: {rotation.ToString("F1")}");
    }

    private Vector3 GetRotation()
    {
        Vector3 rotation = Vector3.zero;

        if (placementRotation == BlockPlacementRotation.Random)
        {
            rotation.y = Random.Range(0, 4) * 90f;
        }
        else if (placementRotation == BlockPlacementRotation.TowardsPlayer)
        {
            Quaternion currentRotation = transform.rotation;
            transform.LookAt(player);
            rotation.y = transform.rotation.eulerAngles.y;
            transform.rotation = currentRotation;
            rotation.y.Snap(90f);
        }

        return rotation;
    }

    public enum BlockPlacementRotation
    {
        Fixed,
        Random,
        TowardsPlayer
    }
}