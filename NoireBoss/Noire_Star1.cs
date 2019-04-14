using UnityEngine;
using System.Collections;

public class Noire_Star1 : MonoBehaviour {

//Classe controla um dos padrões de ataque de feche de fase

	public int speedH = 0;
	public int speedV = 350;
	public PlayerHealth php;

	//fazendo referencias necessarias
	void Start(){
		//velocidade do objeto
		GetComponent <Rigidbody2D> ().velocity = new Vector2(speedH, speedV);

		//hp do player
		php = GameObject.Find("Soren1").GetComponent<PlayerHealth>();

	}

	//setando interações entre colliders
	void OnTriggerEnter2D(Collider2D other){
			
			
			//ignora interações entre duas layers
			Physics2D.IgnoreLayerCollision (12, 14);
			Physics2D.IgnoreLayerCollision (14, 12);
			
		//da dano ao tocar o player
		if (other.gameObject.name == "Soren1") {
			Destroy (gameObject);
			php.TakeDamage(1);
		}

		//se autodestroi ao sair da tela
		if ( other.gameObject.name == "FUNDO") {
			Destroy (gameObject);
			//playerHP.TakeDamage (1);

		}
			

	}
}
