using UnityEngine;
using System.Collections;

public class s_MeteorMove : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.down * speed *Time.deltaTime;
		Destroy(this.gameObject, 5);
	}
}
