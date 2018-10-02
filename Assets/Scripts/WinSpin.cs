using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class WinSpin : MonoBehaviour
{

	public float speed = 250f;
	public int count = 3;
	float x = 0f;
	int spin_target = 0;

	void Start ()
	{
	}
	
	void Update ()
	{

		x += Time.deltaTime * speed;
		transform.localScale += new Vector3(Mathf.Sin(2*Mathf.PI*(x / 360f))/15, Mathf.Sin(2 * Mathf.PI * (x / 360f))/15, 0);
		transform.rotation = Quaternion.Euler(0, 0, x);

		if ( x >= 360f)
		{
			x -= 360f;
			spin_target++;
		}

		if(spin_target == count)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
			transform.localScale = new Vector3(10, 10, 1);
			Destroy(GetComponent<WinSpin>());
		}	
	}
}
