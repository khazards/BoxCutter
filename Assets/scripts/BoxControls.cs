﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoxControls : MonoBehaviour {

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject>();
	private List<GameObject> touchesOld = new List<GameObject>();
	RaycastHit hit;

	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
	
		if(Input.GetMouseButton(0)|| Input.GetMouseButtonDown(0)||Input.GetMouseButtonUp(0)){
		
		//	touchList.Copyto(touchesOld);
			touchesOld=touchList;
			touchList.Clear();
			

				Ray ray= GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

				
				if(Physics.Raycast(ray, out hit,touchInputMask)){
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
				Debug.Log("Test123");
					if(Input.GetMouseButtonDown(0)){

						recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver); 
					}
					if(Input.GetMouseButtonUp(0)){
						recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver); 
					}
					if(Input.GetMouseButton(0)){
						recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver); 
					}
			
				}
				
					
			
			
		}


#endif

		if(Input.touchCount>0){
			//touchesOld = new GameObject[touchList.Count];
			//	touchList.Copyto(touchesOld);
			touchesOld=touchList;
			touchList.Clear();

		foreach (Touch touch in Input.touches) {
				Ray ray= GetComponent<Camera>().ScreenPointToRay(touch.position);

			if(Physics.Raycast(ray, out hit,touchInputMask)){
				GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);

				if(touch.phase == TouchPhase.Began){
					recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver); 
				}
				if(touch.phase == TouchPhase.Ended){
					recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver); 
				}
				if(touch.phase == TouchPhase.Stationary){
					recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver); 
				}
				if(touch.phase == TouchPhase.Canceled){
					recipient.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver); 
				}

			}

		
		}
		foreach (GameObject g in touchesOld){
			if(!touchList.Contains(g)) {
					g.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver);
					}
	      }		


		}


	}
}
