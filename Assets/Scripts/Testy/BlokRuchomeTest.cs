using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class BlokRuchomeTest : MonoBehaviour {

	// PRIVATE VARIABLES
	private	float			force					=	0.0f;
	private	float			mass2					=	0.0f;
	private	float			t						=	0.0f;
	private	float			r						=	0.0f;

	private float			force_increment			=	0.001f;
	private float			force_max				=	0.05f;

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_blockGlobal;
	public	GameObject		object_handlerGlobal;
	public	GameObject		object_ropeA;
	public	GameObject		object_ropeB;
	public	GameObject		object_cubeA;
	public	GameObject		object_cubeB;
	public	GameObject		object_circle;

	public	GameObject		prefab_ropeA;
	public	GameObject		prefab_ropeB;
	public	GameObject		prefab_cubeA;
	public	GameObject		prefab_cubeB;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		//
	}

	void Update() {
		if ( Time.timeScale > 0.0f ) {
	        float horizontalMove	=	Input.GetAxis("Vertical");
			var	position			=	object_cubeA.transform.position;
	
			if ( this.force > force_max ) { this.force = force_max; }
			else if ( this.force < -force_max ) { this.force = -force_max; }
			else { this.force += horizontalMove * force_increment; }

			object_cubeA.transform.position		+=	Vector3.up * force;

			if ( position.y > 31.75f ) {
				force								=	0.0f;
				object_cubeA.transform.position		=	new Vector3( position.x, 31.75f, 0.0f );
			} else if ( position.y < 17.77 ) {
				force								=	0.0f;
				object_cubeA.transform.position		=	new Vector3( position.x, 17.77f, 0.0f );
			}

			calculateData( this.mass2, this.t, this.r );
		}
	}

	// ######################################################################
	//	OBLICZENIA DANYCH
	// ######################################################################

	public void setData( float mass2, float friction, float r ) {
		var	material				=	object_circle.GetComponent<CapsuleCollider>().material;
		material.dynamicFriction	=	friction;
		material.staticFriction		=	friction;

		this.mass2					=	mass2;
		this.t						=	friction;
		this.r						=	r;

		object_cubeA.GetComponent<Rigidbody>().mass		=	mass2;
		object_cubeB.GetComponent<Rigidbody>().mass		=	1.0f;

		calculateData( mass2, friction, r );
	}

	public void calculateData( float mass2, float friction, float r ) {
		float	g	=	9.81f;

		float	f2	=	mass2 * g;
		float	q	=	f2 * r;

		float	nx	=	(1+friction)/2;
		float	p	=	q / nx;
		float	f1	=	p / r;
		float	a	=	f1 / 1.0f;

		a			=	(( g + force ) * mass2) / nx;
		f1			=	a + (a*force) * 1.0f;
		p			=	f1 * r;

		module_ui.GetComponent<BlokRuchomeTUI>().updateData( a + (a*force), p, q );
	}

	// ######################################################################
	//	ELEMENTS DATA
	// ######################################################################

	public void resetData() {
		removeData();
		createData();
	}

	// ----------------------------------------------------------------------
	private void createData() {
		var	block_transform							=	object_blockGlobal.transform.position;
		var handler_transform						=	object_handlerGlobal.transform.position;
		object_handlerGlobal.transform.position		=	new Vector3( block_transform.x, block_transform.y - 4.0f, block_transform.z );

		object_ropeA		=	Instantiate( prefab_ropeA, gameObject.transform );
		object_ropeB		=	Instantiate( prefab_ropeB, gameObject.transform );

		// Lina Górna + CubeA
		var ropeA_elementEnd					=	object_ropeA.transform.GetChild( object_ropeA.transform.childCount - 1 );

		foreach( CharacterJoint component in ropeA_elementEnd.GetComponents<CharacterJoint>() ) {
			if ( component.connectedBody == null ) {
				object_cubeA						=	Instantiate( prefab_cubeA, gameObject.transform );
				component.connectedBody				=	object_cubeA.GetComponent<Rigidbody>();
				component.enableProjection			=	true;

				object_cubeA.GetComponent<CharacterJoint>().connectedBody		=	ropeA_elementEnd.GetComponent<Rigidbody>();
				object_cubeA.GetComponent<CharacterJoint>().enableProjection	=	true;
			}
		}

		// Lina Dolna + GlobalHandler / CubeB
		var	ropeB_elementStart					=	object_ropeB.transform.GetChild( 0 );
		var ropeB_elementEnd					=	object_ropeB.transform.GetChild( object_ropeB.transform.childCount - 1 );

		foreach( CharacterJoint component in ropeB_elementStart.GetComponents<CharacterJoint>() ) {
			if ( component.connectedBody == null ) {
				component.connectedBody				=	object_handlerGlobal.GetComponent<Rigidbody>();
				component.enableProjection			=	true;

				object_handlerGlobal.GetComponent<CharacterJoint>().connectedBody		=	ropeB_elementStart.GetComponent<Rigidbody>();
				object_handlerGlobal.GetComponent<CharacterJoint>().enableProjection	=	true;
			}
		}

		foreach( CharacterJoint component in ropeB_elementEnd.GetComponents<CharacterJoint>() ) {
			if ( component.connectedBody == null ) {
				object_cubeB						=	Instantiate( prefab_cubeB, gameObject.transform );
				component.connectedBody				=	object_cubeB.GetComponent<Rigidbody>();
				component.enableProjection			=	true;

				object_cubeB.GetComponent<CharacterJoint>().connectedBody		=	ropeB_elementEnd.GetComponent<Rigidbody>();
				object_cubeB.GetComponent<CharacterJoint>().enableProjection	=	true;
			}
		}

	}

	// ----------------------------------------------------------------------
	private void removeData() {
		GameObject.Destroy( object_cubeA );
		GameObject.Destroy( object_ropeA );
		GameObject.Destroy( object_cubeB );
		GameObject.Destroy( object_ropeB );
	}

	// ######################################################################

}

// ################################################################################