using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class DzwigniaDwustronnaGame: MonoBehaviour {

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
        float move_cobe1 = Input.GetAxis("Vertical");
        float move_cube2 = Input.GetAxis("Horizontal");
        var rgbody1 = object_cube1.GetComponent<Rigidbody2D>();
        var rgbody2 = object_cube2.GetComponent<Rigidbody2D>();
        rgbody1.velocity = new Vector2(move_cobe1 * 7.0f, rgbody1.velocity.y);
        rgbody2.velocity = new Vector2(move_cube2 * 7.0f, rgbody2.velocity.y);

        calculateF();
    }

	// ######################################################################
	//	OBLICZANIE ALPHA I DOSTOSOWANIE OBIEKTU 
	// ######################################################################

	public void calculate_Block( float length, float traingle, float m1, float m2, float d1, float d2 ) {
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
        object_cube2.GetComponent<Rigidbody2D>().mass = m2;
    }

	// ######################################################################
	//	OBLICZANIE ZMIENNYCH
	// ######################################################################

	public void calculateF() {
        object[]    data    =   module_ui.GetComponent<DzwigniaDwustronnaUI>().getData();
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

        module_ui.GetComponent<DzwigniaDwustronnaUI>().updateData( f1r1, f2r2, d1, d2 );
    }

    // ######################################################################

}

// ################################################################################