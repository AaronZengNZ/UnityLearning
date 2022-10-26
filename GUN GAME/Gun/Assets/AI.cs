using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject sword;
    public GameObject player;
    public GameObject self;
    public GameObject gun;
    public GameObject me;
    public GameObject temp;
    public float rotationSpeed = 1f;
    public float movingCooldown = 0.2f;
    float moving = 0f;
    float e = 0f;
    public float moveSpeed = 5f;
    public float angleTolerance = 3f;
    float direction = 0f;
    bool afk = false;
    public bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //make the sword rotate towards the player
        sword.transform.rotation = Quaternion.Slerp(sword.transform.rotation, Quaternion.LookRotation(player.transform.position - sword.transform.position), rotationSpeed * Time.deltaTime);
        //if the sword is facing to the right of self, flip it's y scale
        if(sword.transform.rotation.eulerAngles.y > 90 && sword.transform.rotation.eulerAngles.y < 270){
            sword.transform.localScale = new Vector3(1f, -1f, 1f);
            
        }
        else{
            sword.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(move == false){
            return;
        }

        //if the gun's direction is the same as temp's direction (with tolerance)
        if(moving == 0f && gun.transform.rotation.eulerAngles.z > temp.transform.rotation.eulerAngles.z - angleTolerance && gun.transform.rotation.eulerAngles.z < temp.transform.rotation.eulerAngles.z + angleTolerance){
            //randomize direction
            direction = Random.Range(0, 360);
            moving = movingCooldown;
            afk = false;
        }
        else{
            if(moving == 0f){
            direction = -90 + Mathf.Atan2(player.transform.position.y - me.transform.position.y, player.transform.position.x - me.transform.position.x) * Mathf.Rad2Deg;
            direction += Random.Range(-30f, 30f);
            moving = movingCooldown;
            afk = true;
            }
        }
        if(moving > 0f){
            me.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            me.transform.position += me.transform.up * moveSpeed * Time.deltaTime;
            moving -= Time.deltaTime;
            e += Time.deltaTime;
            me.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if(afk == false){
            if(moving <= 0f){
                if(gun.transform.rotation.eulerAngles.z > temp.transform.rotation.eulerAngles.z - angleTolerance && gun.transform.rotation.eulerAngles.z < temp.transform.rotation.eulerAngles.z + angleTolerance){
                    moving = movingCooldown;
                }
            }
            if(e >= 1f){
                //make direction point at player
                direction = -90 + Mathf.Atan2(player.transform.position.y - me.transform.position.y, player.transform.position.x - me.transform.position.x) * Mathf.Rad2Deg;
            }
            }
        }
        if(moving <= 0f){
            e = 0f;
        }
        if(moving < 0f){
            moving = 0f;
        }
        //if the player is to the right of self, set the x scale of self to -1, else make it 1
        if(player.transform.position.x > self.transform.position.x){
            self.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else{
            self.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
