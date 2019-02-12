using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XInput : MonoBehaviour
{
    public static XInput Instance;
	[HideInInspector]
    public TouchPhase Phase;
	[HideInInspector]
	public bool IsTouchUI;

    private void Awake()
    {
        Instance = this;
		IsTouchUI = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            Phase = TouchPhase.Ended;
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Phase = TouchPhase.Began;
			if(EventSystem.current.IsPointerOverGameObject())
			{
				IsTouchUI = true;
			}
        }
        else if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.UpArrow))
        {
            Phase = TouchPhase.Moved;
        }
        else
        {
            Phase = TouchPhase.Canceled;
			IsTouchUI = false;
        }
    }
}
