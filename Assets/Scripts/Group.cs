using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour {
	//Tiempo desde el ultimo tick de gravedad
	float lastFall = 0;

	bool isValidGridPos(){
		foreach (Transform child in transform) {
			Vector2 v = Playfield.roundVec2 (child.position);

			//No adentro de el border (?
			if (!Playfield.insideBorder (v))
				return false;

			//Bloque en cuadricula (y no parte del mismo grupo) (?
			if(Playfield.grid[(int)v.x, (int)v.y] != null &&
				Playfield.grid[(int)v.x, (int)v.y].parent != transform)
				return false;
		}
		return true;
	}

	void updateGrid() {
		//Saca los hijos viejos de la cuadricula
		for (int y = 0; y < Playfield.largo; ++y)
			for (int x = 0; x < Playfield.ancho; ++x)
				if (Playfield.grid [x, y] != null)
				if (Playfield.grid [x, y].parent == transform)
					Playfield.grid [x, y] = null;
		
		//Agrega un hijo nuevo a la cuadricula
		foreach (Transform child in transform){
			Vector2 v = Playfield.roundVec2 (child.position);
			Playfield.grid [(int)v.x, (int)v.y] = child;
		}
	}

	void Start () {
		//Posicion inicial invalida? entonces es fin del juego
		if(!isValidGridPos()) {
			Debug.Log ("GAME OVER");
			Destroy (gameObject);
		}
	}

	void Update () {
		//Mover a la izquierda
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			//Modifica posicion
			transform.position += new Vector3(-1, 0, 0);

			//checkea si es valido
			if (isValidGridPos ())
				updateGrid (); //Es un movimiento valido actualiza la cuadricula
			else
				transform.position += new Vector3 (1, 0, 0); //No es valido, va pa'tras
		}
		//Mover a la derecha
		else if(Input.GetKeyDown(KeyCode.RightArrow)){
			//Modifica posicion
			transform.position += new Vector3(1, 0, 0);

			//checkea si es valido
			if (isValidGridPos ())
				updateGrid (); //Es un movimiento valido actualiza la cuadricula
			else
				transform.position += new Vector3 (-1, 0, 0); //No es valido, va pa'tras
		}
		//Rotar
		else if(Input.GetKeyDown(KeyCode.UpArrow)){
			transform.Rotate(0, 0, -90);

			//checkea si es valido
			if (isValidGridPos ())
				updateGrid (); //Es un movimiento valido actualiza la cuadricula
			else
				transform.Rotate(0, 0, 90); //No es valido, va pa'tras
		}
		//Tirar pa'bajo y cae
		else if(Input.GetKeyDown(KeyCode.DownArrow) ||
			    Time.time - lastFall >= 1){
			//Modifica posicion
			transform.position += new Vector3(0, -1, 0);

			//checkea si es valido
			if (isValidGridPos ()){
				updateGrid (); //Es un movimiento valido actualiza la cuadricula
			} else {
				transform.position += new Vector3 (0, 1, 0); //No es valido, va pa'tras

				//Limpia lineas horizontales llenas
				Playfield.deleteFullRows();

				//Invoca siguiente grupo
				FindObjectOfType<Spawner>().spawnNext();

				//Disabilita script
				enabled = false;
			}
			lastFall = Time.time;
		}
	
	}
}
