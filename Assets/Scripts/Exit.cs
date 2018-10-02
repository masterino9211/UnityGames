using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	void Start ()
	{
	}
	void Update () {
		if (Input.GetKey("escape"))
			Application.Quit();
	}

	private void OnMouseDown()
	{
		transform.localScale += new Vector3(-0.8f, -0.8f);
	}

	private void OnMouseUpAsButton()
	{
		Debug.Log("Bye!");
		Application.Quit();
	}
	private void OnMouseUp()
	{
		transform.localScale += new Vector3(+0.8f, +0.8f);
	}
}
