using UnityEngine;
using System.Collections;

public class BacteriaAI : MonoBehaviour {

	public float distanceError = 0f;

	Transform m_transform;
	Vector3 targetLocation;
	Vector3 directionToTarget;
	float speed;
	bool arrivedAtTarget = false;

	void Start()
	{
		m_transform = transform;
		speed = GetComponent<Bacteria> ().speed;
		Initialise ();
	}

	void Initialise() 
	{
		FindNewHeading ();
	}

	void FixedUpdate()
	{
		float distToTarget = Vector2.Distance (m_transform.position, targetLocation);

		if (distToTarget <= distanceError) {
			FindNewHeading ();
		}
        
	}

	public void FindNewHeading() 
	{
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		targetLocation = GameManager.GetRandomPosition ();
		

		directionToTarget = targetLocation - m_transform.position;

		gameObject.GetComponent<Rigidbody2D> ().AddForce (directionToTarget.normalized * speed, ForceMode2D.Impulse);
	}
}
