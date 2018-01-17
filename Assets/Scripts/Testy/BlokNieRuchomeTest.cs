using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class BlokNieRuchomeTest : MonoBehaviour {

	// PRIVATE VARIABLES
	//	...

	// PUBLIC VARIABLES
	public	GameObject		module_ui;

	public	GameObject		object_rope;
	public	GameObject		object_cubeA;
	public	GameObject		object_cubeB;
	public	GameObject		object_circle;

	public	GameObject		prefab_rope;
	public	GameObject		prefab_cubeA;
	public	GameObject		prefab_cubeB;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		//
	}

	// ######################################################################
	//	OBLICZENIA DANYCH
	// ######################################################################

	public void setData( float mass1, float mass2, float friction, float r ) {
		var	material				=	object_circle.GetComponent<CapsuleCollider>().material;
		material.dynamicFriction	=	friction;
		material.staticFriction		=	friction;

		object_cubeA.GetComponent<Rigidbody>().mass		=	mass1;
		object_cubeB.GetComponent<Rigidbody>().mass		=	mass2;

		calculateData( mass1, mass2, friction, r );
	}

	public void calculateData( float mass1, float mass2, float friction, float r ) {
		float	g	=	9.81f;
		float	f1	=	mass1 * g;
		float	f2	=	mass2 * g;
		float	q	=	f2 * r;
		float	p	=	q / friction;

		module_ui.GetComponent<BlokNieRuchomeTUI>().updateData( p, q );
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
		object_rope		=	Instantiate( prefab_rope, gameObject.transform );
		//object_rope.transform.position		=	new Vector3( -18.5f, 29.5f, 0.0f );
		//object_rope.transform.rotation		=	Quaternion.EulerAngles( new Vector3( 0.0f, 0.0f, 90.0f ) );
		//object_rope.transform.localScale	=	new Vector3( 0.25f, 0.25f, 0.25f );

		/*
		for ( int i = 0; i < object_rope.transform.childCount; i++ ) {
			var	rope_element	=	object_rope.transform.GetChild( i ).gameObject;

			rope_element.GetComponent<Rigidbody>().useGravity	=	true;
			rope_element.GetComponent<Rigidbody>().mass			=	1;
		}
		*/

		var	rope_elementStart				=	object_rope.transform.GetChild(0);
		var rope_elementEnd					=	object_rope.transform.GetChild( object_rope.transform.childCount - 1 );
		
		foreach( CharacterJoint component in rope_elementStart.GetComponents<CharacterJoint>() ) {
			if ( component.connectedBody == null ) {
				object_cubeA						=	Instantiate( prefab_cubeA, gameObject.transform );
				//object_cubeA.transform.position		=	new Vector3( -3.515878f, 11.2f, 0.0f );
				//object_cubeA.transform.rotation		=	Quaternion.EulerAngles( new Vector3( 0.0f, 0.0f, 1.825f ) );
				//object_cubeA.transform.localScale	=	new Vector3( 4.0f, 4.0f, 4.0f );
				component.connectedBody				=	object_cubeA.GetComponent<Rigidbody>();
				component.enableProjection			=	true;

				object_cubeA.GetComponent<CharacterJoint>().connectedBody		=	rope_elementStart.GetComponent<Rigidbody>();
				object_cubeA.GetComponent<CharacterJoint>().enableProjection	=	true;
			}
		}

		foreach( CharacterJoint component in rope_elementEnd.GetComponents<CharacterJoint>() ) {
			if ( component.connectedBody == null ) {
				object_cubeB						=	Instantiate( prefab_cubeB, gameObject.transform );
				//object_cubeB.transform.position		=	new Vector3( 4.135021f, 8.75f, 0.0f );
				//object_cubeB.transform.rotation		=	Quaternion.EulerAngles( new Vector3( 0.0f, 0.0f, -2.579f ) );
				//object_cubeB.transform.localScale	=	new Vector3( 4.0f, 4.0f, 4.0f );
				component.connectedBody				=	object_cubeB.GetComponent<Rigidbody>();
				component.enableProjection			=	true;

				object_cubeB.GetComponent<CharacterJoint>().connectedBody		=	rope_elementEnd.GetComponent<Rigidbody>();
				object_cubeB.GetComponent<CharacterJoint>().enableProjection	=	true;
			}
		}
	}

	// ----------------------------------------------------------------------
	private void removeData() {
		GameObject.Destroy( object_cubeA );
		GameObject.Destroy( object_cubeB );
		GameObject.Destroy( object_rope );
	}

	// ######################################################################

}

// ################################################################################