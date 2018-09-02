using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Script que percibe el mundo a su alrededor y brinda información al respecto.
	Brinda información (true/false) sobre qué tan cerca se encuentra el agente de otros objetos en el mundo.
*/
public class Sensores : MonoBehaviour {

	// Variables auxiliares para recordar si hay colisiones
	private bool isTouchingBasura = false;
	private bool isTouchingPared = false;
	private bool isTouchingEstacion = false;
	private bool isInFrontOfBasura = false;
	private bool isInFrontOfPared = false;
	private GameObject basura; // Referencia al objeto basura que se está tocando (null en otro caso)
	private VisionController radar; // Componente (script) externo
	public float rayDistance; // Longitud de rayo/linea para "mirar" al frente

	// Inicialización: Obtener el componente (script) del gameObject "Vision"
	void Start(){
		radar = GameObject.Find("Vision").gameObject.GetComponent<VisionController>();
	}

	// En cada frame lanzar un rayo/linea para verificar qué objeto está frente al agente
	void FixedUpdate(){
		RaycastHit raycastHit; // Auxiliar que almacena la información del contacto con el rayo
		if(Physics.Raycast(transform.position, transform.forward, out raycastHit, rayDistance)){
			if(raycastHit.collider.gameObject.CompareTag("Basura"))
				isInFrontOfBasura = true;
			if(raycastHit.collider.gameObject.CompareTag("Pared"))
				isInFrontOfPared = true;
		}else{
			isInFrontOfBasura = false;
			isInFrontOfPared = false;
		}
	}

	// En cada frame dibujar una linea de color para representar lo que el agente está viendo al frente
	void Update(){
		Debug.DrawLine(transform.position, transform.position + transform.forward * rayDistance, Color.green);
	}

	// Los siguientes métodos verifican las posibles colisiones del agente (el cubo que representa la aspiradora) 
	// con otros objetos tales como las basuras y paredes.
	// Recordar que los métodos "OnCollision" funcionan con objetos sólidos (como paredes)
	// y los métodos "OnTrigger" funcionan con objetos no-solidos (como las basuras)
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag ("Pared"))
			isTouchingPared = true;
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.CompareTag ("Pared"))
			isTouchingPared = true;
	}

	void OnCollisionExit(Collision other) {
		if (other.gameObject.CompareTag ("Pared"))
			isTouchingPared = false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Basura")) {
			isTouchingBasura = true;
			basura = other.gameObject;
		}
		if (other.gameObject.CompareTag ("Estacion"))
			isTouchingEstacion = true;
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag ("Basura")) {
			isTouchingBasura = true;
			basura = other.gameObject;
		}
		if (other.gameObject.CompareTag ("Estacion"))
			isTouchingEstacion = true;
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.CompareTag ("Basura")) {
			isTouchingBasura = false;
			basura = null;
		}
		if (other.gameObject.CompareTag ("Estacion"))
			isTouchingEstacion = false;
	}

	// Los siguientes métodos son públicos y su intención es brindar información a otros scripts 
	// (similar a métodos "get")
	public bool TocandoBasura(){
		return isTouchingBasura;
	}

	public bool TocandoPared(){
		return isTouchingPared;
	}

	public bool TocandoEstacion(){
		return isTouchingEstacion;
	}

	public bool FrenteBasura(){
		return isInFrontOfBasura;
	}

	public bool FrentePared(){
		return isInFrontOfPared;
	}

	public GameObject getBasura(){
		return basura;
	}

	// Estos últimos métodos obtienen la información pública de otros scripts (VisionController)
	public bool CercaDeBasura(){
		return radar.CercaBasura();
	}

	public bool CercaDePared(){
		return radar.CercaPared();
	}

	// Ejemplos de métodos adicionales que podrían ser de utilidad en casos específicos.
	// En este caso son métodos "setters" para las variables definidas
	public void setTouchBasura(bool value){
		isTouchingBasura = value;
	}

	public void setFrenteBasura(bool value){
		isInFrontOfBasura = value;
	}

	public void setCercaBasura(bool value){
		radar.setCercaBasura (value);
	}


}
