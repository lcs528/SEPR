using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class delayedDestroy : MonoBehaviour {

	public float destroyDelay;

	// Use this for initialization
	void Start () {
		Invoke ("k", destroyDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void k() {
		Destroy (this.gameObject);
	}
}
