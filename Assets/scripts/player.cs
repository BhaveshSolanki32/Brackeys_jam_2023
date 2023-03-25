using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float speed = 0.1f;
    public static player manager;
    public float health = 100;
    public GameObject play_er;
    public GameObject crosshair;
    public GameObject missile;
    public Transform ammo;
    public float cooldown = 0.5f;
    float can_fire = 0;
    public Slider health_slider;
    public GameObject blast_particle;
    public bool attacking = false;
    bool can_attack_turt = false;
    public GameObject turtle_particle;
    GameObject particle_;
    void Start()
    {
        manager = this;
        Cursor.visible = false;
    }

    public void damage(float val)
    {
        if (attacking) return;

        health -= val;
        GetComponentInChildren<Renderer>().material.color = Color.red;
        health_slider.value = health;
        if (health <= 0)
            gameplay_manager.manager.dead();
        Invoke("white_color", 0.2f);
    }
    void white_color()
    {

        if (this.gameObject.name == "crow")
        {

            GetComponentInChildren<Renderer>().material.color = new Color(0.1803922f, 0.1803922f, 0.1803922f);
        }

        else
            GetComponentInChildren<Renderer>().material.color = Color.white;
    }
    private void Update()
    {
        if (!attacking && particle_ != null && name!="human")
            Destroy(particle_);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -91, 91), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -94, 94));
    }
    private void LateUpdate()
    {
        can_attack_turt = attacking;
    }
    void FixedUpdate()
    {

        play_er.transform.position = transform.position;
        if( (!attacking && (name == "turtle" || name == "crow"))||  name=="human")
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
        }
        


        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 38.17f));
        crosshair.transform.position = mouseScreenPos;
        Vector3 direction = mouseWorldPos - transform.position;


        if (Input.GetMouseButton(0) && can_fire >= cooldown)
        {
            attacking = true;
            if (gameplay_manager.manager.current_gen == 2)
                StartCoroutine(missile_launch(mouseWorldPos, Instantiate(missile, ammo.transform.position, Quaternion.identity)));
            else if (gameplay_manager.manager.current_gen == 1)
            {
                StopAllCoroutines();
                StartCoroutine(turtle_attack(mouseWorldPos));
            }
            else if (gameplay_manager.manager.current_gen == 0)
            {
                StopAllCoroutines();
                StartCoroutine(crow_hit(mouseWorldPos));
            }

        }

        can_fire += (can_fire > cooldown) ? (0) : (0.05f);
        if (!attacking)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationAngle - ((this.name == "crow") ? (90) : (0)), 0), 1);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (attacking)
        {


            if (name == "crow" && other.gameObject.name == "wall")
            {
                StopAllCoroutines();
                Collider[] enemies;
               particle_=  Instantiate(blast_particle, transform.position, Quaternion.identity);
                enemies = Physics.OverlapSphere(transform.position, 2.8f);
                foreach (Collider x in enemies)
                {
                    if (x.gameObject.tag == "enemy")
                        x.gameObject.GetComponent<enemy>().damage(50);
                    if (x.gameObject.tag == "npc")
                        x.gameObject.GetComponent<npc>().damage(50);
                    GetComponent<Animator>().SetBool("attack", false);
                }
                attacking = false;
            }
            else if (name == "turtle" && !can_attack_turt)
            {

                if ((other.gameObject.name == "wall" || other.gameObject.tag == "enemy" || other.gameObject.tag == "npc"))
                {
                    Debug.Log("turtle attack");
                    if (other.gameObject.tag == "enemy")
                        other.gameObject.GetComponent<enemy>().damage(50);
                    if (other.gameObject.tag == "npc")
                        other.gameObject.GetComponent<npc>().damage(50);
                    if (other.gameObject.name == "wall")
                    {
                        StopAllCoroutines();
                        attacking = false;
                        GetComponent<Animator>().SetBool("attack", false);
                    }

                }
            }




        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( attacking)
        {            
            if (name == "crow" && other.gameObject.name == "wall")
            {
                StopAllCoroutines();
                Collider[] enemies;
                particle_=  Instantiate(blast_particle, transform.position, Quaternion.identity);
                enemies = Physics.OverlapSphere(transform.position, 3f);
                foreach (Collider x in enemies)
                {
                    if (x.gameObject.tag == "enemy")
                        x.gameObject.GetComponent<enemy>().damage(50);
                    if (x.gameObject.tag == "npc")
                        x.gameObject.GetComponent<npc>().damage(50);
                    GetComponent<Animator>().SetBool("attack", false);
                }
                attacking = false;
            }
           else if (name == "turtle")
            {
                
                if ((other.gameObject.name == "wall" || other.gameObject.tag == "enemy" || other.gameObject.tag == "npc"))
                {
                    Debug.Log("turtle attack");
                    if (other.gameObject.tag == "enemy")
                        other.gameObject.GetComponent<enemy>().damage(50);
                    if (other.gameObject.tag == "npc")
                        other.gameObject.GetComponent<npc>().damage(50);
                    if (other.gameObject.name == "wall")
                    {
                        StopAllCoroutines();
                        attacking = false;

                        GetComponent<Animator>().SetBool("attack", false);
                    }

                }
            }
           
           

            
        }
    }
    IEnumerator crow_hit(Vector3 pos)
    {

        can_fire = -2;
        GetComponent<Animator>().SetBool("attack", true);
        while (transform.position != pos)
        {
            Vector3 direction = pos - transform.position;
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.normalized.x, direction.normalized.z) * Mathf.Rad2Deg - 90, 0);
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
            yield return new WaitForSeconds(0.02f);
        }
        Instantiate(blast_particle, transform.position, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(new Vector3(transform.position.x, 1, transform.position.z), 3.1f);
        
        foreach (Collider x in enemies)
        {
            
            if (x.gameObject.tag == "enemy")
                x.gameObject.GetComponent<enemy>().damage(50);
            if (x.gameObject.tag == "npc")
                x.gameObject.GetComponent<npc>().damage(50);
        }
        attacking = false;
        GetComponent<Animator>().SetBool("attack", false);
        yield return null;
    }

    IEnumerator missile_launch(Vector3 pos, GameObject missi)
    {
        can_fire = -0.7f;
        Vector3 direction = pos - missi.transform.position;
        missi.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
        
        while (missi.transform.position != pos)
        {
            missi.transform.position = Vector3.MoveTowards(missi.transform.position, pos, 0.6f);
            yield return new WaitForSeconds(0.002f);
        }
        Collider[] enemies = Physics.OverlapSphere(missi.transform.position, 6);
        foreach (Collider x in enemies)
        {
            if (x.gameObject.tag == "enemy")
                x.gameObject.GetComponent<enemy>().damage(50);
            if (x.gameObject.tag == "npc")
                x.gameObject.GetComponent<npc>().damage(50);
        }

        particle_=  Instantiate(blast_particle, missi.transform.position, Quaternion.identity);
        Destroy(missi);
        attacking = false;
        yield return null;
    }


    IEnumerator turtle_attack(Vector3 pos)
    {
        attacking = true;
        can_fire = -1.2f;
        particle_ = Instantiate(turtle_particle, transform.position, turtle_particle.transform.rotation);
        GetComponent<Animator>().SetBool("attack", true);
        pos = new Vector3(pos.x, transform.position.y, pos.z);
        for (int i = 0; i < 20 && transform.position!=pos; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
            particle_.transform.position = new Vector3(transform.position.x, 0.78f, transform.position.z);
            yield return new WaitForSeconds(0.02f);
        }
        attacking = false;
        Destroy(particle_);
        GetComponent<Animator>().SetBool("attack", false);
        yield return null;
    }

}
