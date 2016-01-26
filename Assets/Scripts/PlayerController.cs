using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// The movement speed of the player on the ground.
	/// </summary>
	public float movementSpeed;

	/// <summary>
	/// The flight speed of the player.
	/// </summary>
	public float flightSpeed;

	/// <summary>
	/// The swim speed of the player.
	/// </summary>
	public float swimSpeed;

	/// <summary>
	/// The look sensitivity.
	/// </summary>
	public float lookSensitivity;

	/// <summary>
	/// Lock the cursor by default?
	/// </summary>
	public bool lockCursor;

	/// <summary>
	/// The maximum height of the player.
	/// </summary>
	public float maximumHeight;

	/// <summary>
	/// The maximum ascent speed of the player.
	/// </summary>
	public float maximumAscentSpeed;

	/// <summary>
	/// The normal ascent speed.
	/// </summary>
	public float ascentSpeed;

	/// <summary>
	/// The duck animator.
	/// </summary>
	public Animator duckAnim;

	/// <summary>
	/// The duck wings object.
	/// </summary>
	public GameObject duckWings;


	/// <summary>
	/// Height at which the player is considered to be flying.
	/// </summary>
	public float flightHeight = 0.25f;

	//current height of the player
	float currentHeight;

	//players rigidbody. autoassigned
	Rigidbody r;

	PlayerStates p;

	// Use this for initialization
	void Start () {
		//Assign the players rigidbody.
		r = this.GetComponent<Rigidbody> ();
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		//get easy, readable reference to playerstates.
		p = PlayerStates.instance;
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
		//pos is altered throghout this call then applied at the end.
		Vector3 pos = transform.position;

		//Handles input from the player
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			if (p.currentState == PlayerStates.State.Walking) {
				pos += transform.forward * Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime;
			} else if(p.currentState == PlayerStates.State.Flying) {
				pos += transform.forward * Input.GetAxis ("Vertical") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * Time.deltaTime;
			} else {
				pos += transform.forward * Input.GetAxis ("Vertical") * swimSpeed * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * swimSpeed * Time.deltaTime;
			}
		}
		//looking around
		if (Input.GetAxis ("Mouse X") != 0) {
			transform.RotateAround(transform.position, Vector3.up ,Input.GetAxis("Mouse X") * lookSensitivity);
		}

		//flying up
		if (Input.GetButton ("Jump") && transform.position.y <= maximumHeight && p.energy >= 0 && p.currentState != PlayerStates.State.Falling) {
			startFlying();
			pos.y +=  ascentSpeed * Time.deltaTime;
		}

		//flying down
		if (Input.GetButton ("Decend") && p.currentState == PlayerStates.State.Flying) {
			pos.y -= ascentSpeed * Time.deltaTime;
		}

		//landing
		if (distanceToGround () <= flightHeight && p.currentState == PlayerStates.State.Flying) {
			startWalking();
		}

		if (p.currentState == PlayerStates.State.Flying) {
			flying ();
		}

		if (p.currentState == PlayerStates.State.Falling) {
			flying ();
			pos.y -= 3*Time.deltaTime;
		}

		if (p.currentState == PlayerStates.State.Walking) {
			walking ();
		}

		if (p.currentState == PlayerStates.State.Swimming) {
			swimming ();
		}

		//apply transformation
		r.MovePosition (pos);
	}

	/// <summary>
	/// Distance to ground from the player.
	/// </summary>
	/// <returns>The to ground.</returns>
	public float distanceToGround () {		
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			return hit.distance;
		} else {
			return -1;
		}
	}

	/// <summary>
	/// Starts flying.
	/// </summary>
	public void startFlying () {
		r.velocity = new Vector3(0,0,0);
		r.useGravity = false;
		duckWings.SetActive(true);;
		duckAnim.Play("Flying");
		p.currentState = PlayerStates.State.Flying;
	}

	/// <summary>
	/// Called each frame the player is flying
	/// </summary>
	public void flying () {
		if(distanceToGround() <= flightHeight) {
			startWalking();
		}
		if (p.energy == 0) {
			startFalling();
		}
	}

	/// <summary>
	/// Starts falling.
	/// </summary>
	public void startFalling () {
		//duckWings.SetActive (false);
		//duckAnim.Play ("Walking");
		p.setState(PlayerStates.State.Falling);
	}

	/// <summary>
	/// Starts walking
	/// </summary>
	public void startWalking () {
		duckWings.SetActive(false);
		r.useGravity = true;
		duckAnim.Play("Walking");
		p.currentState = PlayerStates.State.Walking;
	}

	/// <summary>
	/// Called each frame we're walking
	/// </summary>
	public void walking () {
		if (transform.position.y <= 0) {
			startSwimming();
		}

	}

	/// <summary>
	/// Starts swimming
	/// </summary>
	public void startSwimming () {
		p.currentState = PlayerStates.State.Swimming;
		duckWings.SetActive (false);
		duckAnim.Play ("Walking");
	}

	/// <summary>
	/// Called each frame we're swimming.
	/// </summary>
	public void swimming () {
		if (transform.position.y >= 0 && transform.position.y <= flightHeight) {
			startWalking();
		}
	}


}
