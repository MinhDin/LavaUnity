using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    float[] points;
    [Tooltip("how many screens or pages are there within the content (steps)")]
    public int screens = 1;
    float stepSize;

    ScrollRect scroll;
    bool LerpH;
    float targetH;
    [Tooltip("Snap horizontally")]
    public bool snapInH = true;

    bool LerpV;
    float targetV;
    [Tooltip("Snap vertically")]
    public bool snapInV = true;

    int curIndex;

    Vector2 mouseDownPos;

    bool isDown;

    // Use this for initialization
    void Start()
    {
        scroll = gameObject.GetComponent<ScrollRect>();
        scroll.inertia = false;

        if (screens > 0)
        {
            points = new float[screens];
            stepSize = 1 / (float)(screens - 1);

            for (int i = 0; i < screens; i++)
            {
                points[i] = i * stepSize;
            }
        }
        else
        {
            points[0] = 0;
        }

        curIndex = FindNearest(scroll.horizontalNormalizedPosition, points);
    }

    void Update()
    {
        if (LerpH)
        {
            scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH, 50 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.horizontalNormalizedPosition, targetH)) LerpH = false;
        }
        if (LerpV)
        {
            scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV, 50 * scroll.elasticity * Time.deltaTime);
            if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV)) LerpV = false;
        }
    }

    public void DragEnd()
    {
        if(isDown)
        {
            isDown = false;
        }
        else
        {
            return;
        }
        if(((Vector2)Input.mousePosition - mouseDownPos).magnitude < (Screen.width / 10))
        {
            LerpH = true;
            return;
        }
        
        if (scroll.horizontal && snapInH)
        {
            int newIndex = FindNearest(scroll.horizontalNormalizedPosition, points);
            if(newIndex == curIndex)
            {
                float oldTarget = points[curIndex];
                float offsetPos = scroll.horizontalNormalizedPosition - oldTarget;
                
                if(Mathf.Abs(offsetPos) > stepSize / 50)
                {
                    if(scroll.horizontalNormalizedPosition - oldTarget > 0)
                    {
                        newIndex = Mathf.Min(curIndex + 1, screens - 1);
                    }
                    else
                    {
                        newIndex = Mathf.Max(curIndex - 1, 0);
                    }
                }
                //newIndex = FindNearest(offsetPos, points);
            }

            targetH = points[newIndex];
            curIndex = newIndex;
            LerpH = true;
        }
        if (scroll.vertical && snapInV)
        {
            targetH = points[FindNearest(scroll.verticalNormalizedPosition, points)];
            LerpH = true;
        }
    }

    public void OnDrag()
    {
        LerpH = false;
        LerpV = false;
    }

    public void OnDown()
    {
        mouseDownPos = Input.mousePosition;
        isDown = true;
    }

    int FindNearest(float f, float[] array)
    {
        float distance = Mathf.Infinity;
        int output = 0;
        for (int index = 0; index < array.Length; index++)
        {
            if (Mathf.Abs(array[index] - f) < distance)
            {
                distance = Mathf.Abs(array[index] - f);
                output = index;
            }
        }
        return output;
    }
}