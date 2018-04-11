using UnityEngine;
using System.Collections;

public class Virus : GameEntity {

    private GameObject thePlayer = null;
    private ImmunityColour selfColor;

    public override void Initalise()
    {
        currentColor = startColor;
        currentMass = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetCurrentMass();

        int choice = Random.Range(1, 3);

        switch (choice)
        {
            case (1):
                selfColor = ImmunityColour.BLUE;
                break;
            case (2):
                selfColor = ImmunityColour.GREEN;
                break;
            case (3):
                selfColor = ImmunityColour.RED;
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            thePlayer = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            thePlayer = null;
        }
    }
    
    private new void Update()
    {
        if (thePlayer != null)
        {
            MoveTowards(thePlayer.transform.position);
        }
        else
        {

        Vector2 targetPos;

        do
        {
            targetPos = GameManager.GetRandomPosition();
        }
        while (Vector2.Distance(targetPos, transform.position) > GameManager.worldSizeX / 10f); // change this to balance

        MoveTowards(targetPos);
        }
    }

    private void MoveTowards(Vector2 targetPos)
    {
        Vector2 heading = targetPos - new Vector2(transform.position.x,transform.position.y); //calculate the direction as a vector

        if (heading != Vector2.zero)
        {
            heading.Normalize();//normalise heading
        }

        gameObject.GetComponent<Rigidbody2D>().AddForce(heading.normalized * speed, ForceMode2D.Impulse); //move
    }

	public override float GetCurrentMass ()
	{
		return currentMass;
	}

    public ImmunityColour GetColor()
    {
        return selfColor;
    }
}
