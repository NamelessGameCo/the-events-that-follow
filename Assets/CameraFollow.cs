using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviourExtended
{

	public float followDistance;
	public Transform playerRef;

	bool xFlag = false;
	bool yFlag = false;

	float xVec = 0;
	float yVec = 0;

	//Vector3 tempNormal;


	//---------------------------------------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		CheckPlayerRef ();
	}

	//---------------------------------------------------------------------------------------

	// Update is called once per frame
	void Update ()
	{
		CheckNodeDistance ();
		ApplyCameraMotion ();

		//transform.position = Vector2.Lerp (transform.position, playerRef.position, Time.deltaTime);



	}

	//---------------------------------------------------------------------------------------

	void CheckPlayerRef ()
	{
		if (playerRef == null) {
			try {
				playerRef = GameObject.Find ("Player").GetComponent<Transform> ();
				return;
			} catch {
				print ("Camera missing player reference!");
				//Debug.LogWarning ("Camera missing player reference!");	
			}
		}
	}

	//---------------------------------------------------------------------------------------

	void CheckNodeDistance ()
	{
		//Check against x and y distance for a screen-centered, square-shaped 'dead zone' for camera movement

		//check x distance
		float xDist = Mathf.Abs (transform.position.x - playerRef.position.x);
		if (xDist >= followDistance) {
			xFlag = true;
		} else {
			xFlag = false;
		}

		//check y distance
		float yDist = Mathf.Abs (transform.position.y - playerRef.position.y);
		if (yDist >= followDistance) {
			yFlag = true;
		} else {
			yFlag = false;
		}

		//Update normalized vector pointing FROM FocusNode TO player
		//tempNormal = Vector3.Normalize(playerRef.position - transform.position);
		//print (tempNormal);

		xVec = Mathf.Sign(playerRef.position.x - transform.position.x);
		yVec = Mathf.Sign(playerRef.position.y - transform.position.y);

		print (xVec + " " + yVec);

	}

	//---------------------------------------------------------------------------------------

	void ApplyCameraMotion(){
		//Get scaled speed from player
		float tempSpeed = playerRef.GetComponent<PlayerController> ().GetScaledSpeed ();



		//X Movement
		if(xFlag){
			transform.position = new Vector2 (transform.position.x + (xVec * tempSpeed) * Time.deltaTime, transform.position.y);//(Mathf.Lerp (transform.position.x, playerRef.position.x, tempSpeed * Time.deltaTime), transform.position.y);//
		}	
		//yMovement
		if (yFlag) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + (yVec * tempSpeed) * Time.deltaTime);
		}

	}


}
