using UnityEngine;
using System.Collections;

public class s_MeteorManager : MonoBehaviour {

	public GameObject[] Meteors;
	public float timer;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnMeteors());
	}
	
	IEnumerator SpawnMeteors(){
		int ranNum = Random.Range(0, Meteors.Length);
		Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 6, 1);
		GameObject Meteor = (GameObject) Instantiate(Meteors[ranNum], spawnPos, transform.rotation);
		Meteor.GetComponent<s_MeteorMove>().speed = Random.Range(3, 6);
		yield return new WaitForSeconds(timer);
		StartCoroutine(SpawnMeteors());
	}
}



