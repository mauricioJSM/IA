using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script auxiliar que agrega comprobaciones de colisión adicionales
// Útil para tener más de un collider en un gameObject (sin que actuen como unión de geometrias)
public class VisionController : MonoBehaviour {

	// Variables auxiliares para recordar si hay colisiones
	private bool isNearBasura = false;
	private bool isNearPared = false;

	// Los siguientes métodos verifican las posibles colisiones del gameObject (la esfera que brinda vision a la aspiradora)
	// con otros objetos tales como las basuras y paredes.
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Basura"))
			isNearBasura = true;
		if(other.gameObject.CompareTag("Pared"))
			isNearPared = true;
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.CompareTag("Basura"))
			isNearBasura = true;
		if(other.gameObject.CompareTag("Pared"))
			isNearPared = true;
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Basura"))
			isNearBasura = false;
		if(other.gameObject.CompareTag("Pared"))
			isNearPared = false;
	}

	// Los siguientes métodos son públicos y su intención es brindar información:
	public bool CercaBasura(){
		return isNearBasura;
	}

	public bool CercaPared(){
		return isNearPared;
	}

	public void setCercaBasura(bool value){
		isNearBasura = value;
	}

	public void setFrentePared(bool value){
		isNearPared = value;
	}

}
