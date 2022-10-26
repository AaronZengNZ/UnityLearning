using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sord : MonoBehaviour
{
    public GameObject sword;
    public float knockback = 5f;
    public float originalPos = 6f;

    //on particle collision
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("e");
        if (other.gameObject.tag == "Gun")
        {
            Debug.Log("a");
            Gun gun;
            //set gun to the collision's parents parent
            if(other.gameObject.transform.parent.parent.parent.GetComponent<Gun>()){
                gun = other.gameObject.transform.parent.parent.parent.GetComponent<Gun>();
            }
            else{
                gun = other.gameObject.transform.parent.parent.GetComponent<Gun>();
            } 
            //change x position of sword by gun's damage multiplied by -0.01
            sword.transform.position = new Vector3(sword.transform.position.x + gun.damage * -0.01f * knockback, sword.transform.position.y, sword.transform.position.z);
        }
    }

    void Update(){
        //if sword's x position is less than 0, change it by time.deltatime * 5
        //if(sword.transform.position.x < originalPos){
        //    sword.transform.position = new Vector3(sword.transform.position.x + Time.deltaTime * 5, sword.transform.position.y, sword.transform.position.z);
        //}
        //if sword is more than 0, set it to 0
        //if(sword.transform.position.x > originalPos){
        //    sword.transform.position = new Vector3(originalPos, sword.transform.position.y, sword.transform.position.z);
        //}
    }
}
