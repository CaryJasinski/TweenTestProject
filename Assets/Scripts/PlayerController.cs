using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform[] ControlPath;
	public Transform Player;
	public enum Direction {Forward,Reverse};
	public float PathPosition = 0;
	public float Speed = 0.1f;
	public float Gravity = 0.7f;
	public float JumpForce = 0.2f;
	public float LookAheadAmount = 0.01f;

	public float RayLength = 20;
	public Vector3 FloorPosition;	
	private RaycastHit Hit;
	private Direction CharacterDirection;
	private float YSpeed = 0;

	public int JumpState = 0; //0=grounded 1=jumping 2=double jumping
	
	void OnDrawGizmos()
	{
		iTween.DrawPath(ControlPath,Color.green);	
	}	

	void Start()
	{
		//plop the character pieces in the "Ignore Raycast" layer so we don't have false raycast data:	
		foreach (Transform child in Player) {
			child.gameObject.layer=2;
		}
	}
	
	void Update()
	{
		DetectKeys();
		FindFloorAndRotation();
		MoveCharacter();
		MoveCamera();
	}
	
	void DetectKeys()
	{
		//if(Input.GetKey("up"))
			MoveForward();

		if(Input.GetKey("down"))
			MoveBackward();

		if (Input.GetKeyDown("space"))
			Jump();

	}
	void FindFloorAndRotation()
	{
		float pathPercent = PathPosition%1;
		Vector3 coordinateOnPath = iTween.PointOnPath(ControlPath,pathPercent);
		Vector3 lookTarget;
		
		//calculate look data
		if(pathPercent-LookAheadAmount>=0 && pathPercent+LookAheadAmount <= 1){
			
			//leading or trailing point for the player rotation
			if(CharacterDirection==Direction.Forward){
				lookTarget = iTween.PointOnPath(ControlPath,pathPercent+LookAheadAmount);
			}else{
				lookTarget = iTween.PointOnPath(ControlPath,pathPercent-LookAheadAmount);
			}

			Player.LookAt(lookTarget);
			
			//nullify all rotations except y
			float yRot = Player.eulerAngles.y;
			Player.eulerAngles=new Vector3(0,yRot,0);
		}

		if (Physics.Raycast(coordinateOnPath,-Vector3.up,out Hit, RayLength)){
			Debug.DrawRay(coordinateOnPath, -Vector3.up * Hit.distance);
			FloorPosition=Hit.point;
		}
	}

	void MoveCharacter()
	{
		YSpeed += Gravity * Time.deltaTime;
		Player.position = new Vector3(FloorPosition.x,Player.position.y-YSpeed,FloorPosition.z);
		
		//floor checking
		if(Player.position.y<FloorPosition.y){
			YSpeed = 0;
			JumpState = 0;
			Player.position = new Vector3(FloorPosition.x,FloorPosition.y,FloorPosition.z);
		}		
	}
	
	void MoveForward()
	{
		PathPosition += Time.deltaTime * Speed;
		CharacterDirection=Direction.Forward;
	}

	void MoveBackward()
	{
		CharacterDirection=Direction.Reverse;
		PathPosition -= Time.deltaTime * Speed;
	}

	void Jump()
	{
		//double jump - must be called before jump
		if (JumpState==1) {
			YSpeed-=JumpForce;
			JumpState = 2;
		}
		//jump
		if (JumpState==0) {
			YSpeed-=JumpForce;
			JumpState = 1;
		}
	}

	void MoveCamera()
	{
		iTween.MoveUpdate(Camera.main.gameObject,new Vector3(Player.position.x,2.7f,Player.position.z-5f),.9f);	
	}

}