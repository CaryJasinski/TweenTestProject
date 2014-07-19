using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;
using UnityEngine;
using System.Collections;

/// <summary>
/// Some example of how to create tweens with HOTween.
/// To learn more and find more examples, go to http://hotween.holoville.com
/// </summary>
public class HOPathDemoBrain : MonoBehaviour
{
    public Transform CubeTrans1;
    public Transform CubeTrans2;
	public bool paused = false;
    // ===================================================================================
    // UNITY METHODS ---------------------------------------------------------------------

    void Start()
    {
        // HOTWEEN INITIALIZATION
        // Must be done only once, before the creation of your first tween
        // (you can skip this if you want, and HOTween will be initialized automatically
        // when you create your first tween - using default values)
        HOTween.Init(true, true, true);

        // TWEEN CREATION
        // With each one is shown a different way you can create a tween,
        // so that in the end you can choose what you prefer

        // Tween the first transform using fast writing mode,
        // and applying an animation that will last 4 seconds


	    HOTween.To(CubeTrans1, 20, new TweenParms()
			.Prop( "position", CubeTrans1.GetComponent<HOPath>().MakePlugVector3Path().OrientToPath())
			.Loops(-1, LoopType.Restart)
			.Ease(EaseType.Linear));
	    //HOTween.To(CubeTrans2, 2, new TweenParms()
		//	.Prop( "position", CubeTrans2.GetComponent<HOPath>().MakePlugVector3Path(), true)
		//	.Loops(-1, LoopType.Yoyo)
		//	.Ease(EaseType.Linear));
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
