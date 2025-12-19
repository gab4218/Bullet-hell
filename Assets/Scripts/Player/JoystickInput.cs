using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector2 _startPos;
    [SerializeField] float _maxMagnitude = 125f;
    public Vector2 dir;

    void Start()
    {
        _startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dir = Vector2.ClampMagnitude(eventData.position - _startPos, _maxMagnitude);

        transform.position = _startPos + dir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPos;
        dir = Vector3.zero;
    }

}
