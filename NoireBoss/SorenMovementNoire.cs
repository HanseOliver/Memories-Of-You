using UnityEngine;
using System.Collections;

public class SorenMovementNoire : MonoBehaviour {


	public enum GameStateNoireBOSS{Jogavel, Pause};
	public GameStateNoireBOSS currentStateNoire;
	public Vector2 movement_vectorN;
	public Rigidbody2D rbodyN;
	public Animator animB;
	public GameObject playerN;
	public GameObject bullet;
	public float fireRate;
	public Transform BulletSpawner;
	private float nextFire;
	public Animator anim;

	public NoireBoss boss;

	// Use this for initialization
	void Start () {
		boss = GameObject.Find ("Noire").GetComponent<NoireBoss> ();
		rbodyN =this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentStateNoire) {

		//caso 1. Movimentação do personagem
		case GameStateNoireBOSS.Jogavel:
			{
				

					

				movement_vectorN = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

				if (movement_vectorN != Vector2.zero) {
					anim.SetBool ("isWalking", true); 
					anim.SetFloat ("input.X", movement_vectorN.x);
					anim.SetFloat ("input.Y", movement_vectorN.y);

				} else {
					anim.SetBool ("isWalking", false);
				}

				rbodyN.MovePosition (rbodyN.position + (movement_vectorN * 125) * Time.deltaTime);



				if (Input.GetKeyDown (KeyCode.Escape)) {
					SetCurrentStateNoireBOSS (GameStateNoireBOSS.Pause);
				}

				if (Input.GetButtonDown ("Jump") && Time.time > nextFire) {

					nextFire = Time.time + fireRate;
				Instantiate (bullet, BulletSpawner.position, BulletSpawner.rotation);

				} 


			}break;
		case GameStateNoireBOSS.Pause:
			{
				
				boss.curPOINTS = 4;
				if (Input.GetKeyDown (KeyCode.Escape)) {
					boss.currentHp ();
					SetCurrentStateNoireBOSS (GameStateNoireBOSS.Jogavel);
				}
			}break;
		}
	}


	public void SetCurrentStateNoireBOSS(GameStateNoireBOSS stateNoire){
		currentStateNoire = stateNoire;
	}
}
