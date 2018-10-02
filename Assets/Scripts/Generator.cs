using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IRandomiseGenerators {

	public Sprite[] icons; // Maube keep at BigMom
	const float icon_height = 13.2f;
	public int Number = 1;
	public int travel_factor;

	public void randomSpinLength()
	{
		travel_factor = Random.Range(6, 74);
	}

	void Shuffle(Sprite[] a)
	{
		for (int i = a.Length - 1; i > 0; i--)
		{
			int rnd = Random.Range(0, i);
			Sprite temp = a[i];
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}

	void Start () {

		randomSpinLength();
		icons = Resources.LoadAll<Sprite>("Icons");

		for (int i = 0; i < Random.Range(2, 5); i++)
		{
			Shuffle(icons);
		}

		for(int i = 0; i < 7; i++)
		{
			var tmp = new GameObject();

			tmp.AddComponent<SpriteRenderer>().sprite = icons[i];
			tmp.name = tmp.GetComponent<SpriteRenderer>().sprite.name +(i+1)+Number;

			tmp.AddComponent<SpinAnimation>();

			tmp.transform.SetParent(GameObject.Find("BigMom").transform);
			tmp.transform.position = new Vector3(transform.position.x, transform.position.y + icon_height * (float)i, 0);
			tmp.transform.localScale += new Vector3(9f, 9f,-1f);

			tmp.AddComponent<BoxCollider>();
		}
	}
	void Update () {
	}
}
