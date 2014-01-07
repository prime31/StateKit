using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 10f;


	void Update()
	{
		// really simple player input. just move on the x-z plane
		var input = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
		GetComponent<CharacterController>().Move( input * moveSpeed * Time.deltaTime );
	}
}
