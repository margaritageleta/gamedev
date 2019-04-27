using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManager : MonoBehaviour {

    public Transform[] patrolPoints;
    private bool[] pointOccupation; //true=point is free,  false=point is occupied/someone else is going
    private List<Shooting> hunterList;
    int N=0;

    private bool settled = false;





	// Use this for initialization
	void Start () {
        System.Array.Resize(ref pointOccupation, patrolPoints.Length);
        for (int i=0;i<pointOccupation.Length;i++)
        {
            pointOccupation[i] = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i=0; i<N; i++)
        {
            if (!hunterList[i].hasDestination)
            {
                settled = false;

                while (!settled)
                {
                    int index = Random.Range(0, N);
                    if (pointOccupation[index])
                    {
                        settled = true;
                        hunterList[i].TargetDest = patrolPoints[i].position;
                        pointOccupation[index] = false;
                    }
                }

            }
        }
		
	}


    public void GetHunter(Shooting newHunter)
    {
        hunterList.Add(newHunter);
        N++;
    }


}
