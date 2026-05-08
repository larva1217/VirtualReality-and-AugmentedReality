using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static int kill=0;
    GameObject player;
    NavMeshAgent navMesh;
    Animator ani;
    int HP;
    bool isAttack=false;

    AudioSource audioSource;
    public AudioClip swordSound;  
    public AudioClip hitSound;
    public AudioClip DieSound; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP=100;
        navMesh = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.Find("XR Origin (XR Rig)");
        navMesh.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance<=2.0f){
            navMesh.isStopped = true;
            if(isAttack==false){
                ani.SetBool("Idle",true);
                StartCoroutine(Attack());
            }
        }
        else {
        navMesh.isStopped = false;
        navMesh.destination = player.transform.position;
        ani.SetBool("Idle", false);
    }
    }

    IEnumerator Attack(){
        isAttack=true;
        ani.SetBool("Attack", true);

        yield return new WaitForSeconds(1.0f); // 공격 타이밍
        audioSource.PlayOneShot(hitSound);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.TakeDamage(10); // 데미지 주기

        yield return new WaitForSeconds(4.0f); // 쿨타임
        isAttack = false;
        ani.SetBool("Attack", false);
    }      

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Sword")){
            HP -= 10;
            audioSource.PlayOneShot(swordSound);
            if(HP <= 0f){
                kill+=1;
                audioSource.PlayOneShot(DieSound);
                PlayerStats stats = player.GetComponent<PlayerStats>();
                stats.AddGold(10);
                Destroy(gameObject);
            }
        }
    }
}
