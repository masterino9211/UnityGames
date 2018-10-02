using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetClick : MonoBehaviour {

	public int add_ammount = 0;
	Text label_balance;
	Text label_bet;

	void Start () {
		label_balance = GameObject.Find("TextLabelBalance").GetComponent<Text>();
		label_bet = GameObject.Find("TextLabelBet").GetComponent<Text>();
	}
	void Update ()
	{
	}

	private void OnMouseDown()
	{
		transform.localScale += new Vector3(-0.8f, -0.8f);
	}

	private void OnMouseUp()
	{
		transform.localScale += new Vector3(0.8f, 0.8f);

		int n = Convert.ToInt32(label_balance.text);

		if( n == 0 && add_ammount > 0 )
		{
			return;
		}

		if ( add_ammount < 0  && Convert.ToInt32(label_bet.text) == 0)
		{
			return;
		}

		label_balance.text = (n - add_ammount).ToString();
		label_bet.text = (add_ammount + Convert.ToInt32(label_bet.text)).ToString();
	}
}
