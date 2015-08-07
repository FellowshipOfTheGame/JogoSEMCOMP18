using UnityEngine;
using System.Collections;

public class RtsCam_BHV : MonoBehaviour {

	//Univarsal parametes
	public LayerMask LayerToFollow;
	public float h = 0.016666666f;
	public Vector3 CenterPivot;
	public Vector2 CameraBounds;
	public float MovementSpeedFactor = 0.5f;
	public float MinMovementSpeed = 20.0f;
	public float TurnSpeed = 90.0f;
	public float ZoomSpeed = 5000.0f;
	public float MaxZoom = 150.0f;
	public float ScrollFrame = 10.0f;
	private float Mass = 1.0f;
	private float I = 1.0f;
	//Universal variables
	private Vector2 ScrollDirection;
	private float Zoom = 0.0f;

	//Position parameters
	public float PositionK = 25.0f;
	public float PositionB = 8.0f;
	//Position variables
	private Vector3 TargetPosition;
	private Vector3 Position;
	private Vector3 Speed;
	private Vector3 Acceleration;

	//Angular parameters
	public float RotationRadius = 10.0f;
	public float AttackSoftnes = 0.05f;
	public float MaxAttack = -65.0f;
	public float RotationK = 30.0f;
	public float RotationB = 9.0f;
	//Rotation variables
	private float TargetRotation = 0.0f;
	private float RotationAcceleration = 0.0f;
	private float RotationSpeed = 0.0f;
	private float RotationAngle = 0.0f;
	//Attack variables
	private float TargetAttack = -70.0f;
	private float AttackAcceleration = 0.0f;
	private float AttackSpeed = 0.0f;
	private float AttackAngle = 0.0f;

	//Makes camera softly move to new position.
	public void SetPosition (Vector3 PositionToSet){

		TargetPosition.x = PositionToSet.x;
		TargetPosition.z = PositionToSet.z;

		if (TargetPosition.x >= CenterPivot.x+CameraBounds.x) {TargetPosition.x = CenterPivot.x+CameraBounds.x;}
		if (TargetPosition.x <= CenterPivot.x-CameraBounds.x) {TargetPosition.x = CenterPivot.x-CameraBounds.x;}
		if (TargetPosition.z >= CenterPivot.z+CameraBounds.y) {TargetPosition.z = CenterPivot.z+CameraBounds.y;}
		if (TargetPosition.z <= CenterPivot.z-CameraBounds.y) {TargetPosition.z = CenterPivot.z-CameraBounds.y;}

	}

	//Makes camera instantly warp to new position.
	public void ForceSetPosition (Vector3 PositionToSet){
		
		TargetPosition.x = PositionToSet.x;
		TargetPosition.z = PositionToSet.z;
		
		if (TargetPosition.x >= CenterPivot.x+CameraBounds.x) {TargetPosition.x = CenterPivot.x+CameraBounds.x;}
		if (TargetPosition.x <= CenterPivot.x-CameraBounds.x) {TargetPosition.x = CenterPivot.x-CameraBounds.x;}
		if (TargetPosition.z >= CenterPivot.z+CameraBounds.y) {TargetPosition.z = CenterPivot.z+CameraBounds.y;}
		if (TargetPosition.z <= CenterPivot.z-CameraBounds.y) {TargetPosition.z = CenterPivot.z-CameraBounds.y;}

		Position.x = TargetPosition.x;
		Position.z = TargetPosition.z;
		Speed.x = 0.0f;
		Speed.z = 0.0f;

	}

	//Makes camera softly move and rotate to new zoom. 
	public void SetZoom (float ZoomToSet){

		Zoom = ZoomToSet;
		if (Zoom <= 0.0f) {Zoom = 0.0f;}
		if (Zoom >= MaxZoom) {Zoom = MaxZoom;}

	}

	//Makes camera instantly warp and rotate to new zoom. 
	public void ForceSetZoom (float ZoomToSet){
		
		Zoom = ZoomToSet;
		if (Zoom <= 0.0f) {Zoom = 0.0f;}
		if (Zoom >= MaxZoom) {Zoom = MaxZoom;}

		TargetAttack = -Mathf.Rad2Deg*Mathf.Atan(AttackSoftnes*Zoom-Mathf.Tan(Mathf.Deg2Rad*MaxAttack));
		AttackAngle = TargetAttack;
		AttackSpeed = 0.0f;

	}

