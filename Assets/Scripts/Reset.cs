using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

	void Start ()
	{
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
	}
	private void OnMouseUpAsButton()
	{
		transform.localScale += new Vector3(0.8f, 0.8f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
