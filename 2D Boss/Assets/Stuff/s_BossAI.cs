using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum BossState {
	Default, // Blank State
	BossSpawn, // Decend
	BossTalk, // Oh its you
	MoveToPos, // Moves up
	SpawnMidLaser, // Mid Screen Laser
 	StrafeRun, // Strafe
	RotatingLasers // Dank Spinning move
}

public class s_BossAI : MonoBehaviour {

	[Header("Boss State")]
	public BossState bossState;

	[Header("Boss Stuff")]
	public float Bosshealth = 50;
	public Slider healthSlider;
	private Vector2 curPos;

	[Header("Shooting")]
	public GameObject LaserSpawnPos;
	public GameObject Laser;
	private bool canShoot = false;
	public float shootTimer;
	private float nextFire;

	[Header("Boss Spawn")]
	public Vector2 centerPos;
	public Vector2 middlePos;
	public float startSpeed;
	public string IntroText;
	public GameObject midLaser;
	private bool rainLaser = true;
	private bool sendUIText = true;

	[Header("StrafeRun")]
	public Vector2[] strafeEdge;
	private bool moveLeft, moveRight;
	public float strafeSpeed;
	private int strafeCount;

	[Header("RotatingLasers")]
	public float rotatingSpeed;
	public float _angle;



	// Use this for initialization
	void Start () {
		healthSlider = GameObject.Find("Boss Health").GetComponent<Slider>();
		healthSlider.value = Bosshealth;
		BossSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		curPos = transform.position;
		////////// Boss States /////////////////	
		if(bossState == BossState.BossSpawn){
			transform.position = Vector2.MoveTowards(transform.position, middlePos, (startSpeed*1.5f)*Time.deltaTime);
			if(curPos == middlePos){
				bossState = BossState.BossTalk;
			}
		}
		if(bossState == BossState.BossTalk){
			if(sendUIText){
				StartCoroutine(SayIntroText());
				sendUIText = false;
			}
		}
		if(bossState == BossState.MoveToPos){
			canShoot = false;
			transform.position = Vector2.MoveTowards(transform.position, centerPos, startSpeed*Time.deltaTime);
			if(curPos == centerPos){
				bossState = BossState.SpawnMidLaser;
			}
		} 
		if(bossState == BossState.SpawnMidLaser){
			print("Pew pew oh look a laser in the middle there oh noooo");
			moveLeft = true;
			bossState = BossState.StrafeRun;
		}
		if(bossState == BossState.StrafeRun){
			canShoot = true;
			if(moveLeft){
				transform.position = Vector2.MoveTowards(transform.position, strafeEdge[0], strafeSpeed*Time.deltaTime);
				if(curPos == strafeEdge[0]){
					moveLeft = false;
					moveRight = true;
				}

			} if(moveRight){
				transform.position = Vector2.MoveTowards(transform.position, strafeEdge[1], strafeSpeed*Time.deltaTime);
				if(curPos == strafeEdge[1]){
					moveLeft = true;
					moveRight = false;
					strafeCount += 1;
				}
			}
			if(strafeCount == 3){
				canShoot = false;
				bossState = BossState.RotatingLasers;
			}

		}
		if(bossState == BossState.RotatingLasers){
			transform.position = Vector2.MoveTowards(transform.position, centerPos, strafeSpeed*Time.deltaTime);
			print("Dank shooting spinning move");;
		}

		////////// Shooting /////////////////

		if(canShoot && Time.time > nextFire){
			ShootLaser();
			nextFire = Time.time + shootTimer;
		}
	}

	void BossSpawn(){
		canShoot = false;
		bossState = BossState.Default;
		StartCoroutine(RainLasers());
		StartCoroutine(StopLaser());

	}

	public void TakenDamage(float damage){
		Bosshealth -= damage;
		healthSlider.value = Bosshealth;
		if(Bosshealth == 0){
			Destroy(this.gameObject);
		}
	}

	void ShootLaser(){
		shootTimer = Random.Range(0.1f, 1f);
		Instantiate(Laser, LaserSpawnPos.transform.position, Quaternion.AngleAxis(180, Vector3.right));
	}

	IEnumerator RainLasers(){
		if(rainLaser){
			float ranTimer = Random.Range(0.1f, 0.5f);
			Vector3 spawnPos = new Vector3(Random.Range(-8.5f, 8.5f), 6, 0);
			Instantiate(Laser, spawnPos, Quaternion.AngleAxis(180, Vector3.right));
			yield return new WaitForSeconds(ranTimer);
			StartCoroutine(RainLasers());
		}
	}

	IEnumerator StopLaser(){
		yield return new WaitForSeconds(30f);
		rainLaser = false;
		yield return new WaitForSeconds(2.5f);
		bossState = BossState.BossSpawn;
	}

	IEnumerator SayIntroText(){
		// Text to UI text
		print("Oh, It's you again");
		yield return new WaitForSeconds(2f);
		// Text off
		bossState = BossState.MoveToPos;
	}
}
