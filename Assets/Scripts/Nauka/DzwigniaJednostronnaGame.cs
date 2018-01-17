using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaJednostronnaGame: MonoBehaviour {

	// PRIVATE VARIABLES
	private	Vector3			cube_position;
	private	Vector3			cube_rotation;
	private	Vector3			cube_scale;

	private Vector3			object_crane_sposition;
	private Vector3			object_crane_srotate;
	private	Vector3			object_line_sposition;
	private Vector3			object_line_srotate;
	private Vector3			object_cube_sposition;
	private Vector3			object_cube_srotate;

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_crane;
	public	GameObject		object_line;
	public	GameObject		object_cube;
	public	GameObject		object_triangle;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		//
	}

	// ----------------------------------------------------------------------
	public void Init() {
		object_crane_sposition	=	object_crane.transform.position;
		object_crane_srotate	=	object_crane.transform.rotation.eulerAngles;
		object_line_sposition	=	object_line.transform.position;
		object_line_srotate		=	object_line.transform.rotation.eulerAngles;
		object_cube_sposition	=	object_cube.transform.position;
		object_cube_srotate		=	object_cube.transform.rotation.eulerAngles;
	}

	// ----------------------------------------------------------------------
    void Update() {
		if ( Time.timeScale > 0 ) {
	        float verticalMove	=	Input.GetAxis("Vertical");
	        var rgbody			=	object_crane.GetComponent<Rigidbody>();
			var	position		=	object_crane.transform.position;
	
			if ( position.y < 1.0f ) {
				rgbody.velocity						=	Vector3.zero;
				rgbody.angularVelocity				=	Vector3.zero;
				object_crane.transform.position		=	new Vector3( position.x, 1.0f, 0.0f );
			} else if ( position.y > object_line.transform.localScale.x / 3 ) {
				rgbody.velocity						=	Vector3.zero;
				rgbody.angularVelocity				=	Vector3.zero;
				float	new_y						=	(object_line.transform.localScale.x / 3) + object_cube.transform.localScale.y;
				object_crane.transform.position		=	new Vector3( position.x, new_y - object_cube.transform.localScale.y, 0.0f );
			} else {
				rgbody.velocity				=	new Vector2( rgbody.velocity.x, verticalMove * 5.0f );
			}
		}
    }

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void calculate_Block( float length, float distance, float mass ) {
        var lineScale = object_line.transform.localScale;
        var triangleScale = object_triangle.transform.localScale;
        var cubeScale = object_cube.transform.localScale;
        var craneScale = object_crane.transform.localScale;
        var trianglePosition = object_triangle.transform.position;
        var cubePosition = object_cube.transform.position;
        var cranePosition = object_crane.transform.position;

        object_line.transform.localScale    =   new Vector3(length, lineScale.y, lineScale.z);
        object_triangle.transform.position   =   new Vector3(length/2 * triangleScale.x - (triangleScale.x/2), trianglePosition.y, 0.0f);
        object_cube.transform.position = new Vector3(length / 2 * (cubeScale.x/4) - distance, cubePosition.y, 0.0f);
        object_crane.transform.position = new Vector3(length / 2 * (-craneScale.x/2) - (-craneScale.x / 2), cranePosition.y, 0.0f);

        object_cube.GetComponent<Rigidbody>().mass = mass;
    }

	// ######################################################################
	//	OBLICZANIE ZMIENNYCH
	// ######################################################################

	public float calculateF( float length, float distance, float mass) {
		float	F1			    =	mass*9.81f;
		float	result_F2		=	(distance/length)*F1;

		return result_F2;
	}

	// ######################################################################
	//	POZYCJONOWANIE
	// ######################################################################

	public void resetPosition() {
		object_crane.transform.position		=	object_crane_sposition;
		object_crane.transform.rotation		=	Quaternion.EulerAngles( object_crane_srotate );
		object_line.transform.position		=	object_line_sposition;
		object_line.transform.rotation		=	Quaternion.EulerAngles( object_line_srotate );
		object_cube.transform.position		=	object_cube_sposition;
		object_cube.transform.rotation		=	Quaternion.EulerAngles( object_cube_srotate );
	}

    // ######################################################################

}

// ################################################################################