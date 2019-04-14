using UnityEngine;
using System.Collections;

public class SOREN_BULLET_SHOT : MonoBehaviour {
	public AliceHealth aliceHP;
	public int speed = 250;
	
	//referencia o script q controla o Hp do chefe da fase
	//e referencia as propriedades fisicas do proprio objeto, setando uma velocidade
	void Start () {
		aliceHP = GameObject.Find ("Noire").GetComponent<AliceHealth> ();
		GetComponent <Rigidbody2D> ().velocity = new Vector2(0, speed);

	}
	
	//setando interações dos colisores
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "FUNDO" ) {
			Destroy (gameObject);
		}

		if (other.gameObject.name == "Noire") {
			Destroy (gameObject);
			aliceHP.TakeDamage (2);
		}
	}
}
