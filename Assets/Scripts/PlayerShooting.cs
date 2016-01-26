using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class PlayerShooting : MonoBehaviour {

	/// <summary>
	/// The projectile.
	/// </summary>
	public GameObject projectile;

	/// <summary>
	/// The projectile speed.
	/// </summary>
	public float projectileSpeed;

	/// <summary>
	/// The fire rate.
	/// </summary>
	public float fireRate;

	/// <summary>
	/// The lasor prefab, if this weapon is enabled.
	/// </summary>
	public GameObject lasor;

	//used to track if the player is shooting too fast.
	float lastfireTime = 0;

	// Use this for initialization
	void Start () {
		lasor.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//When we shoot and havent recently shot, instantiate a projectile, give it forwards velocity, and update the GUI and the PlayerStates resources.
		if (Input.GetButton ("Fire1") && Time.time >= lastfireTime + fireRate) {
			if(PlayerStates.instance.resources != 0) {
				GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
				g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed;
				lastfireTime = Time.time;
				PlayerStates.instance.alterResources(-1);
				GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "-1", true);
			}
		}

		//WIP weapon. Shoots array of projectiles when you click E.
		if (Input.GetKey (KeyCode.E) && Time.time >= lastfireTime + fireRate) {
			for(int i = 0; i < 10; i++) {
				GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward*(10-i), transform.rotation);
				g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed+(transform.right*(5-i));
				lastfireTime = Time.time;
			}
		}

		//WIP weapon. Enables and disables the lasor object on the player.
		if (Input.GetKeyDown (KeyCode.Q)) {
			lasor.SetActive (true);
		} else if (Input.GetKeyUp (KeyCode.Q)) {
			lasor.SetActive(false);
		}
	}
}
