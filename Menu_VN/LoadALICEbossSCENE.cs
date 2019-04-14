using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadALICEbossSCENE : MonoBehaviour {

	void Awake(){
		
	}
	// Use this for initialization
	public void RESTART () {
		//transição de cena
		SceneManager.LoadScene (0);
	}

}
