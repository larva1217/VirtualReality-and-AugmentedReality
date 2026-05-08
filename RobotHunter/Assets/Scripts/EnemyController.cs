using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject hitEffect;

    GameObject player;
    NavMeshAgent navMesh;
    Animator ani;
    int HP;
    bool isAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP=100;
        player=GameObject.Find("XR Origin (XR Rig)");
        navMesh = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        navMesh.destination=player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance <= 2.0f){
            navMesh.isStopped = true;
            if(isAttack == false){
                ani.SetBool("Idle",true);
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack(){
        isAttack = true;
        yield return new WaitForSeconds(3.0f);
        ani.SetBool("Attack",true);
        yield return new WaitForSeconds(0.5f);

        player.GetComponent<PlayerController>().ApplyDamage(10);
        isAttack = false;
        ani.SetBool("Attack",false);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Bullet")){
            GameObject effect = Instantiate(hitEffect,other.transform.position,other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(effect,2.0f);
            HP-=10;
            if(HP<=0f){
                Destroy(gameObject);
                player.GetComponent<PlayerController>().ScoreUP(100);
            }
        }
    }
}
