using UnityEngine;
using System.Collections;

public class AliceBulletControl3 : MonoBehaviour {
	public int speedH = 350;
	public int speedV = 0;
	public SorenBossMOve m;
	public PlayerHealth playerHP;
	public GameObject sc;


	void Start(){
		sc  = GameObject.Find("ShotAlice3(Clone)");
		playerHP = GameObject.Find ("Soren").GetComponent<PlayerHealth> ();
		Physics2D.IgnoreLayerCollision (9, 10);
		m = GameObject.Find ("Soren").GetComponent <SorenBossMOve>();
		if (m.alice.curPOINTS == 1) {
			GetComponent <Rigidbody2D> ().velocity = new Vector2(speedH, speedV);
		}
		if (m.alice.curPOINTS == 2) {
			GetComponent <Rigidbody2D> ().velocity = new Vector2(-speedH, speedV);
		}


	}



	void OnCollisionEnter2D (Collision2D coll){

		if (coll.gameObject.name == "Shot(Clone)" | coll.gameObject.name == "Alice" |coll.gameObject.name == "FUNDO"  | coll.gameObject.name == "Plataforma" | coll.gameObject.name == "Plataforma (1)" | coll.gameObject.name == "Plataforma (2)" | coll.gameObject.name == "Plataforma (3)") {
			Destroy (gameObject);
		}

		if (coll.gameObject.name == "Soren") {
			Destroy (gameObject);
			playerHP.TakeDamage (1);

	}
}
}