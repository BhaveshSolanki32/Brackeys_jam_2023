using UnityEngine;

public class npc_manager : MonoBehaviour
{
    public static npc_manager manager;
    public int spawn_rate = 1;
    [SerializeField] GameObject npc;
    void Start()
    {
        manager = this;
        Invoke("vect_rand", 0.1f);
    }
    public void spawner()
    {
        for(int i= 0;i<=spawn_rate;i++)
            Instantiate(npc, vect_rand(), Quaternion.identity);
    }

    private Vector3 vect_rand()
    {
        int ran = Random.Range(1, 5);
        float x = 52.4f, z = 33.6f;
        Vector3 loc;
        switch (ran)
        {
            case 1:
                loc = new Vector3(Random.Range(-x, x) + npc.transform.position.x, 1, -z + npc.transform.position.z);
                break;
            case 2:
                loc = new Vector3(Random.Range(-x, x) + npc.transform.position.x, 1, z + npc.transform.position.z);
                break;
            case 3:
                loc = new Vector3(-x + npc.transform.position.x, 1, Random.Range(-z, z) + npc.transform.position.z);
                break;
            case 4:
                loc = new Vector3(x + npc.transform.position.x, 1, Random.Range(-z, z) + npc.transform.position.z);
                break;
            default:
                loc = new Vector3(x + npc.transform.position.x, 1, Random.Range(-z, z) + npc.transform.position.z);
                break;
        }
        return loc;
    }
}
