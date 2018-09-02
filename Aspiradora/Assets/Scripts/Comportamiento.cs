using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que otorga funcionalidad o comportamiento al agente.
// Utiliza la información/métodos de los script de sensores y actuadores para tomar decisiones.
public class Comportamiento : MonoBehaviour {

	private Sensores sensor;
	private Actuadores actuador;
	public float bateria; // Tiempo de vida (aproximado en segundos)
	public float maxBateria; // Capacidad máxima de la batería
	public float factorRecarga; // Múltiplo para recargar la batería en cada instante de tiempo
	public float random;
	public float num;
	public float bateriaEstado;
	public bool bandera;
    
    // Inicialización: Obtener los componentes (scripts) del sensor y actuador
    void Start(){
		sensor = GetComponent<Sensores>();
		actuador = GetComponent<Actuadores>();
		factorRecarga = factorRecarga > 1.0f ? factorRecarga : 2.0f; // Con valor >1 se asegura ganancia de batería/energía
		random = Random.Range(-10f,10f);
		num = 5.0f;
		bateriaEstado = bateria;
		bandera = false;
	}

	// En cada frame decidimos el comportamiento del agente.
	// Para este caso, tenemos un control "manual" del agente, es decir, un usuario puede controlarlo mediante
	// un control/gamepad o flechas del teclado y imprime información en consola.
	void Update(){

		
		
		if(bateria < 50.0f && !sensor.TocandoEstacion()){
			actuador.Regresar(actuador.StackX(), 0.25f, actuador.StackZ());
		}else if(!sensor.TocandoEstacion()){
			actuador.CaminoRegreso(actuador.GetX(),actuador.GetY(),actuador.GetZ());
		}
		if(sensor.TocandoEstacion() && bateria < maxBateria){
			bateria += Time.deltaTime * factorRecarga;
		}else{
			if(sensor.FrentePared()){
				if(bandera == true){
					actuador.GirarIzquierda();
				}else{
					actuador.GirarDerecha();
					//bandera = !bandera;
				}
			}

			if(sensor.TocandoPared()){
				actuador.Detener();
				actuador.MoverAtras();
				if(bandera == true){
					actuador.GirarIzquierda();
				}else{
					actuador.GirarDerecha();
					//bandera = !bandera;
				}
			}
			

			if(!sensor.CercaDeBasura()){
				actuador.MoverAdelante();
			}else{
				if(!sensor.FrenteBasura()){
					if(bandera == true){
						actuador.GirarIzquierda();
					}else{
						actuador.GirarDerecha();
						//bandera = !bandera;
					}
				}else{
					actuador.MoverAdelante();
				}
				if(sensor.TocandoBasura()){
					actuador.Aspirar(sensor.getBasura());
					sensor.setTouchBasura(false);
					sensor.setCercaBasura(false);		
				}
			}

		}
		
				

        // La batería se consume lentamente. En caso de que se termine, no realiza ninguna acción
        if (bateria <= 0)
			return;
		else
			bateria -= Time.deltaTime;
		// Recargar batería al contacto, sin rebasar límite de carga
		if(sensor.TocandoEstacion() && bateria < maxBateria)
			bateria += Time.deltaTime * factorRecarga;

		/*
		// Ejemplo de control no-automatico. Requiere de la intervención de una persona
		// Se realiza con fines demostrativos y de prueba de actuadores y sensores
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 1)
			actuador.MoverAdelante();
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") == -1)
			actuador.MoverAtras();
		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1)
			actuador.GirarDerecha();
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1)
			actuador.GirarIzquierda();
		*/

		// Imprime informacion en la consola (Debug) para verificar algunos eventos
		if(sensor.TocandoPared())
			Debug.Log("Tocando pared");
		if(sensor.TocandoBasura())
			Debug.Log("Tocando basura");
		if(sensor.CercaDeBasura())
			Debug.Log("Cerca de basura");
		if(sensor.CercaDePared())
			Debug.Log("Cerca de pared");
		if (sensor.FrenteBasura())
			Debug.Log("Frente a basura");
		if (sensor.FrentePared())
			Debug.Log("Frente a pared");


	}
}
