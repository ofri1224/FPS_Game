using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemy_script : CharacterStats
{
    [Header("Animator/Ai Brain")]
    public Animator animator;
    private Vector3 startPos;
    [Range(0.0f,1.0f)]
    public float Shotaccuracy;
    public Vector3[] RoamPoints;
    public float min_distance_from_roamNode;
    private int Roampoints_index=0;
    public bool chasing,investigating,EnemyInView,retreating;
    private Vector3 direction;
    [Range(0,360)]
    public float fov;
    private float distanceFromTarget;
    public float LookRadius =10f;
    [HideInInspector]
    public Vector3 LastSeenLocation;
    Transform target;
    NavMeshAgent agent;
    private bool Roamer=false;
    private bool Melee;
    private void Start() {
        startPos=transform.position;//save starting position
        target = PlayerManager.instance.Player.transform;//get Player
        agent=GetComponent<NavMeshAgent>();
        if (RoamPoints.Length>0)
        {
            Roamer = true;
            animator.SetBool("Roamer",true);
        }
        else{
            Roamer = false;
            animator.SetBool("Roamer",false);
        }
    }
        private void FixedUpdate()
    {
        // Give the values to the FSM (animator)
        Melee=(selectedWeapon==null);
        animator.SetBool("Melee",Melee);
        if (Roamer)
        {
            distanceFromTarget = Vector3.Distance(transform.position,RoamPoints[Roampoints_index]);
            animator.SetFloat("Distance_from_Waypoint", distanceFromTarget);
        }
        animator.SetInteger("CurrentHealth",CurrentHealth);
        animator.SetBool("Enemy_In_Sight", EnemyInView);
        if (EnemyInView)
        {
            animator.SetFloat("Distance_From_Enemy", Vector3.Distance(transform.position,LastSeenLocation));
        }
        if (!Melee)
        {
            animator.SetInteger("Bullets_In_Gun",selectedWeapon.current_mag_capacity);
        }
    }
    private void Update() {
        if (chasing)
        {
            //call Chase state
            agent.SetDestination(LastSeenLocation);
            // direction=-(transform.position-LastSeenLocation);
            // FaceDirection(direction.normalized);
        }
        if (retreating)
        {
            agent.SetDestination(transform.position+(transform.position-LastSeenLocation).normalized);
            // direction=-(transform.position-LastSeenLocation);
            // FaceDirection(direction.normalized);
        }
        if (EnemyInView)
        {
            direction=-(transform.position-LastSeenLocation);
            FaceDirection(direction.normalized);
        }
        FindVisibleTargets();
    }
    //find targets and check if visable
    void FindVisibleTargets(){
        Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position,LookRadius,universal_vars.instance.EntityLayer); // get all entitys in radius
        EnemyInView=false;//set default value
        if (TargetsInViewRadius.Length>0)// check if any entitys in radius
        {
           foreach (Collider target in TargetsInViewRadius)
            {
                if (Team!=target.gameObject.GetComponent<CharacterStats>().Team)// check team
                {
                    Vector3 targetDir = -(transform.position-target.transform.position);//get relative direction of entity
                    float angle = Vector3.Angle(targetDir,transform.forward);//get relative angle of entity
                    if (angle<=fov/2&&angle>=(-fov/2))// check if in field of view
                    {
                        Debug.DrawRay(transform.position,targetDir,Color.red,0.01f);
                        if(!Physics.Raycast(transform.position,targetDir,out RaycastHit Hit,Vector3.Distance(transform.position,target.transform.position),universal_vars.instance.ObstacleLayer)){ //check if view blocked
                            //entity in line of sight
                            LastSeenLocation=target.transform.position;//set last seen location
                            EnemyInView=true;//set var value
                        }
                        else{
                            Debug.Log(Hit.transform.name);
                        }
                    }
                }
            } 
        }
    }

    public void ShootAtEnemy(){
        Debug.Log("shot");
        selectedWeapon.shoot();
        animator.SetFloat("Next_Time_To_Fire",1f/selectedWeapon.fireRate);
    }
    public void ReloadGun(){
        selectedWeapon.reload();
        animator.SetFloat("Next_Time_To_Fire",selectedWeapon.reloadTime);
    }

    public void SetNextRoamPoint(){
        if (Roampoints_index+1>=RoamPoints.Length)
        {
            Roampoints_index=0;
        }
        else{
            Roampoints_index++;
        }
        agent.SetDestination(RoamPoints[Roampoints_index]);
    }
    //turn to face direction
    void FaceDirection(Vector3 direction)
    {
        Quaternion LookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,LookRotation,Time.deltaTime*5f);
    }
    public void StartChasing()
    {
        chasing = true;
    }
   public void StopChasing()
    {
        chasing = false;
    }
    public void StartRetreating()
    {
        retreating = true;
    }
   public void StopRetreating()
    {
        retreating = false;
    }
    public Vector3 angle2direction(float angle,bool isGlobal){
        if (!isGlobal)
        {
            angle+=transform.eulerAngles.y;
        }
        
        return new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad),0,Mathf.Cos(angle*Mathf.Deg2Rad));
    }
}
