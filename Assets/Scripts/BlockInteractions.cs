using UnityEngine;

public class BlockInteractions : Debuggable
{
    public Camera playerCamera;

    public float maxDistance = 5f;

    public Block blockToCreate;

    public BlockPool pool;

    private Block _selectedBlock = null;
    private Vector3 _selectedFace = Vector3.zero;
    private int _selectedHalf = 0;

    private const float _errorMargin = 0.0001f;

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
            _selectedFace = Vector3.zero;
            Deselect(_selectedBlock);
            return;
        }

        if (target == null)
        {
            DebugLog("Couldn't select: object isn't a block");
            _selectedFace = Vector3.zero;
            Deselect(_selectedBlock);
            return;
        }

        if (transform.DistanceTo(hit.point) > maxDistance)
        {
            DebugLog("Couldn't select " + target.displayName + ": object is too far");
            _selectedFace = Vector3.zero;
            Deselect(_selectedBlock);
            return;
        }

        SetSelectedFace(hit, target.transform);

        if (target == _selectedBlock)
        {
            DebugLog("Couldn't select " + target.displayName + ": object is already selected");
            return;
        }

        Deselect(_selectedBlock);
        Select(target);
        DebugLog("Selected: " + target.displayName);
    }

    private void SetSelectedFace(RaycastHit hit, Transform target)
    {
        Bounds bounds = hit.transform.GetComponent<Collider>().bounds;
        Vector3 point = hit.point;
        _selectedHalf = 0;

        if (point.x.IsBetween(bounds.min.x - _errorMargin, bounds.min.x + _errorMargin))
        {
            _selectedFace = Vector3.left;
        }
        else if (point.x.IsBetween(bounds.max.x - _errorMargin, bounds.max.x + _errorMargin))
        {
            _selectedFace = Vector3.right;
        }
        else if (point.y.IsBetween(bounds.min.y - _errorMargin, bounds.min.y + _errorMargin))
        {
            _selectedFace = Vector3.down;
        }
        else if (point.y.IsBetween(bounds.max.y - _errorMargin, bounds.max.y + _errorMargin))
        {
            _selectedFace = Vector3.up;
        }
        else if (point.z.IsBetween(bounds.min.z - _errorMargin, bounds.min.z + _errorMargin))
        {
            _selectedFace = Vector3.back;
        }
        else if (point.z.IsBetween(bounds.max.z - _errorMargin, bounds.max.z + _errorMargin))
        {
            _selectedFace = Vector3.forward;
        }
        else
        {
            DebugError("Couldn't select face");
            _selectedFace = Vector3.zero;
        }

        if (_selectedFace.x != 0f || _selectedFace.z != 0f)
        {
            _selectedHalf = point.y > target.position.y ? 1 : -1;
        }
    }

    private void Select(Block block)
    {
        if (block == null)
        {
            return;
        }

        block.SetSelected(true);

        _selectedBlock = block;
    }

    private void Deselect(Block block)
    {
        if (block == null)
        {
            return;
        }

        block.SetSelected(false);

        _selectedBlock = null;
    }

    public void Destroy()
    {
        if (_selectedBlock == null)
        {
            DebugLog("Couldn't destroy: no object selected");
            return;
        }

        pool.Dispawn(_selectedBlock);
        DebugLog("Destroyed: " + _selectedBlock.displayName);
        _selectedBlock = null;
    }

    public void Create()
    {
        if (_selectedBlock == null)
        {
            DebugLog("Couldn't create: no object selected");
            return;
        }
        if (blockToCreate == null)
        {
            DebugLog("Couldn't create: block to create is null");
            return;
        }

        Vector3 newPosition = _selectedBlock.transform.position + _selectedFace;
        newPosition.Round(1);

        if (Physics.CheckBox(newPosition, Vector3.one * 0.499f))
        {
            DebugLog("Couldn't create: obstructed");
            return;
        }

        Block newBlock = pool.Spawn(blockToCreate);
        newBlock.player = transform;
        newBlock.Place(newPosition, _selectedHalf == 1);
        DebugLog("Created: " + newBlock.name);
    }
}