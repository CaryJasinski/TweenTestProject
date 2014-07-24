using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	void OnDrawGizmos(){
		Gizmos.color=Color.magenta;
		Gizmos.DrawWireCube(transform.position,transform.localScale);
	}
}
