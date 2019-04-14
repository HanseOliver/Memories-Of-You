using UnityEngine;
using System.Collections;

public class AliceBulletControl : MonoBehaviour {
	public int speedH = 250;
	public int speedV = 100;
	public SorenBossMOve m;
	public int speed = 250;
	public GameObject sc;


	public Vector2 difference;
	void Start(){

		m = GameObject.Find ("Soren").GetComponent <SorenBossMOve>();
		sc.transform.localScale = new Vector2 (25, 25);
		if (m.alice.curPOINTS == 1) {
			GetComponent <Rigidbody2D> ().velocity = new Vector2(speedH, -speedV);
		}
		if (m.alice.curPOINTS == 2) {
			sc.transform.localScale = new Vector2 (-25, 25);
			GetComponent <Rigidbody2D> ().velocity = new Vector2(-speedH, -speedV);
		}


	}


	/*void OnBecameInvisible() {
		// Destroy the bullet
		Destroy(gameObject);
		}*/

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.name == "FUNDO" | coll.gameObject.name == "Soren" ) {
			Destroy (gameObject);
		}
	}
}
