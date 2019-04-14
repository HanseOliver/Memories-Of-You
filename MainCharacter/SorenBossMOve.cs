using UnityEngine;
using System.Collections;

public class SorenBossMOve : MonoBehaviour {
	
	public AliceMoviment alice;
	public int jumpForce;
	public int speedforce;
	public enum GameStateAliceBOSS{Jogavel, Pause};
	public GameStateAliceBOSS currentStateAlice;
	public Vector2 movement_vectorB;
	public Rigidbody2D rbodyB;
	public Animator animB;
	public GameObject playerB;
	public GameObject bullet;
	public Sprite FimDaAnim;
	public float fireRate;
	public Transform BulletSpawnerTRUE;
	public Transform BulletSpawner;
	private float nextFire;
	public GameObject scs;
	public bool isGrounded;
	public float radiuss;
	public Transform grounder;
	public LayerMask ground;
	
	// Use this for initialization
	void Start () {
		alice = GameObject.Find ("Alice").GetComponent<AliceMoviment> ();
		rbodyB =this.GetComponent<Rigidbody2D> ();
		BulletSpawnerTRUE = BulletSpawner;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		switch (currentStateAlice) {

		//caso 1. Movimentação do personagem
		case GameStateAliceBOSS.Jogavel:
			{	
				//verifica qual eh o padrão de movimento do chefe
				//ajusta a sprite do player de acordo
				if (alice.curPOINTS == 1) {
					scs.transform.localScale = new Vector2 (50, 50);
					BulletSpawnerTRUE = BulletSpawner;
				} else if (alice.curPOINTS == 2) {
					scs.transform.localScale = new Vector2 (-50, 50);
					BulletSpawnerTRUE = BulletSpawner;
				}
				
				//Variavel q verifica se o personagem esta no chão
				//mto util na hora de permitir q o player pule
				isGrounded = Physics2D.OverlapCircle (grounder.transform.position, radiuss, ground);
				
				//setando pulo
				if (Input.GetKeyDown (KeyCode.W) && isGrounded == true) {
					jumpForce = 7000;
					rbodyB.velocity = (new Vector2 (0, jumpForce * Time.fixedDeltaTime));
				}
				
				//caso personagem ainda esteja no ar
				if (isGrounded == false) {
					jumpForce = 0;
				}
				
				//setando movimento do personagem
				//e setando configurações de animação
				movement_vectorB = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

				if (Input.GetKey (KeyCode.A)) {{
					//configurações de animação
					animB.SetBool ("isWalking", true); 
					animB.SetFloat ("input_X", movement_vectorB.x);
					//movimento
					rbodyB.velocity = new Vector2 (speedforce * -1 * Time.deltaTime, rbodyB.velocity.y);

				} else if (Input.GetKey (KeyCode.D)) {
					//configurações de animação
					animB.SetBool ("isWalking", true); 
					animB.SetFloat ("input_X", movement_vectorB.x);
					///movimento
					rbodyB.velocity = new Vector2 (speedforce * Time.deltaTime, rbodyB.velocity.y);
				} else {
					//configurações de animação
					animB.SetBool ("isWalking", false);
					//movimento
					rbodyB.velocity = new Vector2 (0, rbodyB.velocity.y);

				}

				//mudança de estado na maquina de estados
				//pausa o jogo
				if (Input.GetKeyDown (KeyCode.Escape)) {
					SetCurrentStateAliceBOSS (GameStateAliceBOSS.Pause);
				}
				
				//seta de qnt em qnt tempo o player pode atirar
				//e instancia as balas
				if (Input.GetButtonDown ("Jump") && Time.time > nextFire) {
					animB.SetBool ("Atack", true);

					nextFire = Time.time + fireRate;
					Instantiate (bullet, BulletSpawnerTRUE.position, BulletSpawnerTRUE.rotation);
					
				} else {animB.SetBool ("Atack", false);}
				break;
			
			}
		}
	}


	//muda de estado
	public void SetCurrentStateAliceBOSS(GameStateAliceBOSS stateAlice){
		currentStateAlice = stateAlice;
	}

	}
