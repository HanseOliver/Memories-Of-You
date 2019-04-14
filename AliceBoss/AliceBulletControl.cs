using UnityEngine;
using System.Collections;

public class AliceBulletControl : MonoBehaviour {
	public int speedH = 0;
	public int speedV = 350;
	public SorenBossMOve m;
	public PlayerHealth playerHP;
	public GameObject sc;
	public SoundManager sounds;
	//public AudioClip; 

	  void Start(){
		sounds = GameObject.Find ("SoundManager").GetComponent <SoundManager>();
		playerHP = GameObject.Find ("Soren").GetComponent<PlayerHealth> ();
		sc  = GameObject.Find("ShotAlice(Clone)");
		Physics2D.IgnoreLayerCollision (9, 10);
		m = GameObject.Find ("Soren").GetComponent <SorenBossMOve>();

		if (m.alice.curPOINTS == 1) {
			sc.transform.localScale = new Vector2 (50, 50);
			GetComponent <Rigidbody2D> ().velocity = new Vector2(0, speedV);
		}
		if (m.alice.curPOINTS == 2) {
			sc.transform.localScale = new Vector2 (-50, 50);
			GetComponent <Rigidbody2D> ().velocity = new Vector2(-0, -speedV);
		}


	}



	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.name == "FUNDO" | coll.gameObject.name == "Plataforma" | coll.gameObject.name == "Plataforma (1)" | coll.gameObject.name == "Plataforma (2)" | coll.gameObject.name == "Plataforma (3)") {
			Destroy (gameObject);
		}

		if (coll.gameObject.name == "Soren") {
			//sounds.PlaySingles ();
			Destroy (gameObject);
			playerHP.TakeDamage (1);

		}
			
	}
}
