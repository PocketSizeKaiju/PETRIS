using UnityEngine;
using System.Collections;

public class Playfield : MonoBehaviour {
	//La cuadricula
	public static int ancho = 10;
	public static int largo = 20;
	public static Transform[,] grid = new Transform[ancho, largo];

	public static Vector2 roundVec2(Vector2 v){
		return new Vector2 (Mathf.Round (v.x), 
							Mathf.Round (v.y));
	}

	public static bool insideBorder(Vector2 pos){
		return((int)pos.x >= 0 &&
		       (int)pos.x < ancho &&
		       (int)pos.y >= 0);
	}

	public static void deleteRow(int y){
		for (int x = 0; x < ancho; ++x) {
			Destroy (grid [x, y].gameObject);
			grid [x, y] = null;
		}
	}

	public static void decreaseRow(int y){ //Baja una fila
		for (int x = 0; x < ancho; ++x) {
			if (grid [x, y] != null) {
				//Mueve cuadricula pa'bajo
				grid[x, y-1] = grid [x, y];
				grid [x, y] = null;

				//Actualiza posicion de los bloques
				grid [x, y-1].position += new Vector3(0, -1, 0);
			}
		}
	}

	public static bool isRowFull(int y){
		for (int x = 0; x < ancho; ++x)
			if (grid [x, y] == null)
				return false;
		return true;
	}

	public static void decreaseRowsAbove(int y){ //Baja todas las filas de arriba
		for (int i = y; i < largo; ++i)
			decreaseRow (i);
	}

	public static void deleteFullRows(){
		for (int y = 0; y < largo; ++y) {
			if (isRowFull (y)) {
				deleteRow (y);
				decreaseRowsAbove (y + 1);
				--y;
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
