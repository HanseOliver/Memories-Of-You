using UnityEngine;
using System.Collections;

public class AliceMoviment : MonoBehaviour {

	//Classe controla o movimento do primeiro chefe do jogo

	public Dt dT;
	public AliceHealth aliceHP;
	
	//timer things
	public float startTime;								//when the game starts
	public float t ;									//the timer in seconds
	public float Pattern1_TIME;							//when the first pattern starts. FIRE
	public float Pattern2_TIME;	
	public float Pattern3_TIME;							//when the first pattern starts. UPSIDE

	public Vector2 UPsideSpeed;

	public GameObject shot1; 							//the shoot object. UPSIDE
	public GameObject shot2;
	public GameObject shot3;
	public GameObject UpsidePOS1;
	public GameObject UpsidePOS2;
	public GameObject UpsidePOS3;
	public GameObject UpsidePOS4;
	public GameObject UpsideLADO1;
	public GameObject UpsideLADO2;
	public GameObject UpsideLADO3;
	public GameObject UpsideLADO4;

	public GameObject fireTerrain;
	public Transform firePosition;
	public Transform firePosition1;
	public Transform firePosition2;
	public Transform firePosition3;
	public Transform firePosition4;
	public Transform firePosition5;
	public Transform firePosition6;
	public Transform firePosition7;
	public Transform firePosition8;
	public Transform firePosition9;


	public GameObject sc;								//reference to this gameobject, just to scale it
	public int curPOINTS;								//which points is the sprite floating around
	public Transform[] WayPoints = null;				//the points
	public Transform[] WayPoints2 = null;
	public Transform WayPoint3;							//the points
	public float speed;									//the speed that the sprite floats around
	public int curWaypoint;								//which point is the sprite at
	public bool Patrol = true;					
	public Vector3 Target;								//position of the target	
	public Vector2 MoveDirection;						//where the sprite has to move
	public Vector2 Velocity;							//the velocity in which it moves. It will be mutiplied by speed

	private  Vector3 alvo;
	public  GameObject position1;
	public float vel =1 ;


	void Awake(){
		//Setando tempo inicial do timer
		startTime = 1;
		//Setando pontos de referencia no timer
		Pattern1_TIME = 5.0f;
		Pattern2_TIME = 2.0f;
		Pattern3_TIME = 3.0f;

	}
	// Use this for initialization
	void Start () {
			aliceHP = gameObject.GetComponent<AliceHealth> ();

		}
	
