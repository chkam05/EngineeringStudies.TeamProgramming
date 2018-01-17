using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaDwustronna2Game: MonoBehaviour {

	// PRIVATE VARIABLES
	private	Vector3			cube_position;
	private	Vector3			cube_rotation;
	private	Vector3			cube_scale;

	private	float			timer							=	0.0f;

	private	Vector3			object_line_sposition;
	private Vector3			object_line_srotate;
	private Vector3			object_cube1_sposition;
	private Vector3			object_cube1_srotate;
	private Vector3			object_cube2_sposition;
	private Vector3			object_cube2_srotate;
	private Vector3			object_triangle_sposition;
	private Vector3			object_triangle_srotate;

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_line;
	public	GameObject		object_cube1;
	public	GameObject		object_cube2;
	public	GameObject		object_triangle;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start() {
		object_cube2.GetComponent<CubeDetectCollision>().setTimeRelease( 50.0f );
	}

	// ----------------------------------------------------------------------
	public void Init() {
		object_triangle_sposition	=	object_triangle.transform.position;
		object_triangle_srotate		=	object_triangle.transform.rotation.eulerAngles;
		object_line_sposition		=	object_line.transform.position;
		object_line_srotate			=	object_line.transform.rotation.eulerAngles;
		object_cube1_sposition		=	object_cube1.transform.position;
		object_cube1_srotate		=	object_cube1.transform.rotation.eulerAngles;
		object_cube2_sposition		=	object_cube2.transform.position;
		object_cube2_srotate		=	object_cube2.transform.rotation.eulerAngles;
	}

	// ----------------------------------------------------------------------
    void Update() {
        calculateF();
		if ( object_cube2.GetComponent<CubeDetectCollision>().getCollision() == object_line ) {
			object_cube2.SetActive( false );
		}
    }

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void calculate_Block( float length, float traingle, float m1, float d1, float d2 ) {
		var trianglePosition = object_triangle.transform.position;
        var triangleScale = object_triangle.transform.localScale;
        object_triangle.transform.position = new Vector3( traingle, trianglePosition.y, 0.0f);

        var lineScale = object_line.transform.localScale;
        object_line.transform.localScale = new Vector3(length, lineScale.y, 2.0f);
        
        var cubeAScale = object_cube1.transform.localScale;
        var cubeAPosition = object_cube1.transform.position;
        object_cube1.transform.position = new Vector3( -d1 * (cubeAScale.x / 2) - traingle, cubeAPosition.y, 0.0f);
        object_cube1.GetComponent<Rigidbody>().mass = m1;

        var cubeBScale = object_cube2.transform.localScale;
        var cubeBPosition = object_cube2.transform.position;
        object_cube2.transform.position = new Vector3(d2 * (cubeBScale.x / 2) + traingle, cubeBPosition.y, 0.0f);
    }

	// ######################################################################
	//	OBLICZANIE ZMIENNYCH
	// ######################################################################

	public void calculateF() {
        object[]    data    =   module_ui.GetComponent<DzwigniaDwustronna2UI>().getData();
        var cubeAScale      =   object_cube1.transform.localScale;
        var cubeBScale      =   object_cube2.transform.localScale;

        float       m1      =   (float) data[2];
        float       d1      =   -object_cube1.transform.position.x / (cubeAScale.x / 2) + object_triangle.transform.position.x;
        float       d2      =   object_cube2.transform.position.x / (cubeBScale.x / 2) - object_triangle.transform.position.x;

        float f1 = 0.0f;
        float f2 = 0.0f;

        f1 = m1 * 9.81f;
        f2 = (f1 * d1) / d2;
        float m2 = f2 / 9.81f;

        object_cube2.GetComponent<Rigidbody>().mass = m2*2;
        module_ui.GetComponent<DzwigniaDwustronna2UI>().updateData( f1, f2, d1, d2 );
    }

    // ######################################################################
	//	POZYCJONOWANIE
	// ######################################################################

	public void resetTier() {
		this.timer	=	0.0f;
	}

	public void resetPosition() {
		object_cube2.SetActive( true );

		object_triangle.transform.position		=	object_triangle_sposition;
		object_triangle.transform.rotation		=	Quaternion.EulerAngles( object_triangle_srotate );
		object_line.transform.position			=	object_line_sposition;
		object_line.transform.rotation			=	Quaternion.EulerAngles( object_line_srotate );
		object_cube1.transform.position			=	object_cube1_sposition;
		object_cube1.transform.rotation			=	Quaternion.EulerAngles( object_cube1_srotate );
		object_cube2.transform.position			=	object_cube2_sposition;
		object_cube2.transform.rotation			=	Quaternion.EulerAngles( object_cube2_srotate );
	}

    // ######################################################################

}

// ################################################################################