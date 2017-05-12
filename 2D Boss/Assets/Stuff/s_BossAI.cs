using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum BossState {
	BossSpawn, // Decend
	BossTalk, // Oh its you
	MoveToPos, // Moves up
	SpawnMidLaser, // Spawns Mid Screen Laser
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
	public bool canShoot = true;
	public float shootTimer;
	private float nextFire;


	[Header("Boss Spawn")]
	public Vector2 centerPos;
	public float startSpeed;

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
			canShoot = false;
			transform.position = Vector2.MoveTowards(transform.position, centerPos, startSpeed*Time.deltaTime);
			if(curPos == centerPos){
				moveLeft = true;
				bossState = BossState.StrafeRun;
			}
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
				transform.position = Vector2.MoveTowards(transform.position, centerPos, (strafeSpeed/2)*Time.deltaTime);
				if(curPos == centerPos){					
					bossState = BossState.RotatingLasers;
				}
			}

		}
		if(bossState == BossState.RotatingLasers){
			print("Dank shooting spinning move");;
		}

		////////// Shooting /////////////////

		if(canShoot && Time.time > nextFire){
			ShootLaser();
			nextFire = Time.time + shootTimer;
		}


	}

	void BossSpawn(){
		// UI Text Manager
		// 'Oh, it's you again'
		bossState = BossState.BossSpawn;
	}

	public void TakenDamage(float damage){
		Bosshealth -= damage;
		healthSlider.value = Bosshealth;
		if(Bosshealth == 0){
			Destroy(this.gameObject);
		}
	}

	void ShootLaser(){
		Instantiate(Laser, LaserSpawnPos.transform.position, Quaternion.AngleAxis(180, Vector3.right));
	}
}
