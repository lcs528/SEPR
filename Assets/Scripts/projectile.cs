using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class projectile : MonoBehaviour {

	/// <summary>
	/// The damage.
	/// </summary>
	public int damage;

	/// <summary>
	/// The explosion prefab instantiated on collision with anything.
	/// </summary>
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// On collision, if its a player it alters the health. Regardless of this
	/// we make an explosion and destroy the projectile.
	/// </summary>
	/// <param name="c">C.</param>
	void OnCollisionEnter (Collision c) {
		if (c.transform.tag == "Enemy") {
			c.gameObject.GetComponent<Enemy>().changeHealth(damage);
		}
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}

}
