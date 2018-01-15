using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################
//	ROPE BUILDER
// ################################################################################

public class RopeBuilder : MonoBehaviour {

	// PRIVATE VARIABLES
	//	...

	// PUBLIC VARIABLES
	public	int			count_elements;
	public	float		diffrence_elements;
	public	GameObject	clone_object;

	// ######################################################################
	//	INIT BUILDER
	// ######################################################################

	void Start() {
		functionBuildRope( gameObject );
		functionTwinMode( gameObject );
	}

	// ######################################################################
	//	BUILD ROPE
	// ######################################################################

	public void functionBuildRope( GameObject container ) {
		var	last_position	=	container.transform.position;

		for ( int i = 1; i < count_elements; i++ ) {
			var		new_object				=	Instantiate( clone_object, container.transform );
			new_object.transform.position	=	last_position;
			last_position					=	last_position - new Vector3( 0, diffrence_elements, 0 );
		}
	}

	// ######################################################################
	//	CONNECT ROPE AND SET PHYSICS
	// ######################################################################
	
	public void functionTwinMode ( GameObject container ) {
		Time.timeScale	=	0.0f;

		for ( int i = container.transform.childCount-1; i >= 0; i-- ) {
			var		rope_element	=	container.transform.GetChild( i ).gameObject;

			if ( i > 0 ) {
				var	rope_backward				=	rope_element.AddComponent<CharacterJoint>();
				rope_backward.connectedBody		=	container.transform.GetChild( i-1 ).GetComponent<Rigidbody>();
				rope_backward.enableProjection	=	true;
			}

			if ( i < transform.childCount-1 ) {
				var	rope_forward				=	rope_element.AddComponent<CharacterJoint>();
				rope_forward.connectedBody		=	container.transform.GetChild( i+1 ).GetComponent<Rigidbody>();
				rope_forward.enableProjection	=	true;
			}

			rope_element.GetComponent<Rigidbody>().useGravity				=	true;
			rope_element.GetComponent<CapsuleCollider>().isTrigger			=	false;
		}
	}

	// ######################################################################

}

// ################################################################################