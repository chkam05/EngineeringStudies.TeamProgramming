using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class RopeModule : MonoBehaviour {

	// PRIVATE VARIABLES
	private		Rigidbody		rigid_body;

	// PUBLIC VARIABLES
	public		GameObject		rope_part;

	// ######################################################################

	void Start() {
		setPhysics();
	}

	// ----------------------------------------------------------------------
	public void createRope( int length ) {
		for ( int i = 1; i < length; i++ ) {
			var		new_part				=	Instantiate( rope_part, this.transform );
			new_part.transform.position		=	new Vector3( this.transform.position.x, this.transform.position.y + (i*0.25f), this.transform.position.z );
		}
	}

	// ----------------------------------------------------------------------
	public void setPhysics () {
		this.gameObject.AddComponent<Rigidbody>();

		this.rigid_body					=	this.gameObject.GetComponent<Rigidbody>();
		this.rigid_body.isKinematic		=	true;

		int childCount					=	this.transform.childCount;
		for ( int i = 0; i < childCount; i++ ) {
			Transform	t				=	this.transform.GetChild(i);

			t.gameObject.AddComponent<HingeJoint>();

			HingeJoint	h				=	t.gameObject.GetComponent<HingeJoint>();
			h.connectedBody				=	(i == 0) ? this.rigid_body : this.transform.GetChild( i-1 ).GetComponent<Rigidbody>();
			h.useSpring					=	true;
			h.enableCollision			=	true;
		}
	}

	// ######################################################################

}

// ################################################################################