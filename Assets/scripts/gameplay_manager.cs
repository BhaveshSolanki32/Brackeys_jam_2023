using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameplay_manager : MonoBehaviour
{
    [Range(0, 100)] float karma=0;
    public GameObject over;
    public static gameplay_manager manager;
    public Slider karma_slid;
    public bool GameOver = false;
    public TextMeshProUGUI kill_sec_txt;
    int total_kill=0;
    public int gen = 0;
    public int current_gen = 0;
    public TextMeshProUGUI over_t_k;
    public GameObject[] creatures;
    public GameObject reinc_screen;
    public Image char_img;
    public TextMeshProUGUI char_text;
    public Sprite[] char_images;

    public TextMeshProUGUI time_left;
    private void Start()
    {
        StartCoroutine(wave_system());
        manager = this;
        time_left.text = "120";
        kill_sec_txt.text = 0.ToString();
    }


    IEnumerator wave_system()
    {
        int turns = 0;
        yield return new WaitForSeconds(0.5f);
        
        while (turns < 4)
        {
            enemy_manager.manager.spawn_rate -= 0.5f;
            npc_manager.manager.spawn_rate += 7*2;
            npc_manager.manager.spawner();
            time_left.text = ((5-turns)*30).ToString();
           
            for(int i =0; i<=30; i++)
            {
                time_left.text = (120-(turns * 30 + i)).ToString();
                yield return new WaitForSeconds(1);
            }
            Debug.Log("wave+");
            turns++;
        }
        game_over();
        yield return null;
    }

    void game_over()
    {
        GameOver = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        over.SetActive(true);
        over_t_k.text = "score: " + total_kill.ToString()+ " kill/sec";
    }

    public void restart_game()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void karma_handler(float a)
    {
        if (a == 1)
            total_kill++;
        kill_sec_txt.text = total_kill.ToString();
        karma = karma+(3 * a);
        karma = Mathf.Clamp(karma, 0, 100);
        karma_slid.value = karma;
        Debug.Log(karma);
        if (karma >= 66.6)
            gen = 2;
        else if (karma >= 33.3)
            gen = 1;
        else
            gen = 0;
    }

    public void reincarnate()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        Vector3 pos = player.manager.transform.position;
        player.manager.gameObject.SetActive(false);
        player.manager.health = 100;
        player.manager.health_slider.value = 100;
        Quaternion rot = player.manager.gameObject.transform.rotation;
        creatures[gen].SetActive(true);
        creatures[gen].transform.position = new Vector3(pos.x, creatures[gen].transform.position.y, pos.z);
        creatures[gen].transform.rotation = rot;
        current_gen = gen;
    }


    public void dead()
    {
        Debug.Log("dead");
        reinc_screen.SetActive(true);
        string next_name = creatures[gen].name.ToUpperInvariant();
        char_text.text = "You are reincarnated as a " + next_name + " !!";
        char_img.sprite = char_images[gen];
        foreach (enemy x in FindObjectsOfType<enemy>())
        {
            Destroy(x.gameObject);
            
        }
        enemy_manager.manager.spawner();

        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
