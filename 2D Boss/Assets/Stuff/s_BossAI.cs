using UnityEngine;
using System.Collections;

public enum BossState {
	BossSpawn,
 	StrafeRun,
	RotatingLasers
}


public class s_BossAI : MonoBehaviour {

	[Header("Boss State")]
	public BossState bossState;

	[Header("Boss Stuff")]
	public int BossHealth = 50;
	public GameObject LaserSpawnPos;
	public GameObject Laser;

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
		BossSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		if(bossState == BossState.BossSpawn){
			transform.position = Vector2.MoveTowards(transform.position, centerPos, startSpeed*Time.deltaTime);
			Vector2 curPos = transform.position;
			if(curPos == centerPos){
				moveLeft = true;
				bossState = BossState.StrafeRun;
			}
		} 
		if(bossState == BossState.StrafeRun){
			if(moveLeft){
				transform.position = Vector2.MoveTowards(transform.position, strafeEdge[0], strafeSpeed*Time.deltaTime);
				Vector2 curPos = transform.position;
				if(curPos == strafeEdge[0]){
					moveLeft = false;
					moveRight = true;
				}

			} if(moveRight){
				transform.position = Vector2.MoveTowards(transform.position, strafeEdge[1], strafeSpeed*Time.deltaTime);
				Vector2 curPos = transform.position;
				if(curPos == strafeEdge[1]){
					moveLeft = true;
					moveRight = false;
					strafeCount += 1;
				}
			}

		//	 if(can shoot){
		//		Shooty shooty code goes here
		//	}

			if(strafeCount == 3){
				//stop shooting
				transform.position = Vector2.MoveTowards(transform.position, centerPos, strafeSpeed*Time.deltaTime);
				Vector2 curPos = transform.position;
				if(curPos == centerPos){					
					bossState = BossState.RotatingLasers;
				}
			}

		}
		if(bossState == BossState.RotatingLasers){
			print("Dank shooting spinning move");
//			Vector2 dir = Vector3.forward.normalized;
//			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//			angle -= _angle;
//			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotatingSpeed*Time.deltaTime);
		}
	}

	void BossSpawn(){
		// UI Text Manager
		// 'Oh, it's you again'
		bossState = BossState.BossSpawn;
	}
}
