using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float speed=20f;
    public float padding = 0.5f;
    public float projectileSpeed = 10f;
    public float projectileRate = 0.1f;
    public GameObject laser01prefab;
    [SerializeField] SpriteRenderer jetfly;

    bool outcontrol = false;

    #region restrict the player to the game place by parameters;
    /*
float xMax=6.11f;
float xMin=-6.11f;
float yMax = 3.88f;
float yMin = -3.88f;
    */
    #endregion

    float xMin;
    float xMax;
    float yMin;
    float yMax;

   

    private void Start()
    {
        float zdistance = transform.position.z- Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zdistance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zdistance));

        xMin = leftMost.x+padding;
        xMax = rightMost.x-padding;
        yMin = leftMost.y+padding;
        yMax = rightMost.y-padding;

        jetfly.enabled = false;
    }

    void Update () {
        if (outcontrol==false)
        {
            PlayerControl();
        }

        if (outcontrol==true)
        {
            jetfly.enabled = true;
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }

// restrict the player to the game place by parameters;
    float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
    float newY = Mathf.Clamp(transform.position.y,yMin, yMax);
    transform.position = new Vector3(newX, newY, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, projectileRate);  //注意，这里用 在多少秒后调用 的那个time时，即使要立刻调用，至少也要讲它设置为很小的数，这样就可以避免重叠的错误发生。
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        GameObject beam = Instantiate(laser01prefab, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void PlayerControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);  another way to implement player control; 

            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    public bool OutControl()
    {
        return outcontrol = true;
    }

    bool InControl()
    {
        return outcontrol = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinshedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinshedLoading;
    }

    void OnLevelFinshedLoading(Scene scene, LoadSceneMode mode)
    {
        InControl();
        transform.position = new Vector3(0, -3.88f, 0);
        jetfly.enabled = false;
    }
}
