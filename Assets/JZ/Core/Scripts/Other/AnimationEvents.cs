using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] UnityEvent event1;
    [SerializeField] UnityEvent event2;
    [SerializeField] UnityEvent event3;

    public void ActivateEvent(int _eventNumber)
    {
        switch(_eventNumber)
        {
            case 1:
                event1?.Invoke();
                break;
            case 2:
                event2?.Invoke();
                break;
            case 3:
                event3?.Invoke();
                break;
            default:
                Debug.Log(_eventNumber + " is an invalid input");
                break;
        }
    }
}
