using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField] private float joystickRadius;
    [SerializeField] int joystickSpeed;

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector3 joystickVec;
    public bool isTouch;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;

    public bool ArrowKeysSimulationEnabled = true;

    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2;
        joystick.SetActive(false);
        joystickBG.SetActive(false);
    }

    private void Update()
    {
        if (isTouch &&
            (Input.mousePosition - joystick.transform.position).magnitude >
            joystickRadius)
        {
            joystick.transform.position = Vector3.MoveTowards(joystick.transform.position,
                Input.mousePosition, Time.deltaTime * joystickSpeed);
            joystickBG.transform.position = Vector3.MoveTowards(joystickBG.transform.position,
                Input.mousePosition, Time.deltaTime * joystickSpeed);
        }
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
        joystick.SetActive(true);
        joystickBG.SetActive(true);
        isTouch = true;
    }

    public void Drag(BaseEventData baseEventData)
    {
        if (baseEventData is PointerEventData pointerEventData)
        {
            joystick.SetActive(true);
            joystickBG.SetActive(true);
            Vector2 dragPos = pointerEventData.position;
            joystickVec = (dragPos - (Vector2)joystickBG.transform.position).normalized;
            float joystickDist = Vector2.Distance(dragPos, joystickBG.transform.position);
            if (joystickDist < joystickRadius)
            {
                joystick.transform.position = (Vector3)joystickBG.transform.position + joystickVec * joystickDist;
            }
            else
            {
                joystick.transform.position = (Vector3)joystickBG.transform.position + joystickVec * joystickRadius;
            }
        }
    }

    public void PointerUp()
    {
        joystick.SetActive(false);
        joystickBG.SetActive(false);
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
        joystickVec = Vector3.zero;
        isTouch = false;
    }

    public void HideJoystick()
    {
        joystick.SetActive(false);
        joystickBG.SetActive(false);
    }

    public float Horizontal()
    {
        if (ArrowKeysSimulationEnabled)
            return (joystickVec.x != 0) ? joystickVec.x : Input.GetAxisRaw("Horizontal");

        return joystickVec.x;
    }

    public float Vertical()
    {
        if (ArrowKeysSimulationEnabled)
            return (joystickVec.y != 0) ? joystickVec.y : Input.GetAxisRaw("Vertical");
        return joystickVec.y;
    }
}
