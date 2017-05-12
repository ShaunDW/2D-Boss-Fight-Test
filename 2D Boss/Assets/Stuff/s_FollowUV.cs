using UnityEngine;
using System.Collections;

public class s_FollowUV : MonoBehaviour {
		
	public float parralax = 2f;

	void Update () {
		MeshRenderer mr = GetComponent<MeshRenderer>();
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;
		offset.y += Time.deltaTime/parralax;
		mat.mainTextureOffset = offset;
	}
}
