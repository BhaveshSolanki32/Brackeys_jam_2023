using System.Collections;
using UnityEngine;

public class enemy_manager : MonoBehaviour
{
    public static enemy_manager manager;
    public float spawn_rate = 5;
    [SerializeField] GameObject enemy;
    void Start()
    {
        manager = this;
        StartCoroutine(spawn());
        StartCoroutine(spawn());
        StartCoroutine(spawn());
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            int ran = Random.Range(1, 5);
            float x = 52.4f, z = 33.6f ;
            Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), Color.green,40);
            Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(-x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
            Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
            Debug.DrawLine(new Vector3(x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
            Vector3 loc;
            switch (ran)
            {
                case 1:
                    loc = new Vector3(Random.Range(-x, x) + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z);
                    break;
                case 2:
                    loc = new Vector3(Random.Range(-x, x) + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z);
                    break;
                case 3:
                    loc = new Vector3(-x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                    break;
                case 4:
                    loc = new Vector3(x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                    break;
                default:
                    loc = new Vector3(x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                    break;
            }
            Instantiate(enemy, loc, Quaternion.Euler(-90,0,0));
            yield return new WaitForSeconds(spawn_rate);
        }

    }

    public void spawner()
    {
        int ran = Random.Range(1, 5);
        float x = 52.4f, z = 33.6f;
        Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), Color.green, 40);
        Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(-x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
        Debug.DrawLine(new Vector3(-x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
        Debug.DrawLine(new Vector3(x + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z), new Vector3(x + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z), Color.green, 40);
        Vector3 loc;
        switch (ran)
        {
            case 1:
                loc = new Vector3(Random.Range(-x, x) + player.manager.gameObject.transform.position.x, 1, -z + player.manager.gameObject.transform.position.z);
                break;
            case 2:
                loc = new Vector3(Random.Range(-x, x) + player.manager.gameObject.transform.position.x, 1, z + player.manager.gameObject.transform.position.z);
                break;
            case 3:
                loc = new Vector3(-x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                break;
            case 4:
                loc = new Vector3(x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                break;
            default:
                loc = new Vector3(x + player.manager.gameObject.transform.position.x, 1, Random.Range(-z, z) + player.manager.gameObject.transform.position.z);
                break;
        }
        Instantiate(enemy, loc, Quaternion.Euler(-90, 0, 0));
    }

}
