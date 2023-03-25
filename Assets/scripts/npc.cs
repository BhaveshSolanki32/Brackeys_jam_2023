using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public float health = 50;
    private void Start()
    {
        StartCoroutine(run());
    }

    public void damage(float amount)
    {
        health -= amount;
        GetComponentInChildren<Renderer>().material.color = Color.red;
        if (health <= 0)
        {
            gameplay_manager.manager.karma_handler(-1.5f);
            Destroy(this.gameObject);
        }
        Invoke("colortowhite", 0.2f);
    }
    void colortowhite() => GetComponentInChildren<Renderer>().material.color = Color.white;

    IEnumerator run()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            Vector3 loc2 = vect_rand(15);
            Vector3 loc3 = vect_rand(15);
            Vector3 loc4 = vect_rand(100);
            Vector3 player_post = player.manager.gameObject.transform.position;
            List<Vector3> pointList = vertexpos(transform.position, loc2+new Vector3(player_post.x,transform.position.y,player_post.z), loc3 + new Vector3(player_post.x, transform.position.y, player_post.z), loc4);
            pointList.Insert(0, transform.position);

            for (int i = 1; i < pointList.Count; i++)
                Debug.DrawLine(pointList[i - 1], pointList[i], Color.red, 40);

            for (int i = 1; i < pointList.Count; i++)
            {
                while (transform.position != pointList[i])
                {
                    transform.position = Vector3.MoveTowards(transform.position, pointList[i], 0.7f);
                    transform.LookAt(pointList[i]);
                    yield return new WaitForSeconds(0.02f);
                }

            }
        }
        
    }

    private Vector3 vect_rand(int amount)
    {

        Vector3 rand = new Vector3((Random.insideUnitCircle.x) * amount, 0,( Random.insideUnitCircle.y )* amount);
        return rand;
    }



    Vector3 quadlerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }
    Vector3 cubiclerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 abc = quadlerp(a, b, c, t);
        Vector3 bcd = quadlerp(b, c, d, t);

        return Vector3.Lerp(abc, bcd, t);
    }
    List<Vector3> vertexpos(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        List<Vector3> pointList = new List<Vector3>();
        for (float ratio = 0.14f; ratio <= 1; ratio += 0.14f)
        {
            Vector3 curve = cubiclerp(a, b, c, d, ratio);
            pointList.Add(curve);
        }
        return pointList;
    }
}
