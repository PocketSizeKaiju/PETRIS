using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	//Grupos
	public GameObject[] groups;

	//Colores
	/*Color[] colors = {
		Color.blue,
		Color.magenta,
		Color.red,
		Color.green,
		Color.cyan};*/
	
	void Start () {
		spawnNext (); //Invoca el primer bichito
	}

	void Update () {
	
	}

	public void spawnNext(){
		//Indice aleatorio
		int i = Random.Range (0, groups.Length);
		//Invoca un grupo en la posicion actual
		GameObject grupo = (GameObject) Instantiate (groups [i], 
			transform.position, //Esta es la posicion del spawner
			Quaternion.identity); //Y esto es la rotacion default del grupete
		//grupo.GetComponent<SpriteRenderer>().color = colors[Random.Range (0, 5)];
	}
}
