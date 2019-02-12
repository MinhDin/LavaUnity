using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveAspectAlign : MonoBehaviour 
{
	public Sprite Target;
	public RectTransform InsideTrans;

	[Range(0, 1)]
	public float TopToBtmPercent = 1;
	float moveAmount;
	
	private void Start()
	{

		RectTransform rect = transform as RectTransform;
		if(rect == null)
		{
			return;
		}

		float aspect =  Target.bounds.size.x / Target.bounds.size.y;		
		if(InsideTrans != null)
		{
			aspect *= (InsideTrans.anchorMax.y - InsideTrans.anchorMin.y) / (InsideTrans.anchorMax.x - InsideTrans.anchorMin.x);
		}	
		float height = rect.rect.height;
		float target = rect.rect.width / aspect;
		float offset = rect.rect.height - target;

		rect.offsetMin = new Vector2(rect.offsetMin.x, rect.offsetMin.y + (offset) * (1 - TopToBtmPercent));
		rect.offsetMax = new Vector2(rect.offsetMax.x, rect.offsetMax.y + (-offset) * TopToBtmPercent);
	}
}
