using UnityEngine;
using System.Collections;

public class NoireBoss : MonoBehaviour {

	//public Dt dT;
	//timer things
	public float startTime;								//when the game starts
	public float t ;									//the timer in seconds
	public float Pattern1_TIME;							//when the first pattern starts. FIRE
	public float Pattern2_TIME;	
	public float Pattern3_TIME;	

	public GameObject shot1; //the shoot object. 
	public GameObject shot1_2; //the shoot object. 
	public GameObject shot2;
	public GameObject shot3;

	public Transform Pattern1transform1;
	public Transform Pattern1transform2;

	public Transform Pattern3transform1;
	public Transform Pattern3transform2;



	public GameObject sc;								//reference to this gameobject, just to scale it
	public int curPOINTS;								//which points is the sprite floating around
	public Transform[] WayPoints = null;				//the points
	public Transform[] WayPoints2 = null;
	public Transform WayPoint3;//the points
	public float speed;									//the speed that the sprite floats around
	public int curWaypoint;								//which point is the sprite at
	public bool Patrol = true;					
	public Vector3 Target;								//position of the target	
	public Vector2 MoveDirection;						//where the sprite has to move
	public Vector2 Velocity;							//the velocity in which it moves. It will be mutiplied by speed

	public float vel =1 ;



	// Use this for initialization
	void Start () {
		startTime = 1;
		Pattern1_TIME = 5.0f;
		Pattern3_TIME = 3.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{


		switch (curPOINTS) {

		case 1:
			{
				sc.GetComponent<Rigidbody2D> ().isKinematic = false;
				t += startTime * Time.deltaTime;

	
				if (t >Pattern2_TIME && t<Pattern2_TIME + 0.08) {
					Pattern2_TIME += 3;
					Instantiate (shot1, Pattern1transform1.transform.position, Pattern1transform1.transform.rotation);
					Instantiate (shot1_2, Pattern1transform2.transform.position, Pattern1transform2.transform.rotation);

				}

				if (t >Pattern3_TIME && t<Pattern3_TIME+00.8) {
					Pattern3_TIME += 3;
					Instantiate (shot3, Pattern3transform1.transform.position, Pattern3transform1.transform.rotation);
					Instantiate (shot3, Pattern3transform2.transform.position, Pattern3transform2.transform.rotation);

				}

				if (curWaypoint < WayPoints.Length) {
					Target = WayPoints [curWaypoint].position;
					MoveDirection = Target - gameObject.transform.position;
					Velocity = GetComponent <Rigidbody2D> ().velocity;

					if (MoveDirection.magnitude < 1) {

						curWaypoint++;

					} else {

						Velocity = MoveDirection.normalized * speed; 

					}

				} else {

					if (Patrol) {

						curWaypoint = 0;

					} else {

						Velocity = Vector2.zero;

					}
				}
				GetComponent <Rigidbody2D> ().velocity = Velocity;





			}
			break;
		case 4:
			{
				sc.GetComponent<Rigidbody2D> ().isKinematic = true;

			}
			break;


	
		}
	}

	public void currentHp(){

			curPOINTS = 1;

	}
}