	//Makes camera instantly rotate to new rotation. 
	public void ForceSetRotation (float RotationToSet){
		
		TargetRotation = RotationToSet;
		RotationAngle = TargetRotation;
		RotationSpeed = 0.0f;

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		//Calculates target position
		ScrollDirection = Vector2.zero;

		if (Input.GetKey(KeyCode.D)) {ScrollDirection.x = ScrollDirection.x+1.0f;}
		if (Input.GetKey(KeyCode.A)) {ScrollDirection.x = ScrollDirection.x-1.0f;}
		if (Input.GetKey(KeyCode.W)) {ScrollDirection.y = ScrollDirection.y+1.0f;}
		if (Input.GetKey(KeyCode.S)) {ScrollDirection.y = ScrollDirection.y-1.0f;}
		if (Input.mousePosition.x >= Screen.width-ScrollFrame) {ScrollDirection.x = ScrollDirection.x+1.0f;}
		if (Input.mousePosition.x <= ScrollFrame) {ScrollDirection.x = ScrollDirection.x-1.0f;}
		if (Input.mousePosition.y >= Screen.height-ScrollFrame) {ScrollDirection.y = ScrollDirection.y+1.0f;}
		if (Input.mousePosition.y <= ScrollFrame) {ScrollDirection.y = ScrollDirection.y-1.0f;}
		if (ScrollDirection.x > 1.0f) {ScrollDirection.x = 1.0f;}
		if (ScrollDirection.x < -1.0f) {ScrollDirection.x = -1.0f;}
		if (ScrollDirection.y > 1.0f) {ScrollDirection.y = 1.0f;}
		if (ScrollDirection.y < -1.0f) {ScrollDirection.y = -1.0f;}

		Zoom = Zoom-Input.GetAxis("Mouse ScrollWheel")*ZoomSpeed*h;
		if (Zoom <= 0.0f) {Zoom = 0.0f;}
		if (Zoom >= MaxZoom) {Zoom = MaxZoom;}

		TargetPosition.x = TargetPosition.x+(MovementSpeedFactor*Zoom+MinMovementSpeed)*h*(-ScrollDirection.y*Mathf.Sin(Mathf.Deg2Rad*TargetRotation)+ScrollDirection.x*Mathf.Cos(Mathf.Deg2Rad*TargetRotation));
		TargetPosition.z = TargetPosition.z+(MovementSpeedFactor*Zoom+MinMovementSpeed)*h*( ScrollDirection.y*Mathf.Cos(Mathf.Deg2Rad*TargetRotation)+ScrollDirection.x*Mathf.Sin(Mathf.Deg2Rad*TargetRotation));

		if (TargetPosition.x >= CenterPivot.x+CameraBounds.x) {TargetPosition.x = CenterPivot.x+CameraBounds.x;}
		if (TargetPosition.x <= CenterPivot.x-CameraBounds.x) {TargetPosition.x = CenterPivot.x-CameraBounds.x;}
		if (TargetPosition.z >= CenterPivot.z+CameraBounds.y) {TargetPosition.z = CenterPivot.z+CameraBounds.y;}
		if (TargetPosition.z <= CenterPivot.z-CameraBounds.y) {TargetPosition.z = CenterPivot.z-CameraBounds.y;}

		RaycastHit Hit;
		if (Physics.Raycast(Position+Vector3.up*100.0f, Vector3.down, out Hit, Mathf.Infinity, LayerToFollow)){
			TargetPosition.y = Hit.point.y*(1.0f-Zoom/MaxZoom)+CenterPivot.y+Zoom;
		}
		else{
			TargetPosition.y = CenterPivot.y+Zoom;
		}

		//Calculates target rotation
		if (Input.GetKey(KeyCode.Q)){

			TargetRotation = TargetRotation+TurnSpeed*h;

		}
		if (Input.GetKey(KeyCode.E)){
			
			TargetRotation = TargetRotation-TurnSpeed*h;
			
		}
		//Calculate target attack
		TargetAttack = -Mathf.Rad2Deg*Mathf.Atan(AttackSoftnes*Zoom-Mathf.Tan(Mathf.Deg2Rad*MaxAttack));

		//Simulation for camera position
		Acceleration = -(PositionK/Mass)*(Position-TargetPosition) - (PositionB/Mass)*Speed;
		Speed = Speed+Acceleration*h;
		Position = Position+Speed*h;

		//Simulation for camera rotation
		RotationAcceleration = -(RotationK/I)*(RotationAngle-TargetRotation) - (RotationB/I)*RotationSpeed;
		RotationSpeed = RotationSpeed+RotationAcceleration*h;
		RotationAngle = RotationAngle+RotationSpeed*h;

		//Simulation for camera attack
		AttackAcceleration = -(RotationK/I)*(AttackAngle-TargetAttack) - (RotationB/I)*AttackSpeed;
		AttackSpeed = AttackSpeed+AttackAcceleration*h;
		AttackAngle = AttackAngle+AttackSpeed*h;
		
		if (AttackAngle >= 89.5f){
			
			AttackAngle = 89.5f;
			AttackSpeed = 0.0f;
			
		}
		
		if (AttackAngle <= -89.5f){
			
			AttackAngle = -89.5f;
			AttackSpeed = 0.0f;
			
		}

		//Camera transformation
		Vector3 RotationDirection = new Vector3(-Mathf.Sin(Mathf.Deg2Rad*RotationAngle), Mathf.Tan(Mathf.Deg2Rad*AttackAngle), Mathf.Cos(Mathf.Deg2Rad*RotationAngle));

		Debug.DrawLine(transform.position, transform.position+RotationDirection*10.0f,Color.red);

		transform.position = Position-RotationRadius*new Vector3(RotationDirection.x, 0.0f, RotationDirection.z);
		transform.rotation = Quaternion.LookRotation(RotationDirection, Vector3.up);

	}
}
