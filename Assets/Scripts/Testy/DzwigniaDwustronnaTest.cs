using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaDwustronnaTest: MonoBehaviour {

	// PRIVATE VARIABLES
	private	Vector3			cube_position;
	private	Vector3			cube_rotation;
	private	Vector3			cube_scale;

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

	void Start () {
		//
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
		if ( Time.timeScale > 0 ) {
	        float move_cube1	=	Input.GetAxis("Vertical");
        	float move_cube2	=	Input.GetAxis("Horizontal");

			var rgbody1			=	object_cube1.GetComponent<Rigidbody>();
        	var rgbody2			=	object_cube2.GetComponent<Rigidbody>();
	
			object_cube1.transform.position	+=	Vector3.left * move_cube1 * 0.2f;
        	object_cube2.transform.position	+=	Vector3.left * move_cube2 * 0.2f;

			calculateF();
		}
    }

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void calculate_Block( float length, float traingle, float m1, float m2, float d1, float d2 ) {
        var lineScale = object_line.transform.localScale;
        object_line.transform.localScale = new Vector3(length, lineScale.y, lineScale.z);

        var trianglePosition = object_triangle.transform.position;
        var triangleScale = object_triangle.transform.localScale;
        object_triangle.transform.position = new Vector3( traingle, trianglePosition.y, 0.0f);
        
        var cubeAScale = object_cube1.transform.localScale;
        var cubeAPosition = object_cube1.transform.position;
        object_cube1.transform.position = new Vector3( -d1 * (cubeAScale.x / 2) - traingle, cubeAPosition.y, 0.0f);
        object_cube1.GetComponent<Rigidbody>().mass = m1;

        var cubeBScale = object_cube2.transform.localScale;
        var cubeBPosition = object_cube2.transform.position;
        object_cube2.transform.position = new Vector3(d2 * (cubeBScale.x / 2) + traingle, cubeBPosition.y, 0.0f);
        object_cube2.GetComponent<Rigidbody>().mass = m2;
    }

	// ######################################################################
	//	OBLICZANIE ZMIENNYCH
	// ######################################################################

	public void calculateF() {
        object[]    data    =   module_ui.GetComponent<DzwigniaDwustronnaTUI>().getData();
        var cubeAScale      =   object_cube1.transform.localScale;
        var cubeBScale      =   object_cube2.transform.localScale;

        float       m1      =   (float) data[2];
        float       m2      =   (float) data[3];
        float       d1      =   -object_cube1.transform.position.x / (cubeAScale.x / 2) + object_triangle.transform.position.x;
        float       d2      =   object_cube2.transform.position.x / (cubeBScale.x / 2) - object_triangle.transform.position.x;

        float f1 = 0.0f;
        float f2 = 0.0f;

        f1 = m1 * 9.81f;
        f2 = m2 * 9.81f;

        float f1r1;
        float f2r2;

        f1r1 = f1 * d1;
        f2r2 = f2 * d2;

        module_ui.GetComponent<DzwigniaDwustronnaTUI>().updateData( f1r1, f2r2, d1, d2 );
    }

    // ######################################################################
	//	POZYCJONOWANIE
	// ######################################################################

	public void resetPosition() {
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