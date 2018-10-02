using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButtonBehaviour : MonoBehaviour {

	void Start () {
	}
	void Update () {
	}
	IEnumerator AddFunds()
	{
		GameObject.Find("TextLabelBet").GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(.25f);
		GameObject.Find("TextLabelBet").GetComponent<Text>().color = new Color(1f, 0.7294f, 0.2235f, 1);
	}

	private void OnMouseDown()
	{
		transform.localScale += new Vector3(-0.8f, -0.8f);

	}
	private void OnMouseUp()
	{
		transform.localScale += new Vector3(0.8f, 0.8f);

		int n = Convert.ToInt32(GameObject.Find("TextLabelBet").GetComponent<Text>().text);

		if (n == 0)
		{
			StartCoroutine(AddFunds());
			return;
		}

		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GameObject.Find("coin_static1").GetComponent<BoxCollider2D>());
		Destroy(GameObject.Find("coin_static2").GetComponent<BoxCollider2D>());

		foreach (Transform child in GameObject.Find("BigMom").transform)
		{
			child.transform.rotation = Quaternion.Euler(0, 0, 0);
			child.transform.localScale = new Vector3(10.0f, 10.0f,1);
			Destroy(child.gameObject.GetComponent<WinSpin>());

			ExecuteEvents.Execute<IEventMsgInterface>(child.gameObject, null, (x, y) => x.setActive());
		}
		for(int i = 1; i < 6; i++)
		{
			ExecuteEvents.Execute<IRandomiseGenerators>(GameObject.Find("GenObj" + i), null, (x, y) => x.randomSpinLength());
		}
	}
}
