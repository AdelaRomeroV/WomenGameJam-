using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour
{
    public UnityEvent onActive;

    private void OnEnable()
    {
        onActive.Invoke();
    }
}