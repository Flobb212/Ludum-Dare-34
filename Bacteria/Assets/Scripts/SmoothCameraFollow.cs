using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour 
{

    private Transform target;
    public float smoothing = 1.0f;
	public Player player;
	public Camera camera;
	private float massTarget = 100.0f;


    Vector3 offset;

    void Start()
    {
		target = player.transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        ////If the players Mass is over 100, call the zoom out script
        //if(player.GetCurrentMass() >= massTarget)
        //{
        //    camera.orthographicSize += 3;

        //    massTarget = massTarget * 2;
        //}
        ZoomToLevel();
    }

    void ZoomToLevel()
    {
        camera.orthographicSize =  Mathf.Lerp(camera.orthographicSize,((0.1f * player.GetCurrentMass()) + 2.0f), smoothing * Time.deltaTime);
    }
}
