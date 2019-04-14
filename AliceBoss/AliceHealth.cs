using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AliceHealth : MonoBehaviour {
	Game_Control control;
	public int startingHP1 = 50;
	public int currentHP1;
	public Slider sliderHP1;
	// Use this for initialization
	void Awake () {
		sliderHP1.value = startingHP1; 
		currentHP1 = startingHP1;
		control = GameObject.Find ("GameControl").GetComponent<Game_Control> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void TakeDamage(int amount){
		currentHP1 -= amount;
		sliderHP1.value = currentHP1;
		if (currentHP1 <= 0) {
			SceneManager.LoadScene (0);
			control.enabled = true;
		}

	}
}
