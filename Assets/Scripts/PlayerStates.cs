﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class PlayerStates : MonoBehaviour {

	//singleton. Access with PlayerStates.instance. ...
	public static PlayerStates inst;
	public static PlayerStates instance {
		get {
			if (inst == null) {
				inst =  FindObjectOfType(typeof (PlayerStates)) as PlayerStates;
			}
			return inst;
		}
	}


	public enum State {
		Flying, Walking, Swimming, Falling
	}

	public int health = 100;

	/// <summary>
	/// Current state of the player.
	/// </summary>
	public State currentState;

	/// <summary>
	/// Current points the player has.
	/// </summary>
	public int points;

	/// <summary>
	/// Current resources the player has.
	/// </summary>
	public int resources;

	/// <summary>
	/// Current energy the player has.
	/// </summary>
	public int energy = 50;

	/// <summary>
	/// Energy increaes rate of the player when grounded.
	/// </summary>
	public float energyIncreaseRate;

	/// <summary>
	/// Energy drain rate when flying.
	/// </summary>
	public float energyFlyingDecreaseRate;

	//private variable for handling gradual energy increase.
	float lastIncreaseTime;

	int geeseKilled = 0;
	int rabbitsKilled = 0;
	int breadCollected = 0;


	// Use this for initialization
	void Start () {
		//must be set to start the energy handling.
		lastIncreaseTime = energyIncreaseRate;

		//ensures GUI is in sync with energy.
		GUIHandler.instance.updateEnergyBar (energy);
	
	}
	
	// Update is called once per frame
	void Update () {
		gradualEnergyChange ();
		GUIHandler.instance.updateHealthBar(health);
	}

	/// <summary>
	/// Alters the points.
	/// </summary>
	/// <param name="amount">Amount to add.</param>
	/// <param name="mission">If set to <c>true</c> GUIUpdate's accodringly.</param>
	/// <param name="updateGUI">If set to <c>true</c> update GUI.</param>
	public void alterPoints(int amount, bool mission = false, bool updateGUI = true) {
		points += amount;
		if (updateGUI) {
			if(!mission) {
				GUIHandler.instance.updatePointsText (points.ToString (), "+" + amount.ToString ());
			} else {
				GUIHandler.instance.updatePointsText (points.ToString(), "+" + amount.ToString(), true);
			}
		}
	}

	/// <summary>
	/// Alters the players energy. Energy = Energy + amuount
	/// </summary>
	/// <param name="amount">Amount to add.</param>
	public void alterEnergy (int amount) {
		//energy must be between 0 and 100.
		energy = Mathf.Clamp (energy + amount, 0, 100);
	}


	public void alterHealth(int amount) {
		//health must be between 0 and 100
		health = Mathf.Clamp (health + amount, 0, 100);
		GUIHandler.instance.updateHealthBar(health);
		if (health == 0) {
			GUIHandler.instance.updateGameOver ();
			Invoke("loadMain",5f);
		}


	}

	public void alterResources(int amount) {
		resources += amount;
	}

	/// <summary>
	/// Sets the player state.
	/// </summary>
	/// <param name="st">State to change to (PlayerStates.State....)</param>
	public void setState(State st) {
		currentState = st;
		lastIncreaseTime = Time.time;
	}

	/// <summary>
	/// Handles player energy, decreases when flying and increases when grounded.
	/// </summary>
	void gradualEnergyChange () {
		//inside each if statement is an if(Time.time >= lastincreasetime).
		//This will wait until the time passes a certain point, perform some actions
		//and then move this point forwards by some amount, giving the effect of 
		//gradual / slowed increase.
		if (currentState != State.Flying) {
			if (Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyIncreaseRate;
				alterEnergy (1);
				GUIHandler.instance.updateEnergyBar (energy);
			}
		} else {
			//else assume we are swimming or walking, thus resting.
			if(Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyFlyingDecreaseRate;
				alterEnergy(-1);
				GUIHandler.instance.updateEnergyBar(energy);
			}
		}
	}

	public void loadMain(){
		Application.LoadLevel ("mainmenu");
	}

}
