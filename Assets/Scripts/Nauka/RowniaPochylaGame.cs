using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class RowniaPochylaGame : MonoBehaviour {

	// PRIVATE VARIABLES
	private	Vector3			cube_position;
	private	Vector3			cube_rotation;
	private	Vector3			cube_scale;

	private float			cube_moveX					=	1.5f;
	private float			cube_moveY					=	3.0f;
	private	float			scale_diff					=	2;

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_inclided_plane;
	public	GameObject		object_cube;
	public	GameObject		object_ground;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		prepareCubeData();
		calculateInclidedPlaneScale();
	}

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void importData( float alpha_angle, float base_a, float cube_mass ) {
		calculateInclidedPlane( base_a, alpha_angle );
		object_cube.GetComponent<Rigidbody2D>().mass	=	cube_mass;
		prepareCubeData();
	}

	// ----------------------------------------------------------------------
	private	void calculateInclidedPlaneScale() {
		var			inclided_plane				=	object_inclided_plane.transform.GetChild(0).gameObject;
		var			collider_points				=	object_inclided_plane.GetComponent<PolygonCollider2D>().points;

		Vector2		A							=	collider_points[0];
		Vector2		B							=	collider_points[1];
		Vector3		scale						=	inclided_plane.transform.localScale;

		float		a							=	Mathf.Sqrt( Mathf.Pow( B.x - A.x, 2 ) + Mathf.Pow( B.y - A.y, 2 ) );
		scale_diff								=	a / scale.z;
	}

	// ----------------------------------------------------------------------
	private void calculateInclidedPlane( float base_a, float alpha_angle ) {
		var			inclided_plane				=	object_inclided_plane.transform.GetChild(0).gameObject;
		var			collider_points				=	object_inclided_plane.GetComponent<PolygonCollider2D>().points;
		Vector3		scale						=	inclided_plane.transform.localScale;

		float		base_half					=	base_a / 2;
		collider_points[0]						=	new Vector2( -base_half, 0 );
		collider_points[1]						=	new Vector2( base_half, 0 );

		//float		sinus						=	Mathf.Sin( Tools.degreeToRadian(alpha_angle) );
		//float		cosinus						=	Mathf.Cos( Tools.degreeToRadian(alpha_angle) );
		float		tangens						=	Mathf.Tan( Tools.degreeToRadian(alpha_angle) );

		float		b							=	tangens * base_a;
		//float		bx							=	( base_a / cosinus ) * sinus;

		collider_points[2]						=	new Vector2( base_half, Mathf.FloorToInt( b ) );
		inclided_plane.transform.localScale		=	new Vector3( scale.x, b/scale_diff, base_a/scale_diff );
		object_inclided_plane.GetComponent<PolygonCollider2D>().points	=	collider_points;
		calculateCube( base_a, b );
	}

	// ----------------------------------------------------------------------
	private void calculateCube( float base_a, float base_b ) {
		var			inclided_plane				=	object_inclided_plane.transform.GetChild(0).gameObject;
		Vector3		scale						=	inclided_plane.transform.localScale;
		float		base_half					=	base_a / 2;

		object_cube.transform.position			=	new Vector3( base_half - cube_moveX, base_b + cube_moveY, 0.0f );
	}

	// ######################################################################
	//	CUBE DATA
	// ######################################################################

	public void prepareCubeData() {
		cube_position	=	object_cube.transform.position;
		cube_rotation	=	object_cube.transform.rotation.eulerAngles;
		cube_scale		=	object_cube.transform.localScale;
	}

	// ----------------------------------------------------------------------
	public void resetCubeData() {
		object_cube.GetComponent<Rigidbody2D>().velocity			=	Vector3.zero;
 		object_cube.GetComponent<Rigidbody2D>().angularVelocity		=	0.0f;
		object_cube.transform.position								=	cube_position;
		object_cube.transform.rotation								=	Quaternion.EulerAngles( cube_rotation );
		object_cube.transform.localScale							=	cube_scale;
	}

	// ######################################################################

}

// ################################################################################