using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_Player : MonoBehaviour {

	public float playerHealth;
	public Slider healthSlider;

	void Start(){
		healthSlider = GameObject.Find("Player Health").GetComponent<Slider>();
		healthSlider.value = playerHealth;
	}

	public void TakenDamage(float damage){
		playerHealth -= damage;
		healthSlider.value = playerHealth;
		if(playerHealth == 0){
			Destroy(this.gameObject);
		}
	}
}
