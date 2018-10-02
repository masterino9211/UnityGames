using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.UIElements;

public class SpinAnimation : MonoBehaviour, IEventMsgInterface
{
	Vector3 pos;
	float speedFactor = 242f, traveled,targetDist,TOP,slider_factor,icon_height;
	bool active = false;
	GameObject gen_ref;
	UnityEngine.UI.Slider sl;

	void IEventMsgInterface.setActive()
	{
		if (!active)
		{
			active = true;
		}
		else
		{
			active = false;
		}
	}
	void set_gen_ref()
	{
		string x = new String(name.Where(Char.IsDigit).ToArray());
		int p = Int32.Parse(x) % 10;
		gen_ref = GameObject.Find("GenObj" + p);
		pos = gen_ref.transform.position;
	}

	void Start()
	{
		sl = GameObject.Find("speed_slider").GetComponent<UnityEngine.UI.Slider>();
		sl.onValueChanged.AddListener(delegate { UpdateValue();});
		slider_factor = sl.value;

		set_gen_ref();

		icon_height = transform.GetComponent<SpriteRenderer>().sprite.rect.height / 10f;
		TOP = gen_ref.transform.position.y + 7*icon_height;
		traveled = 0f;

		targetDist = icon_height * (float)gen_ref.GetComponent<Generator>().travel_factor; 
	}
	public void UpdateValue()
	{
		slider_factor = sl.value;
	}

	void Update()
	{
		if (active)
		{
			float step = speedFactor * Time.deltaTime * slider_factor;
			traveled += step;
			transform.position = new Vector3(transform.position.x, transform.position.y + step);

			if (transform.position.y >= TOP)
			{
				transform.position = new Vector3(pos.x, pos.y + (transform.position.y - TOP));
				GameObject.Find("BigMom").GetComponent<BigMomScript>().playSpinSound.Invoke();
			}
			if (traveled >= targetDist)
			{
				active = false;
				transform.position = new Vector3(transform.position.x, transform.position.y - (traveled - targetDist));
				traveled = 0f;
				targetDist = icon_height * (float)gen_ref.GetComponent<Generator>().travel_factor;
				GameObject.Find("BigMom").GetComponent<BigMomScript>().FinishedSpining++; // Maube Event
			}
		}
	}
}
