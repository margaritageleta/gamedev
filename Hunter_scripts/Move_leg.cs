using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_leg : MonoBehaviour
{

    public float slerpCoefficient = 60f;   // Speed of leg motion.
    public bool goingUp = true;
    public float switchDirection = 1f;
    //public float legLife = 0f;


    // Update is called once per frame
    void Update() {

        //legLife += Time.deltaTime;
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.x = switchDirection*(Mathf.PingPong(Time.time * slerpCoefficient, 60f)) - switchDirection*30f;
        if (slerpCoefficient == 0) rotation.x = 0f;
        transform.localRotation = Quaternion.Euler(rotation);


        /*
        rotation = new Vector3(Mathf.PingPong(Time.deltaTime, 30), transform.localRotation.y, transform.localRotation.z);
        rotation.x = Mathf.Clamp(switchDirection * (rotation.x + Time.deltaTime * slerpCoefficient), -30f, 30f);

        if (rotation.x.Equals(30f) || rotation.x.Equals(-30f)) slerpCoefficient *= -1f;
        Debug.Log(rotation.x.Equals(0f));
        if (rotation.x.Equals(0f))switchDirection *= -1 ;
        Debug.Log(switchDirection);
        Debug.Log(Vector3.Dot(Vector3.right, rotation));

        if (Vector3.Dot(Vector3.right, rotation) == 30) slerpCoefficient *= -1;
        else if (Vector3.Dot(Vector3.right, rotation) <= 1 && Vector3.Dot(Vector3.right, rotation) >=0) slerpCoefficient *= -1;
*/
        /*
    if (goingUp){
        Debug.Log("IN IF");
        if (Vector3.Dot(Vector3.right, rotation) == 30) slerpCoefficient *= -1;
        else if (Vector3.Dot(Vector3.right, rotation) <= 1 && slerpCoefficient==1){
            Debug.Log("ENTROO");
            return;
            goingUp = false;
              switchDirection *= -1;
        }
     }
     else {
         Debug.Log("IN ELSE");
        if (Vector3.Dot(Vector3.right, rotation) == -30) slerpCoefficient *= -1;
          else if (Vector3.Dot(Vector3.right, rotation) == -1)
          {
            Debug.Log("IN ELSEqqq");
            switchDirection *= -1;
              goingUp = true;
          }
      }
      */
    }
}