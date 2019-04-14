using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class back : MonoBehaviour {

	// Use this for initialization
	public void scene1 () {
		//Carrega outra cena do jogo
		SceneManager.LoadScene ("Menu");
	}

}
