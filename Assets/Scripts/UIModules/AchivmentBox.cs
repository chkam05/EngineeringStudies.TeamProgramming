using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	 XXX     XXX    X   X   XXXXX   X   X   X   X   XXXXX   X   X   XXXXX    XXXX      XXX      XXX    X   X
//	X   X   X   X   X   X     X     X   X   XX XX   X       XX  X     X     X          X  X    X   X    X X 
//	XXXXX   X       XXXXX     X     X   X   X X X   XXX     X X X     X      XXX       XXXX    X   X     X  
//	X   X   X   X   X   X     X      X X    X   X   X       X  XX     X         X      X   X   X   X    X X 
//	X   X    XXX    X   X   XXXXX     X     X   X   XXXXX   X   X     X     XXXX       XXXX     XXX    X   X
// ################################################################################

public class AchivmentBox : MonoBehaviour {

	// ######################################################################
	public enum AchivmentType {
		complete,
		work,
		fail
	}

	// ######################################################################
	//	 XXX     XXX    X   X   XXXXX   X   X   X   X   XXXXX   X   X   XXXXX
	//	X   X   X   X   X   X     X     X   X   XX XX   X       XX  X     X  
	//	XXXXX   X       XXXXX     X     X   X   X X X   XXX     X X X     X  
	//	X   X   X   X   X   X     X      X X    X   X   X       X  XX     X  
	//	X   X    XXX    X   X   XXXXX     X     X   X   XXXXX   X   X     X  
	// ######################################################################
	public class Achivment {
		
		// PRIVATE VARIABLES:
		private	string			title;
		private	string			description;
		private AchivmentType	status;
		private	string			res_customImage;

		// PUBLIC VARIABLES:
		//	...

		// ############################################################

		public Achivment( string title, string description ) {
			this.title				=	title;
			this.description		=	description;
			this.status				=	AchivmentType.work;
			this.res_customImage	=	"";
		}

		// ------------------------------------------------------------
		public void setStatus( AchivmentType status ) { 
			this.status				=	status;
		}

		// ------------------------------------------------------------
		public void setCustomImageResource( string resource ) {
			this.res_customImage	=	resource;
		}

		// ------------------------------------------------------------
		public string 			getTitle() 			{ return this.title; }
		public string 			getDescription() 	{ return this.description; }
		public AchivmentType	getStatus()			{ return this.status; }
		public bool				isCustomImage()		{ return this.res_customImage != string.Empty; }
		public Texture			getCustomImage()	{ return Resources.Load( this.res_customImage ) as Texture; }

		// ############################################################

	}

	// ######################################################################
	// ######################################################################

	// PRIVATE VARIABLES:
	private	List<Achivment>		achivments;
	private	List<GameObject>	achivment_items;

	// PUBLIC VARIABLES:
	public	GameObject			item_achivment;
	public	GameObject			container_items;
	public	GameObject			button_close;

