using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;
using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public Transform Player;

	public Transform LeftPathObject;
	public Transform CenterPathObject;
	public Transform RightPathObject;

	private HOPath LeftPath;
	private HOPath CenterPath;
	private HOPath RightPath;

	public enum Paths { Left = -1 , Center = 0 , Right = 1 };

	public int CurrentPath = (int)Paths.Center;

	[HideInInspector]public bool paused = false;

	void Awake()
	{
		LeftPath = LeftPathObject.GetComponent<HOPath>();
		CenterPath = CenterPathObject.GetComponent<HOPath>();
		RightPath = RightPathObject.GetComponent<HOPath>();
	}
    void Start()
    {
		//TODO: Initialize in Game Manager when added to game project
        HOTween.Init(true, true, true);


		HOTween.To(Player, 20, new TweenParms()
		    .Prop( "position", CenterPath.MakePlugVector3Path().OrientToPath())
			.Loops(-1, LoopType.Restart)
			.Ease(EaseType.Linear));

		//Adition Movement Type: Allows for control of character rotation
		//HOTween.To(Player, 20, new TweenParms()
		//	.Prop( "position", Player.GetComponent<HOPath>().MakePlugVector3Path(), true)
		//	.Loops(-1, LoopType.Yoyo)
		//	.Ease(EaseType.Linear));
    }

	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.LeftArrow))
			MoveLeft();
		if(Input.GetKeyDown (KeyCode.RightArrow))
			MoveRight();
	}

	void OnTriggerEnter(Collider other)
	{
		StartCoroutine(PauseTweenForSeconds(2));
	}

	IEnumerator PauseTweenForSeconds(float seconds)
	{
		TogglePause();
		yield return new WaitForSeconds(seconds);
		TogglePause();
	}


	void MoveRight()
	{
		if(CurrentPath == (int)Paths.Center)
		{
			CurrentPath = (int)Paths.Right;
			HOTween.Kill();
			HOTween.To(Player, 20, new TweenParms()
			   .Prop( "position", RightPath.MakePlugVector3Path().OrientToPath())
			   .Loops(-1, LoopType.Restart)
			   .Ease(EaseType.Linear));
		}
		if(CurrentPath == (int)Paths.Left)
		{
			CurrentPath = (int)Paths.Center;
			HOTween.Kill();
			HOTween.To(Player, 20, new TweenParms()
	           .Prop( "position", RightPath.MakePlugVector3Path().OrientToPath())
	           .Loops(-1, LoopType.Restart)
	           .Ease(EaseType.Linear));
		}
	}

	void MoveLeft()
	{
		if(CurrentPath == (int)Paths.Center)
		{
			CurrentPath = (int)Paths.Left;
			HOTween.Kill();
			HOTween.To(Player, 20, new TweenParms()
			           .Prop( "position", LeftPath.MakePlugVector3Path().OrientToPath())
			           .Loops(-1, LoopType.Restart)
			           .Ease(EaseType.Linear));
		}
		if(CurrentPath == (int)Paths.Right)
		{
			CurrentPath = (int)Paths.Center;
			HOTween.Kill();
			HOTween.To(Player, 20, new TweenParms()
			           .Prop( "position", CenterPath.MakePlugVector3Path().OrientToPath())
			           .Loops(-1, LoopType.Restart)
			           .Ease(EaseType.Linear));
		}
	}

	void TogglePause()
	{
		if(!paused)
		{
			HOTween.Pause();
			paused = true;
		}
		else
		{
			HOTween.Play();
			paused = false;
		}
	}
}
