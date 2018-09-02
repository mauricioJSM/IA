using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
	Script que dota de acciones al agente. De manera general, realiza acciones que cambian su estado propio
	o el de su entorno/mundo. Principalmente permite que el agente tenga movimiento.
*/ 
public class Actuadores : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;
    public Stack<float> posicionX = new Stack<float>();
    public Stack<float> posicionY = new Stack<float>();
    public Stack<float> posicionZ = new Stack<float>();
    public float grados;


	// Mueve (Translate) al objeto en la direccion hacia adelante con respecto a su vector de direccion (forward)
	public void MoverAdelante(){
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

	// Mueve (Translate) al objeto en la direccion hacia atrás con respecto a su vector de direccion (forward)
	public void MoverAtras(){
		transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
    }

	// Gira (Rotate) al objeto hacia la derecha con respecto a su posicion actual
	public void GirarDerecha(){
		transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * rotationSpeed * Time.deltaTime);
    }

    //Gira el objeto hacia la derecha un numero de grados
    public void GirarDerecha(float grados){
        transform.Rotate(new Vector3(0.0f, grados, 0.0f) * rotationSpeed * Time.deltaTime);
    }

    //Gira el objeto hacia la derecha un numero de grados
    public void GirarIzquierda(float grados)
    {
        transform.Rotate(new Vector3(0.0f, -grados, 0.0f) * rotationSpeed * Time.deltaTime);
    }

    // Gira (Rotate) al objeto hacia la izquierda con respecto a su posicion actual
    public void GirarIzquierda(){
		transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * rotationSpeed * Time.deltaTime);
    }

	// Aspira, borra, elimina o deja inactivo al gameObject basura dado como parámetro
	// Recibe un gameObject que representa una basura
	public void Aspirar(GameObject basura){
		if(basura.CompareTag("Basura"))
			basura.SetActive(false);
	}

    //Detener, se detiene en cualquier movimiento que se encuentre la aspiradora
    public void Detener(){
        transform.Translate(Vector3.zero);
    }

    public float GetSpeed(){
        return this.movementSpeed;
    }

    public float GetX(){
        return transform.localPosition.x;
    }

    public float GetY(){
        return transform.localPosition.y;
    }

    public float GetZ(){
        return transform.localPosition.z;
    }
    
    public void CaminoRegreso(float x, float y, float z){
        posicionX.Push(x);
        
        posicionZ.Push(z);
        //Debug.Log("posicion x: " + x);
        //Debug.Log("posicion y: " + y);
        //Debug.Log("posicion z: " + z);
    }

    public void Regresar(float x, float y, float z){
        transform.localPosition = new Vector3(x,y,z);
    }

    public float StackX(){
        return posicionX.Pop();
    }

    public float StackY(){
        return posicionY.Pop();
    }

    public float StackZ(){
        return posicionZ.Pop();
    }

}
