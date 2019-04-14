using UnityEngine;
using System.Collections;

public class Dt : MonoBehaviour {
	public PlayerHealth playerHP;
	public BoxCollider2D fireCollider;
	public SpriteRenderer fireRenderer;
	
	//A classe ajusta a funcionamento de uma mecanica de luta contra chefe
	//Controla qnt tempo havera fogo no chão da fase
	public void Start(){
		//Achando o componente q dis respeito a vida do player
		//Para haver interação entre o fogo e o player 
		playerHP = GameObject.Find ("Soren").GetComponent<PlayerHealth> ();
		//Assim q o objeto eh iniciado, se ele n for tocado, ele eh destruido apos 6 segundos
		Destroy(gameObject, 6.0f);
	}

	//Caso seja tocado
	void OnCollisionEnter2D(Collision2D other){
		//Identificando o objeto q colidiu com o collider
		if(other.gameObject.name == "Soren"){
			//Mandando pontos de dano para serem computados
			playerHP.TakeDamage (3);
			//Tirando as sprites e os collider do fogo da tela
			fireCollider.enabled = false;
			fireRenderer.enabled = false;
		}
	}
}