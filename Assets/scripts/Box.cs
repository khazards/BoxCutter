using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public Color defaultColor = Color.red;
	public Color selectedColor =  Color.green;
	public Renderer mat;

	void Start() {
		mat = GetComponent<Renderer>();
	}

    void OnTouchDown(){
		mat.material.color = selectedColor;
	}
	void OnTouchUp(){
		mat.material.color = defaultColor;
	}
	void OnTouchStay(){
		mat.material.color = selectedColor;
	}
	void OnTouchExit(){
		mat.material.color = defaultColor;
	}
}
