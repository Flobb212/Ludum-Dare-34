using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform location;

    Rigidbody2D m_Rigidbody;
	Vector2 maintainedSpeed;
	Vector2 direction;
    float speed;
    float boostValue = 1.2f;
	bool isBoosting = false;
	public bool ableToBoost = true;
    Player thePlayer;
	
	void Start ()
    {
        thePlayer = GetComponent<Player>();
        maintainedSpeed = (transform.up);
        m_Rigidbody = this.GetComponent<Rigidbody2D>();
	}

    void Update()
    { 
        speed = 24f / thePlayer.GetCurrentMass();

        if (speed < 0.75f)
            speed = 0.75f;

		m_Rigidbody.angularDrag = 10000f;
		isBoosting = false;

        direction = location.transform.position - transform.position;

        transform.position += (UnityEngine.Vector3)direction.normalized * speed * Time.deltaTime;
		m_Rigidbody.velocity = Vector2.zero;

		if (!isBoosting)
			m_Rigidbody.angularDrag = 0.05f;

        if (((Input.GetAxisRaw("RotateRight") == 1) && (Input.GetAxisRaw("RotateLeft")) == 1) || (Input.GetAxisRaw("LeftTrigger") == 1 && Input.GetAxisRaw("RightTrigger") == 1))
        {
			if(ableToBoost)
			{
            	Boost();                
			}
        }

        if (Input.GetAxisRaw("RotateRight") == 1 || Input.GetAxisRaw("RightTrigger") == 1)
        {
            this.transform.Rotate(0, 0, -2);

            if (Input.GetAxisRaw("RotateLeft") == 1 || Input.GetAxisRaw("LeftTrigger") == 1)
            {
				if(ableToBoost)
				{
					Boost();
				}
            }
        }

        if (Input.GetAxisRaw("RotateLeft") == 1 || Input.GetAxisRaw("LeftTrigger") == 1)
        {
            this.transform.Rotate(0, 0, 2);

            if (Input.GetAxisRaw("RotateRight") == 1 || Input.GetAxisRaw("RightTrigger") == 1)
            {
				if(ableToBoost)
				{
					Boost();
				}
            }
        }
    }
	

    void Boost()
    {
		isBoosting = true;
		m_Rigidbody.angularDrag = 10000f;
        float originalSpeed = speed;
        do
        {
            speed++;
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        }
        while (speed < originalSpeed * 1.2);
        speed = originalSpeed;
    }   

	public void SetSpeed(float value) {
		speed = value;
	}
}
