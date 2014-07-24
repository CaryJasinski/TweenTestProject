using UnityEngine;
using System.Collections;

public class PutOnPathExample : MonoBehaviour{
	public Transform[] path;
	public float percentage;
	public float moveSpeed = 0;
	
	void OnGUI () {
		percentage=GUI.HorizontalSlider(new Rect(23,194,204,40),percentage,0,1);

		transform.LookAt(iTween.PointOnPath(path,percentage+.05f));
		//You can cause the object to orient to its path by calculating a spot slightly ahead on the path for a look at target:

	}
	
	void OnDrawGizmos(){
		iTween.DrawPath(path);
	}
	void Update()
	{

		iTween.PutOnPath(transform, path, percentage);
		transform.position = transform.forward*(moveSpeed*Time.deltaTime);
//		percentage += moveSpeed*Time.deltaTime;
//		iTween.PutOnPath(gameObject,path,percentage);
//		transform.LookAt(iTween.PointOnPath(path,moveSpeed+.05f));
	}
}

