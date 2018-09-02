using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para que un gameObject siga a otro. Opcionalmente con una separación dada (útil para la cámara en tercera persona).
public class Follow : MonoBehaviour {

	public GameObject target; // GameObject a seguir
	public bool lookAtTarget = true; // Dirigir "mirada" hacia el objeto
	public Vector3 separacion = new Vector3(0.0f, 6.0f, -5.0f); // Separación con respecto a "target"

	void Update(){
		transform.position = target.transform.position + separacion;
		if(lookAtTarget)
			transform.LookAt(target.transform);
	}
}
