using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {

	//A classe controla os pontos de vida do player
	
	public int startingHP = 10;
	public int currentHP;
	public Slider sliderHP;


	// Use this for initialization
	void Awake () {
		//sera o Hp atual como o hp incial
		currentHP = startingHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//essa função eh chamada pelos scripts q estão nos Gameobjets das balas
	//Ela rebece um valor int q sera convertido em dano ao player
	public void TakeDamage(int amount){
		//dano sendo recebido
		currentHP -= amount;
		//barra de vida caindo
		sliderHP.value = currentHP;
		//caso player tenha 0 de vida a cena do respectivo chefe de fase eh reiniciada
		if (currentHP <= 0) {
			if (SceneManager.GetActiveScene ().name == "NoireBoss") {
				SceneManager.LoadScene (4);

			}

			if (SceneManager.GetActiveScene ().name == "AliceBossFight") {
				SceneManager.LoadScene (2);
			}

		



		}
		
	}
}
