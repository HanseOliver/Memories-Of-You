using UnityEngine;
using System.Collections;

public class Aia : MonoBehaviour {

	void onDisable(){
		//n destroy o objeto durante as trocas de cena
		DontDestroyOnLoad (gameObject);
	}
}
