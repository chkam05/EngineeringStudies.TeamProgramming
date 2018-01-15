using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaDwustronna2Game: MonoBehaviour {

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
	public	GameObject		object_cube1;
	public	GameObject		object_cube2;
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
        calculateF();
    }

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void calculate_Block( float length, float traingle, float m1, float d1, float d2 ) {
        var lineScale = object_line.transform.localScale;
        object_line.transform.localScale = new Vector3(length, lineScale.y, 1.0f);

        var trianglePosition = object_triangle.transform.position;
        var triangleScale = object_triangle.transform.localScale;
        object_triangle.transform.position = new Vector3( traingle, trianglePosition.y, 0.0f);
        
        var cubeAScale = object_cube1.transform.localScale;
        var cubeAPosition = object_cube1.transform.position;
        object_cube1.transform.position = new Vector3( -d1 * (cubeAScale.x / 2) - traingle, cubeAPosition.y, 0.0f);
        object_cube1.GetComponent<Rigidbody2D>().mass = m1;

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

        object_cube2.GetComponent<Rigidbody2D>().mass = m2;

        module_ui.GetComponent<DzwigniaDwustronna2UI>().updateData( f1, f2, d1, d2 );
    }

    // ######################################################################

}

// ################################################################################