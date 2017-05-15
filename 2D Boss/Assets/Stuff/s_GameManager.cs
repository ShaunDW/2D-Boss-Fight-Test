using UnityEngine;
using System.Collections;

public class s_GameManager : MonoBehaviour {

	public GameObject Boss;
	private Vector3 spawnPos = new Vector3(0,6,0);

	// Use this for initialization
	void Start () {
		BossFight();
	}

	void BossFight(){
		GameObject Monster = (GameObject) Instantiate(Boss, spawnPos, transform.rotation);
		Monster.name = "Boss";
	}


}
