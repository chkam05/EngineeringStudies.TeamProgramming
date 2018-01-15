using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaJednostronnaGame: MonoBehaviour {

	// PRIVATE VARIABLES
	private	Vector3			cube_position;
	private	Vector3			cube_rotation;
	private	Vector3			cube_scale;

	private float			cube_moveX					=	1.5f;
	private float			cube_moveY					=	3.0f;
	private	float			scale_diff					=	2;

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_line;
	public	GameObject		object_cube;
	public	GameObject		object_crane;
	public	GameObject		object_triangle;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		//prepareCubeData();
		//calculateInclidedPlaneScale();
	}

    void Update()
    {
        float verticalMove = Input.GetAxis("Vertical");
        var rgbody = object_crane.GetComponent<Rigidbody2D>();
        rgbody.velocity = new Vector2(rgbody.velocity.x, verticalMove * 7.0f);
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

        object_line.transform.localScale    =   new Vector3(length, lineScale.y, 1.0f);
        object_triangle.transform.position   =   new Vector3(length/2 * triangleScale.x - (triangleScale.x/2), trianglePosition.y, 0.0f);
        object_cube.transform.position = new Vector3(length / 2 * (cubeScale.x/4) - distance, cubePosition.y, 0.0f);
        object_crane.transform.position = new Vector3(length / 2 * (-craneScale.x/2) - (-craneScale.x / 2), cranePosition.y, 0.0f);

        object_cube.GetComponent<Rigidbody2D>().mass = mass;
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

}

// ################################################################################