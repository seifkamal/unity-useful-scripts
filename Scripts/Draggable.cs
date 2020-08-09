using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;

    Vector3 mOffset;
    float mZCoord;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    void OnMouseDrag()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            GetMouseAsWorldPoint() + mOffset,
            speed
        );
    }

    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
