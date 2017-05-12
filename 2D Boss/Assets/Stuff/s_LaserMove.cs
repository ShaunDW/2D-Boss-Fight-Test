using UnityEngine;
using System.Collections;

public class s_LaserMove : MonoBehaviour {

	public float laserSpeed;
	public float laserDamage;
	public string targetTag;
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0, laserSpeed*Time.deltaTime,0);
		Destroy(this.gameObject, 2.5f);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == targetTag){
			Destroy(this.gameObject);
			if(targetTag =="Player"){
				col.gameObject.GetComponent<s_Player>().TakenDamage(laserDamage);
			} else if(targetTag == "Enemy"){
				col.gameObject.GetComponent<s_BossAI>().TakenDamage(laserDamage);
			}
		}
	}
}
