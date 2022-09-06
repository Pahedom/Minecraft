using UnityEngine;

public class Debuggable : MonoBehaviour
{
    [SerializeField]
    private bool _debugMode = false;

    protected void DebugLog(string message)
    {
        if (_debugMode)
        {
            Debug.Log(message);
        }
    }

    protected void DebugWarning(string message)
    {
        if (_debugMode)
        {
            Debug.LogWarning(message);
        }
    }

    protected void DebugError(string message)
    {
        if (_debugMode)
        {
            Debug.LogError(message);
        }
    }
}