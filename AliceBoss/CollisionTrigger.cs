using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

	public BoxCollider2D playerCollider;
	public BoxCollider2D triggerCollider;
	public BoxCollider2D platCollider;
	BoxCollider2D slashCollider;
	
	//A classe manipula os colliders da fase,
	//Desativando o collider da parte de baixo da plataforma
	//Se o player pula do chão de baixo da plataforma visando subir em cima da msm
	
	
	//Ignora a intereção entre os dois colliders enviados a função IgnoreCollision  
	void Start () {
		Physics2D.IgnoreCollision(platCollider, triggerCollider, true);
	}
		
	//Sempre q o player interage com o collider especificado, no caso seria o da plataforma,
	//A colição entre o player e a plataforma eh ignorada
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Soren") {
			Physics2D.IgnoreCollision(platCollider, playerCollider, true);

		}
	}
	//Assim q o player sai da area do collider a interação entre ele e a plataforma deixa de ser ignorada 
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Soren") {
			Physics2D.IgnoreCollision(platCollider, playerCollider, false);
		}
	} 
}
