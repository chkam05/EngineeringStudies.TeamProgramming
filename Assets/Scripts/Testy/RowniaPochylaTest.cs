using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class RowniaPochylaTest : MonoBehaviour {

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
	public	GameObject		object_ghost;
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
	//	OBLICZANIE ZMIENNYCH
	// ######################################################################

	public float calculatea( float alpha_angle, float friction ) {
		float	g			=	9.81f;
		float	sinus		=	Mathf.Sin( Tools.degreeToRadian(alpha_angle) );
		float	u			=	friction;
		float	cosinus		=	Mathf.Cos( Tools.degreeToRadian(alpha_angle) );
		float	result_a	=	g * ( sinus - u * cosinus );

		return result_a;
	}

	// ----------------------------------------------------------------------
	public float calculateG( float cube_mass ) {
		float	g			=	9.81f;
		float	result_G	=	g * cube_mass;

		return result_G;
	}

	// ######################################################################
	//	RYSOWANIE SIŁ
	// ######################################################################

	public void setGhost( float angle ) {
		var			inclided_plane				=	object_inclided_plane.transform.GetChild(0).gameObject;
		var			collider_points				=	object_inclided_plane.GetComponent<PolygonCollider2D>().points;

		float		center_point				=	collider_points[2].y - collider_points[0].y;
		float		posX						=	1.0f;
		float		posY						=	center_point / scale_diff + (1.0f * scale_diff);

		object_ghost.transform.position			=	new Vector3( posX, posY, 0.0f );
		object_ghost.transform.rotation			=	Quaternion.EulerAngles( new Vector3( 0.0f, 0.0f, Tools.degreeToRadian(angle) ) );
		object_ghost.transform.GetChild(1).GetComponent<LineRenderer>().SetPosition( 0, new Vector3( posX, posY, -1.1f) );
		object_ghost.transform.GetChild(1).GetComponent<LineRenderer>().SetPosition( 1, new Vector3( posX, posY - 4.0f, -1.1f) );
	}

	// ----------------------------------------------------------------------
	public void showGhost() {
		object_ghost.SetActive( true );
	}

	// ----------------------------------------------------------------------
	public void hideGhost() {
		object_ghost.SetActive( false );
	}

	// ######################################################################
	//	CUBE DATA
	// ######################################################################

	public void importData( float alpha_angle, float base_a, float cube_mass, float friction ) {
		calculateInclidedPlane( base_a, alpha_angle );
		object_cube.GetComponent<Rigidbody2D>().mass	=	cube_mass;
		object_inclided_plane.GetComponent<PolygonCollider2D>().sharedMaterial.friction		=	friction;
		prepareCubeData();
	}

	// ----------------------------------------------------------------------
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