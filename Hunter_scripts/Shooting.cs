using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    
    private bool chasing=false;   
    bool visible = false;
    public float KeepWatchingDistance = 50f; //further than this, he won't look at you
    public float chasingTime=30; //in seconds

    public float fireRate=3;
    private float nextFire=0;
    

    public Vector3 TargetDest;
    private GameObject target;
    UnityEngine.AI.NavMeshAgent nav;
    public bool hasDestination=false;

    public GameObject fireLight;
    

    //for shoot:
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    



    // Use this for initialization
    void Awake() {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        

    }

    // Update is called once per frame
    void Update() {
        Move();

        


        if (visible)
        {
            Ray myRay = new Ray(transform.position, target.transform.position - transform.position);
            RaycastHit hit;
            Physics.Raycast(myRay, out hit, KeepWatchingDistance); 
           // Debug.DrawLine(myRay.origin, hit.point);
            transform.LookAt(hit.point);  //this is what works the best
        }
        
    }


    void Move()
    {
        if (chasing)
        {
            nav.SetDestination(target.transform.position);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }

        else
        {
            nav.SetDestination(TargetDest);
            if ((transform.position - TargetDest).magnitude < 1)
                hasDestination = true;
        }
    }

    

    void Shoot()
    {
        fireLight.SetActive(true);
        Invoke("DisableShootingEffects", 0.1f);
        Ray gunRay = new Ray(transform.position,transform.forward);
        RaycastHit hit2;

        if (Physics.Raycast(gunRay, out hit2, KeepWatchingDistance)) { }
         //   Debug.DrawLine(gunRay.origin, hit2.point);
       // else
          //  Debug.DrawLine(gunRay.origin, transform.forward+new Vector3(30f,0f,30f));




        //       if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        //       {
        //           EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
        //            if (enemyHealth != null) //Por si lo que llega es a la pared
        //         {
        //enemyHealth.TakeDamage(damagePerShot, shootHit.point);//TakeDamage era una función pública
        //}

        //}


        // }


        //}

    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==9) { //OJO
            chasing = true;
            Invoke("SetBoolBack", chasingTime);
            visible = true;
            target = other.gameObject;

        }
    }




    private void SetBoolBack()
    {
        chasing = false;
        visible = false;
    }

    private void DisableShootingEffects()
    {
        fireLight.SetActive(false);
       
    }
}








