using UnityEngine;
using System.Collections;

public class PathPoint : MonoBehaviour {
	void OnDrawGizmos(){
		Gizmos.color=Color.green;
		Gizmos.DrawWireSphere(transform.position,.25f);
	}
}
