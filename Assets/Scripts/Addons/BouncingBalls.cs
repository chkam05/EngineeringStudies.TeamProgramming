using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class BouncingBalls : MonoBehaviour {

	// PRIVATE VARIABLES
	private	GameObject			ball_current;
	private bool				gravity				=	true;
	private float				speed				=	30.0f;

	private Vector3				start_position		=	new Vector3( 0.0f, 15.0f, 0.0f );
	private float				start_mass			=	1.0f;
	private	float				start_friction		=	0.0f;
	private	float				start_bounciess		=	1.0f;

	// PUBLIC VARIABLES
	public	GameObject			module_ui;

	public	GameObject			border_top;
	public	GameObject			border_left;
	public	GameObject			border_right;
	public	GameObject			border_bottom;

	public	GameObject			balls_container;
	public	List<GameObject>	balls;

	// ######################################################################
	//	INICJOWANIE
	// ######################################################################

	void Start () {
		//
	}

	void Update() {
		if ( Input.GetMouseButtonDown( 0 ) ) {
			raycastHit();

		} else if ( Input.GetMouseButton( 0 ) ) {
			onMouseDrag();

		} else if ( Input.GetMouseButtonUp( 0 ) ) {
			onMouseUp();
		}

		ballChecker();
	}

	public void resetSimulation() {
		for ( int i = balls.Count-1; i > 0; i-- ) {
			destroyBall( i );
		}

		balls[0].GetComponent<Rigidbody>().velocity			=	Vector3.zero;
		balls[0].GetComponent<Rigidbody>().angularVelocity	=	Vector3.zero;
		balls[0].transform.position							=	start_position;
		balls[0].GetComponent<Rigidbody>().useGravity		=	gravity;
	}

	// ######################################################################
	//	CHECK
	// ######################################################################

	private void ballChecker() {
		for ( int b = 0; b < balls.Count; b++ ) {
			var	position	=	balls[b].transform.position;
			var scale		=	balls[b].transform.localScale;

			if ( position.y >= border_top.transform.position.y ) {
				balls[b].transform.position = new Vector3 ( position.x, border_top.transform.position.y - (scale.y/2), 0.0f );
			}
			if ( position.y <= border_bottom.transform.position.y ) {
				balls[b].transform.position = new Vector3 ( position.x, border_bottom.transform.position.y + (scale.y/2), 0.0f );
			}
			if ( position.x < border_left.transform.position.x ) {
				balls[b].transform.position = new Vector3 ( border_left.transform.position.x + (scale.x/2), position.y, 0.0f );
			}
			if ( balls[b].transform.position.x > border_right.transform.position.x ) {
				balls[b].transform.position = new Vector3 ( border_right.transform.position.x - (scale.x/2), position.y, 0.0f );
			}
		}
	}

	// ######################################################################
	//	BALLS
	// ######################################################################

	public void addBall() {
		if ( balls.Count >= 10 ) { return; }

		var		new_ball	=	Instantiate( balls[0].gameObject, balls_container.transform );
		new_ball.GetComponent<Rigidbody>().velocity			=	Vector3.zero;
		new_ball.GetComponent<Rigidbody>().angularVelocity	=	Vector3.zero;
		new_ball.transform.position							=	start_position;
		new_ball.GetComponent<Rigidbody>().useGravity		=	gravity;

		var		color_material		=	new Color( Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ), 1.0f );
		var		color_emmision		=	new Color( color_material.r * 0.25f, color_material.g * 0.25f, color_material.b * 0.25f );

		new_ball.GetComponent<Renderer>().material.color	=	color_material;
		var		material			=	new_ball.GetComponent<Renderer>().material;
		material.SetColor( "_EmissionColor", color_emmision );

		balls.Add( new_ball );
		setData( balls.Count - 1, start_mass, start_friction, start_bounciess );
		module_ui.GetComponent<BouncingBallsUI>().updateBall( balls.Count - 1 );
	}

	// ----------------------------------------------------------------------
	public void destroyBall( int index ) {
		if ( balls.Count <= 1 || index >= balls.Count ) { return; }

		GameObject.Destroy( balls[index] );
		balls.RemoveAt( index );
		if ( index > 0 ) { module_ui.GetComponent<BouncingBallsUI>().updateBall( index - 1 ); }
	}

	// ######################################################################
	//	DATA
	// ######################################################################

	public void setData( int index, float mass, float friction, float bounciess ) {
		if ( index >= 0 && index < balls.Count ) {
			balls[index].GetComponent<Rigidbody>().mass		=	mass;

			var		material			=	balls[index].GetComponent<SphereCollider>().material;
			material.dynamicFriction	=	friction;
			material.staticFriction		=	friction;
			material.bounciness			=	bounciess;
			balls[index].GetComponent<SphereCollider>().material	=	material;
		}
	}

	// ----------------------------------------------------------------------
	public void setGravity( bool gravity ) {
		this.gravity	=	gravity;

		foreach ( GameObject ball in balls ) {
			ball.GetComponent<Rigidbody>().useGravity	=	gravity;
		}
	}

	// ----------------------------------------------------------------------
	public void setSpeed( float speed ) {
		this.speed		=	speed;
	}

	// ----------------------------------------------------------------------
	public object[] getData( int index ) {
		if ( index >= 0 && index < balls.Count ) {
			var		material	=	balls[index].GetComponent<SphereCollider>().material;

			float	mass		=	balls[index].GetComponent<Rigidbody>().mass;
			float	friction	=	material.dynamicFriction;
			float	bounciess	=	material.bounciness;
			
			return new object[] { mass, friction, bounciess };
		} else {
			return new object[] { 0.0f, 0.0f, 0.0f };
		}
	}

	// ----------------------------------------------------------------------
	public bool getGravity() {
		return this.gravity;
	}

	// ----------------------------------------------------------------------
	public float getSpeed() {
		return this.speed;
	}

	// ######################################################################
	//	CONTROLLER
	// ######################################################################

	private void raycastHit() {
		RaycastHit	hitInfo		=	new RaycastHit();
		GameObject	hitObject	=	null;

		if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hitInfo ) ) {
			hitObject	=	hitInfo.transform.gameObject;

			if ( balls.Contains( hitObject ) ) {
				ball_current	=	hitObject;
				onMouseDown();
			}
		}
	}

	// ----------------------------------------------------------------------
	private void onMouseDown() {
		ball_current.GetComponent<Rigidbody>().isKinematic	=	true;
		//ball_current.GetComponent<Collider>().enabled		=	false;
	}

	// ----------------------------------------------------------------------
	private void onMouseDrag() {
		if ( ball_current != null ) {
			Ray			ray		=	Camera.main.ScreenPointToRay( Input.mousePosition );
			ball_current.transform.position		=	new Vector3( ray.GetPoint(40f).x, ray.GetPoint(40f).y, 0.0f );
		}
	}

	// ----------------------------------------------------------------------
	private void onMouseUp() {
		if ( ball_current != null ) {
			ball_current.GetComponent<Rigidbody>().isKinematic	=	false;
			//ball_current.GetComponent<Collider>().enabled		=	true;
	
			ball_current.GetComponent<Rigidbody>().AddForce( Camera.main.transform.right * Input.GetAxis("Mouse X") * speed, ForceMode.Impulse );
			ball_current.GetComponent<Rigidbody>().AddForce( Camera.main.transform.up * Input.GetAxis("Mouse Y") * speed, ForceMode.Impulse );
			ball_current	=	null;
		}
	}

	// ######################################################################

}

// ################################################################################