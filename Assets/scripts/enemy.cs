using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health=100;
    public float speed =0.2f;
    Material[] mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().materials;
        StartCoroutine(chase());
    }

     IEnumerator chase()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.manager.transform.position.x, 0, player.manager.transform.position.z), speed);
            Quaternion rotx = transform.rotation;
            transform.LookAt(player.manager.transform);
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void damage(float amount)
    {
        health -= amount;

       

        foreach (Material x in mat)
            if(x!=null)
            x.color = Color.red;


        if (health <= 0)
        {
            Destroy(gameObject);
            gameplay_manager.manager.karma_handler(1);
        }
            
        Invoke("color_to_white", 0.2f);
    }
    void color_to_white()
    {
        mat[0].color = new Color(0.799f,0.799f,0.799f);
        mat[1].color = new Color(0.529f,0.001f,0);
        mat[2].color = new Color(0.181f,0.013f,0.238f);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag=="Player"  && !player.manager.attacking)
        {
            player.manager.damage(5);
            StopAllCoroutines();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(chase());
        }
    }
}
