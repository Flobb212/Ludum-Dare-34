using UnityEngine;
using System.Collections;
using System;

public class Player : GameEntity {

	public bool useDegen = false;

    private int bacteriaEaten = 1;
    private SpriteRenderer theRenderer;
	private GameObject collidedObject;
	private PlayerController controller;

	float timeSinceLastEat;
	float pickupTimer;
	float timer;
    float tempSpeed;
    float noEatTimer;
    float noEatMaxTime = 5.0f;
	bool hasPickup;
	bool canTakeDamage = true;
    bool canEat = true;
    ImmunityColour immuColor;
    AudioSource audio;
    public AudioClip[] audioArray;

    public ParticleSystem green;
    public ParticleSystem blue;
    public ParticleSystem red;
    private GameObject virusHit;

    private new void Start()
    {
        base.Start();
		controller = this.GetComponent<PlayerController>();
        audio = GetComponent <AudioSource>();
        theRenderer = this.GetComponent<SpriteRenderer>();
        theRenderer.color = currentColor;

		if (useDegen)
        	StartCoroutine("DegenMass");
    }

	public override void Initalise ()
	{
		base.Initalise ();
		gameObject.SetActive (true);

		transform.localScale = new Vector3(currentMass / 12f, currentMass / 12f, 1f);

	}

    IEnumerator DegenMass()
    {
        while (currentMass > 0)
        {
            currentMass -= 0.2f;
            currentMass -= (currentMass * 0.1f);

			m_transform.localScale = new Vector3 (currentMass /12.0f, currentMass / 12.0f, 1f);
            //LerpScale();
            yield return new WaitForSeconds(1.0f);
        }
    }

	void Update() {
		timeSinceLastEat += Time.deltaTime;

		if (timeSinceLastEat >= 2.0f)
		{
			ScoreManager.scoreMultiplyer -= 1;
			timeSinceLastEat = 0.0f;
		}

		if (ScoreManager.scoreMultiplyer < 1)
			ScoreManager.scoreMultiplyer = 1;

		if (bacteriaEaten >= 10) 
		{
			ScoreManager.scoreMultiplyer += 1;
			bacteriaEaten = 0;
		}

        if (currentMass < 3f)
        {
            ScoreManager.won = false;
            this.Die();
        }

		//StartCoroutine("NoEating");
		

	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		immuColor = ColorCalculator.GetDominantColour(currentColor);

		if (col.gameObject.CompareTag("Virus"))
		{
                ImmunityColour virusColor = col.gameObject.GetComponent<VirusV2>().GetColor();

                if (virusColor != immuColor)
                {
                    switch (virusColor)
                    {
                        case (ImmunityColour.BLUE):
                            ActivateEffect(PickUpType.SlowDown, 5.0f);
                            virusHit = Instantiate(blue, transform.position, transform.rotation) as GameObject;                            
                            break;
                        case (ImmunityColour.GREEN):
                            ActivateEffect(PickUpType.DontEat, 5.0f);
                            virusHit = Instantiate(green, transform.position, transform.rotation) as GameObject;
                            break;
                        case (ImmunityColour.RED):
                            ActivateEffect(PickUpType.LoseMass, 1.0f);
                            virusHit = Instantiate(red, transform.position, transform.rotation) as GameObject;
                        break;
                    }
                }
				else
				{
                    //Do nothing
				}
			col.gameObject.SetActive(false);            
		}

		if(canEat)
			CheckEating (col.collider);
	}

	protected override float CheckEating(Collider2D col)
	{
        float returnVal = base.CheckEating(col);
        if (col.CompareTag("Bacteria"))
        {
            Bacteria bacteriaInfo = col.GetComponent<Bacteria>();
            if (bacteriaInfo.GetCurrentMass() < currentMass)
            {
                ScoreManager.playerScore += (int)bacteriaInfo.GetCurrentMass() * ScoreManager.scoreMultiplyer;
                EatingAudio();
            }
        }

        return returnVal;
	}

	public void ActivateEffect (PickUpType type, float duration)
	{
		timer = 0;
		pickupTimer = duration;

		if (type == PickUpType.Immunity) 
		{
			StartCoroutine("SetImmunity");
		} 
		else if(type == PickUpType.SpeedUp)
		{
            tempSpeed = 10.0f;
			StartCoroutine("SetSpeed");
		}
        else if (type == PickUpType.SlowDown)
        {
			StartCoroutine("StopBoost");            
        }
        else if (type == PickUpType.DontEat)
        {
            StartCoroutine("NoEating");
        }
		else if (type == PickUpType.LoseMass)
		{
			LoseMass(this.currentMass * 0.05f);
		}
	}

    void EatingAudio()
    {
        if (audio.isPlaying)
        {
            return;
        }
        else
        {
            audio.clip = audioArray[UnityEngine.Random.Range(0, audioArray.Length)];
            audio.Play();                
        }
    }


	public override float GetCurrentMass ()
	{
		return currentMass;
	}

	IEnumerator StopBoost()
	{
		controller.ableToBoost = false;
		yield return new WaitForSeconds(noEatMaxTime);
		controller.ableToBoost = true;
	}

    IEnumerator NoEating()
    {
        canEat = false;
        //this.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        green.enableEmission = true;
        yield return new WaitForSeconds(noEatMaxTime);
        //this.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        green.enableEmission = false;
        canEat = true;

        green.enableEmission = true;
    }

    IEnumerator SetImmunity()
    {
        canTakeDamage = false;
        hasPickup = true;

        yield return new WaitForSeconds(pickupTimer);

        hasPickup = false;
        canTakeDamage = true;
    }

	IEnumerator SetSpeed() 
	{
        float origMass = currentMass;
        currentMass -= (currentMass * 0.05f);
        float diff = origMass - currentMass;
		hasPickup = true;
        

        yield return new WaitForSeconds(pickupTimer);
        

        hasPickup = false;
        currentMass += diff;
	}

	private void LoseMass(float amount) 
	{
		this.currentMass -= amount;
		if (currentMass < 0) 
		{
            ScoreManager.won = false;
			this.Die ();
		}
	}

    public override void Die()
    {
        GameManager.SetFailState();
    }
}
