using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class menuCameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (new Vector3 (Mathf.Sin (Time.time) / 1500, Mathf.Cos (Time.time) / 1000, Mathf.Sin (Time.time) / 3000));
	}
}
