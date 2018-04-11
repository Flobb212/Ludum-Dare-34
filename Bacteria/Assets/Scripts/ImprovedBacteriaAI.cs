using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImprovedBacteriaAI : MonoBehaviour {


	//What the AI needs to do:
	//Wander by default,
	//Seek the closest bacteria if they are in range
	//Avoid larger AI when they are closer than a smaller bacteria

	public float distanceError = 0.5f;

	GameObject closestObj;
	Transform m_transform;
	List<Transform> currTargets;
	Vector3 targetLocation;
	Vector3 directionToTarget;
	float closestDist = 1000f;
	float speed;
	float mass;
	bool seeking;
	bool avoiding;

	void Start() {
		currTargets = new List<Transform>();
		Initialise ();
	}

	void OnEnable() {
		Initialise ();
	}

	void Initialise() {
		m_transform = transform;
		speed = GetComponent<Bacteria> ().speed;
		mass = GetComponent<Bacteria> ().GetCurrentMass ();
		FindNewHeading ();
	}

	void Update() {
		avoiding = false;
		seeking = false;

		if (closestObj != null) {
			if (closestObj.GetComponent<GameEntity> ().GetCurrentMass () < mass) 
			{
				Seek ();
			} 
			else if (closestObj.GetComponent<GameEntity> ().GetCurrentMass () >= mass) 
			{
				Avoid ();
			}
			else if (closestObj.GetComponent<GameEntity> ().GetCurrentMass () == mass) 
			{
				closestObj = currTargets[Random.Range(0, currTargets.Count)].gameObject;
			}
		}
		else 
		{
			Wander ();
		}
	}

	void CheckForClosest() {
		//Check what the closest bacteria or player is
		for (int loop = 0; loop < currTargets.Count; loop++) {
			float newDist = Vector3.Distance (m_transform.position, currTargets[loop].position);
			
			if (newDist < closestDist) {
				closestDist = newDist;
				closestObj = currTargets[loop].gameObject;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) 
	{
		if (col.CompareTag ("Bacteria") || col.CompareTag ("Player")) 
		{
			if (!currTargets.Contains(col.gameObject.transform)) 
			{
				currTargets.Add(col.gameObject.transform);
				CheckForClosest();
			}
		}


	}

	void OnTriggerExit2D(Collider2D col) 
	{
		if (col.CompareTag ("Bacteria") || col.CompareTag ("Player")) 
		{
			currTargets.Remove(col.gameObject.transform);
		}
	}

	public void FindNewHeading() {
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		
		targetLocation = GameManager.GetRandomPosition ();
		
		
		directionToTarget = targetLocation - m_transform.position;
		
		gameObject.GetComponent<Rigidbody2D> ().AddForce (directionToTarget.normalized * speed, ForceMode2D.Impulse);
	}

	void Wander() {
		float distToTarget = Vector2.Distance (m_transform.position, targetLocation);

		if (!seeking || !avoiding) {
			if (distToTarget <= distanceError) 
				FindNewHeading();
		}
	}

	void Seek() {
		seeking = true;

		Vector2 steering = closestObj.transform.position - m_transform.position;

		if (steering != Vector2.zero) {
			steering.Normalize();
		}

		m_transform.position += (Vector3)steering * speed * Time.deltaTime;
	}

	void Avoid() {
		avoiding = true;

		Vector2 steering = m_transform.position - closestObj.transform.position;

		if (steering != Vector2.zero) {
			steering.Normalize();
		}

		m_transform.position += (Vector3)steering * speed * Time.deltaTime;
	}
}
