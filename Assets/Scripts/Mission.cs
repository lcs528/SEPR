using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class Mission : MonoBehaviour {

	public bool complete = false;

	public string missionDescription;

	public string missionText;

	public string missionTag;

	public int progress;

	public int completeProgress;

	public int pointsForComplete;

	public void checkProgress () {
		if(progress >= completeProgress && complete == false) {
			complete = true;
			PlayerStates.instance.alterPoints(pointsForComplete, true);
		}
	}

}
