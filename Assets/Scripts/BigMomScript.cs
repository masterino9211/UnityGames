using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class json_pack
{
	public int bet_size;
	public int[] win_ammount = new int[3];
	public string[] order;
	public int[] Line1;
	public int[] Line2;
	public int[] Line3;
}

[Serializable]
class Ser_L
{
	public List<json_pack> played_rounds;
}

[RequireComponent(typeof(AudioSource))]
public class BigMomScript : MonoBehaviour {

	public UnityEvent playSpinSound;
	public int FinishedSpining = 0;
	List<json_pack> JL;
	IDictionary<string, int>[] hits;
	AudioSource audioData;
	const float icon_height = 13.2f;

	void Start ()
	{
		JL = new List<json_pack>();

		playSpinSound = new UnityEvent();
		playSpinSound.AddListener(Play);
		audioData = GetComponent<AudioSource>();
	}
	void Play()
	{
		if (FinishedSpining < 35)
		{
			SpinSound();
		}
	}
	void SpinSound()
	{
		audioData.Play(0);
	}

	void clear_dict1(IDictionary<string, int> hits)
	{
		hits["orange"] = 0; hits["cherry"] = 0; hits["plum"] = 0; hits["bar"] = 0;
		hits["seven"] = 0; hits["diamond"] = 0; hits["lemon"] = 0;
	}

	void updateFunds(int multiplier)
	{
		int to_add = Convert.ToInt32(GameObject.Find("TextLabelBet").GetComponent<Text>().text) * multiplier ;
		string t = GameObject.Find("TextLabelBalance").GetComponent<Text>().text;
		GameObject.Find("TextLabelBalance").GetComponent<Text>().text = (Convert.ToInt32(t) + to_add).ToString();
	}

	void score_check()
	{
		float ray_pos = -11.87f;

		hits = new Dictionary<string, int>[3];
		for(int i = 0; i < 3; i++)
		{
			hits[i] = new Dictionary<string, int>();
			clear_dict1(hits[i]);
		}

		int[] win_a = new int[3];

		for(int i=0; i<3; i++)
		{
			RaycastHit[] combi = Physics.RaycastAll(new Vector3(-46f, ray_pos), new Vector3(1, 0), 75f);
			ray_pos += icon_height;

			foreach(var j in combi)
			{
				string t=new System.String(j.transform.gameObject.name.Where(Char.IsLetter).ToArray());
				hits[i][t]++;
			}
			/*
			print_dict(hits[i]);
			Debug.Log("--------------------------------------");
			*/
			foreach(var icon in hits[i])
			{
				if( icon.Value >= 3)
				{
					foreach (var j in combi)
					{
						string t = new System.String(j.transform.gameObject.name.Where(Char.IsLetter).ToArray());
						if(t == icon.Key)
						{
							j.transform.gameObject.AddComponent<WinSpin>();
						}
					}
					updateFunds(icon.Value);
					win_a[i]= Convert.ToInt32(GameObject.Find("TextLabelBet").GetComponent<Text>().text) * icon.Value;
				}
			}
		}

		json_pack J = new json_pack();
		J.bet_size = Convert.ToInt32(GameObject.Find("TextLabelBet").GetComponent<Text>().text);
		J.order = hits[0].Keys.ToArray();
		J.Line1 = hits[0].Keys.Select(key => hits[0][key]).ToArray();
		J.Line2 = hits[1].Keys.Select(key => hits[1][key]).ToArray();
		J.Line3 = hits[2].Keys.Select(key => hits[2][key]).ToArray();
		for(int i=0; i <3; i++) 
		{
			J.win_ammount[i] = win_a[i];
		}
		JL.Add(J);	
	}

	void print_dict(IDictionary<string,int> dictionary)
	{
		foreach (KeyValuePair<string,int > kvp in dictionary)
		{
			Debug.Log(String.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
		}
	}

	void Update () {
		if(FinishedSpining == 35)
		{
			score_check();

			GameObject.Find("TextLabelBet").GetComponent<Text>().text =
			(0).ToString();
			GameObject.Find("UI_Icon_Confirm").AddComponent<BoxCollider2D>();
			GameObject.Find("coin_static1").AddComponent<BoxCollider2D>();
			GameObject.Find("coin_static2").AddComponent<BoxCollider2D>();

			FinishedSpining = 0;
		}
	}

	private void OnDestroy()
	{	

		Ser_L tmp = new Ser_L();
		tmp.played_rounds = JL;
		Debug.Log(Application.dataPath + "/roundsLog.JSON");
		System.IO.File.WriteAllText(Application.dataPath+"/roundsLog.JSON", JsonUtility.ToJson(tmp));
	}
}
