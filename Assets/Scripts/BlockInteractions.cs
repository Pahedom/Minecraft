using UnityEngine;

public class BlockInteractions : Debuggable
{
    public Camera playerCamera;

    public float maxDistance = 5f;

    public Block blockToCreate;

    public BlockPool pool;

    private Block _currentSelected = null;
    private Vector3 _selectedFace = new Vector3();

    private void Update()
    {
        SetSelectedBlock();
    }

    private void SetSelectedBlock()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Block target = null;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.transform.GetComponentInParent<Block>();
        }
        else
        {
            DebugLog("Couldn't select: nothing to select");
            Deselect(_currentSelected);
            return;
        }

        if (target == null)
        {
            DebugLog("Couldn't select: object isn't a block");
            Deselect(_currentSelected);
            return;
        }
        
        if (transform.DistanceTo(target.transform) > maxDistance)
        {
            DebugLog("Couldn't select " + target.displayName + ": object is too far");
            Deselect(_currentSelected);
            return;
        }

        SetSelectedFace(hit);

        if (target == _currentSelected)
        {
            DebugLog("Couldn't select " + target.displayName + ": object is already selected");
            return;
        }

        Deselect(_currentSelected);
        Select(target);
        DebugLog("Selected: " + target.displayName);
    }

    private void SetSelectedFace(RaycastHit hit)
    {
        Bounds bounds = hit.transform.GetComponent<Collider>().bounds;
        Vector3 point = hit.point;

        if (point.x == bounds.min.x)
        {
            _selectedFace = Vector3.left;
        }
        else if (point.x == bounds.max.x)
        {
            _selectedFace = Vector3.right;
        }
        else if (point.y == bounds.min.y)
        {
            _selectedFace = Vector3.down;
        }
        else if (point.y == bounds.max.y)
        {
            _selectedFace = Vector3.up;
        }
        else if (point.z == bounds.min.z)
        {
            _selectedFace = Vector3.back;
        }
        else if (point.z == bounds.max.z)
        {
            _selectedFace = Vector3.forward;
        }
        else
        {
            DebugLog("Couldn't select face");
            _selectedFace = Vector3.zero;
        }
    }

    private void Select(Block block)
    {
        if (block == null)
        {
            return;
        }

        block.SetSelected(true);

        _currentSelected = block;
    }

    private void Deselect(Block block)
    {
        if (block == null)
        {
            return;
        }

        block.SetSelected(false);

        _currentSelected = null;
    }

    public void Destroy()
    {
        if (_currentSelected == null)
        {
            DebugLog("Couldn't destroy: no object selected");
            return;
        }

        pool.Dispawn(_currentSelected);
        DebugLog("Destroyed: " + _currentSelected.displayName);
        _currentSelected = null;
    }

    public void Create()
    {
        if (_currentSelected == null)
        {
            DebugLog("Couldn't create: no object selected");
            return;
        }
        if (blockToCreate == null)
        {
            DebugLog("Couldn't create: block to create is null");
            return;
        }

        Vector3 newPosition = _currentSelected.transform.position + _selectedFace;
        newPosition.Round(1);

        if (Physics.CheckBox(newPosition, Vector3.one * 0.499f))
        {
            DebugLog("Couldn't create: obstructed");
            return;
        }

        Block newBlock = pool.Spawn(blockToCreate);
        newBlock.player = transform;
        newBlock.Place(newPosition);
        DebugLog("Created: " + newBlock.name);
    }
}