	// Update is called once per frame
	void Update () {

		UpsidePOS1.GetComponent<Rigidbody2D>().velocity =UPsideSpeed;
		if(UpsidePOS1.transform.position.x > 80| UpsidePOS1.transform.position.x < -80 ){
			UPsideSpeed.x *=-1;
		}

		 //updating the timer
		switch (curPOINTS) {

		case 1:{
				//Setando timer
				t +=  startTime *Time.deltaTime;
				
				//Controla o fogo no chão da fase
				if (t >Pattern1_TIME && t<Pattern1_TIME + 0.03) {
					//Seta um novo ponto de referencia
					Pattern1_TIME += 20;
					//Instanciando s objetos referentes ao fogo
					Instantiate (fireTerrain, firePosition.position, firePosition.rotation);
					Instantiate (fireTerrain, firePosition1.position, firePosition1.rotation);
					Instantiate (fireTerrain, firePosition2.position, firePosition2.rotation);
					Instantiate (fireTerrain, firePosition3.position, firePosition3.rotation);
					Instantiate (fireTerrain, firePosition4.position, firePosition4.rotation);
					Instantiate (fireTerrain, firePosition5.position, firePosition5.rotation);
					Instantiate (fireTerrain, firePosition6.position, firePosition6.rotation);
					Instantiate (fireTerrain, firePosition7.position, firePosition7.rotation);
					Instantiate (fireTerrain, firePosition8.position, firePosition8.rotation);
					Instantiate (fireTerrain, firePosition9.position, firePosition9.rotation);
				}
				//controla os tiros  q vem do teto
				if (t >Pattern2_TIME && t<Pattern2_TIME + 0.08) {
					//seta novo tempo de referencia
					Pattern2_TIME += 2;
					//instancia os objetos referentes aos tiros
					Instantiate (shot1, UpsidePOS1.transform.position, UpsidePOS1.transform.rotation);
					Instantiate (shot2, UpsidePOS2.transform.position, UpsidePOS2.transform.rotation);
					Instantiate (shot1, UpsidePOS3.transform.position, UpsidePOS3.transform.rotation);
					Instantiate (shot2, UpsidePOS4.transform.position, UpsidePOS4.transform.rotation);

				}
				
				//Controla os tiros laterais
				if (t >Pattern3_TIME && t<Pattern3_TIME+00.8) {
					Pattern3_TIME += 2;
					Instantiate (shot3, UpsideLADO1.transform.position, UpsideLADO1.transform.rotation);
					Instantiate (shot3, UpsideLADO2.transform.position, UpsideLADO2.transform.rotation);

				}

				//mudando tamanho do personagem
				sc.transform.localScale = new Vector2 (50, 50);
			//Controla o movimento do chefe da fase
			if (curWaypoint < WayPoints.Length) {
				//Posição a se mover
				Target = WayPoints [curWaypoint].position;
				//setando caminho
				MoveDirection = Target - gameObject.transform.position;
				//setando velocidade
				Velocity = GetComponent <Rigidbody2D> ().velocity;

				if (MoveDirection.magnitude < 1) {
					//nova posição
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

				if(aliceHP.currentHP1 <= 25){
					curPOINTS = 2;
				}



			}break;
		//Setando os msm comandos, só q para coordenadas diferentes
		case 2:
			{
				t +=  startTime *Time.deltaTime;
				if(aliceHP.currentHP1==0 ){
					curPOINTS = 3;
				}

				if (t >Pattern1_TIME && t<Pattern1_TIME + 0.03) {
					Pattern1_TIME += 20;
					Instantiate (fireTerrain, firePosition.position, firePosition.rotation);
					Instantiate (fireTerrain, firePosition1.position, firePosition1.rotation);
					Instantiate (fireTerrain, firePosition2.position, firePosition2.rotation);
					Instantiate (fireTerrain, firePosition3.position, firePosition3.rotation);
					Instantiate (fireTerrain, firePosition4.position, firePosition4.rotation);
					Instantiate (fireTerrain, firePosition5.position, firePosition5.rotation);
					Instantiate (fireTerrain, firePosition6.position, firePosition6.rotation);
					Instantiate (fireTerrain, firePosition7.position, firePosition7.rotation);
					Instantiate (fireTerrain, firePosition8.position, firePosition8.rotation);
					Instantiate (fireTerrain, firePosition9.position, firePosition9.rotation);
				}
				if (t >Pattern2_TIME && t<Pattern2_TIME + 0.08) {
					Pattern2_TIME += 2;
					Instantiate (shot1, UpsidePOS1.transform.position, UpsidePOS1.transform.rotation);
					Instantiate (shot2, UpsidePOS2.transform.position, UpsidePOS2.transform.rotation);
					Instantiate (shot1, UpsidePOS3.transform.position, UpsidePOS3.transform.rotation);
					Instantiate (shot2, UpsidePOS4.transform.position, UpsidePOS4.transform.rotation);

				}

				if (t >Pattern3_TIME && t<Pattern3_TIME+00.8) {
					Pattern3_TIME += 5;
					Instantiate (shot3, UpsideLADO3.transform.position, UpsideLADO4.transform.rotation);
					Instantiate (shot3, UpsideLADO4.transform.position, UpsideLADO3.transform.rotation);

				}

				sc.transform.localScale = new Vector2 (-50, 50);
				if (curWaypoint < WayPoints2.Length) {
					Target = WayPoints2 [curWaypoint].position;
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
		//O chefe da fase eh derotado, e cai no chão
		case 3:
			{
				gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
				transform.Translate(Vector2.down*Time.deltaTime);
			}
			break;
		
		}

	}


}
