using UnityEngine;
using System.Collections;

public class SaveLoadVars : MonoBehaviour {
	Game_Control control;

	void Start(){
		control = GameObject.Find ("GameControl").GetComponent<Game_Control> ();
	}

	public void DataSAVE1(){
		control.Save (1);
		
	}

	public void DataSAVE2(){
		control.Save (2);

	}
	public void DataSAVE3(){
		control.Save (3);

	}
	public void DataSAVE4(){
		control.Save (4);

	}
	public void DataSAVE5(){
		control.Save (5);

	}
	public void DataSAVE6(){
		control.Save (6);

	}
	public void DataSAVE7(){
		control.Save (7);

	}
	public void DataSAVE8(){
		control.Save (8);

	}	
	public void DataSAVE9(){
		control.Save (9);

	}	




	public void DataLOAD1(){
		control.Load (1);
	}
	public void DataLOAD2(){
		control.Load (2);
	}
	public void DataLOAD3(){
		control.Load (3);
	}
	public void DataLOAD4(){
		control.Load (4);
	}
	public void DataLOAD5(){
		control.Load (5);
	}
	public void DataLOAD6(){
		control.Load (6);
	}
	public void DataLOAD7(){
		control.Load (7);
	}
	public void DataLOAD8(){
		control.Load (8);
	}
	public void DataLOAD9(){
		control.Load (9);
	}

		
}