	public	delegate void	EndingBoxOKAction( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	
	public void Setup() {
		this.achivments			=	new List<Achivment>();
		this.achivment_items	=	new List<GameObject>();
		this.button_close.GetComponent<Button>().onClick.RemoveAllListeners();
		this.button_close.GetComponent<Button>().onClick.AddListener( onButtonCloseClick );
	}

	// ----------------------------------------------------------------------
	public void Init() {
		showBox();
	}
	
	// ######################################################################
	//	 XXX     XXX    X   X   XXXXX   X   X   X   X   XXXXX   X   X   XXXXX    XXXX
	//	X   X   X   X   X   X     X     X   X   XX XX   X       XX  X     X     X    
	//	XXXXX   X       XXXXX     X     X   X   X X X   XXX     X X X     X      XXX 
	//	X   X   X   X   X   X     X      X X    X   X   X       X  XX     X         X
	//	X   X    XXX    X   X   XXXXX     X     X   X   XXXXX   X   X     X     XXXX 
	// ######################################################################

	public void addAchivment( string title, string description, string res_image ) {
		Achivment	achivment	=	new Achivment( title, description );
		if ( res_image != null ) { achivment.setCustomImageResource( res_image ); }
		achivments.Add( achivment );
		if ( gameObject.activeInHierarchy ) { updateBox(); }
	}

	// ----------------------------------------------------------------------
	public void changeAchivmentStatus( int index, AchivmentType status ) {
		if ( index >= 0 && index < achivments.Count ) {
			achivments[index].setStatus( status );
			if ( gameObject.activeInHierarchy ) { updateBox(); }
		}
	}

	// ----------------------------------------------------------------------
	public void removeAchivment( int index ) {
		if ( index >= 0 && index < achivments.Count ) {
			achivments.RemoveAt( index );
			if ( gameObject.activeInHierarchy ) { updateBox(); }
		}
	}

	// ######################################################################
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################

	private void clearBox() {
		for ( int i = 0; i < achivment_items.Count; i++ ) {
			GameObject.Destroy( achivment_items[i] );
		}
		achivment_items.Clear();
	}

	// ----------------------------------------------------------------------
	private void updateBox() {
		clearBox();
		
		//float	x					=	item_achivment.GetComponent<RectTransform>().sizeDelta.x;
		float	y					=	item_achivment.GetComponent<RectTransform>().sizeDelta.y;
		float	width				=	container_items.GetComponent<RectTransform>().sizeDelta.x;
		float	height				=	( 8 + y ) * achivments.Count + 32;

		container_items.GetComponent<RectTransform>().sizeDelta		=	new Vector2( width, height );
		for ( int i = 0; i < achivments.Count; i++ ) {
			float	top				=	( 8 + y ) * i + 16;

			float	offset_MaxX		=	-16;
			float	offset_MaxY		=	-top;
			float	offset_MinX		=	16;
			float	offset_MinY		=	-top - y;
			//Debug.Log( "offsetMin :: " + i + " :: " + offset_MinX + " :: " + offset_MinY );
			//Debug.Log( "offsetMax :: " + i + " :: " + offset_MaxX + " :: " + offset_MaxY );

			GameObject	item		=	Instantiate( item_achivment, container_items.transform );
			item.GetComponent<RectTransform>().offsetMin			=	new Vector2( offset_MinX, offset_MinY );
			item.GetComponent<RectTransform>().offsetMax			=	new Vector2( offset_MaxX, offset_MaxY );
			//item.GetComponent<RectTransform>().sizeDelta			=	new Vector2( x, y );

			item.transform.GetChild( 1 ).GetComponent<Text>().text	=	achivments[i].getTitle();
			item.transform.GetChild( 2 ).GetComponent<Text>().text	=	achivments[i].getDescription();
			Color		achivment_color								=	new Color( 0.0f, 0.0f, 0.0f, 0.87f );

			switch ( achivments[i].getStatus() ) {
				case AchivmentType.complete:
					item.transform.GetChild( 0 ).GetComponent<RawImage>().texture	=	Resources.Load( "Images/icon_achivmentcomplete" ) as Texture;
					ColorUtility.TryParseHtmlString( Tools.achivment_colors[0], out achivment_color );
					break;
				case AchivmentType.work:
					item.transform.GetChild( 0 ).GetComponent<RawImage>().texture	=	Resources.Load( "Images/icon_achivmentwork" ) as Texture;
					break;
				case AchivmentType.fail:
					item.transform.GetChild( 0 ).GetComponent<RawImage>().texture	=	Resources.Load( "Images/icon_achivmentfailed" ) as Texture;
					ColorUtility.TryParseHtmlString( Tools.achivment_colors[2], out achivment_color );
					break;
			}

			item.GetComponent<Image>().color						=	achivment_color;
			achivment_items.Add( item );
		}
	}

	// ----------------------------------------------------------------------
	private void showBox() {
		updateBox();
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	private void hideBox() {
		clearBox();
		gameObject.SetActive( false );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################

	private void onButtonCloseClick() {
		hideBox();
	}

	// ######################################################################

}

// ################################################################################