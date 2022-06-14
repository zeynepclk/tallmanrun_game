using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour
{
    //Animations
    private Animator animator;

    //Move
    private Rigidbody rb;
    private Vector3 direction;
    private float x;
    private float y;
    public float forwardSpeed = 12f;

    //Scale
    Transform trans;
    private Vector3 scaleChange;

    //point
    public float point=0f;

    //Jump
    public float gravity ;
    public float jumpSpeed ;

    //Controls
    bool ifRun = false;
    bool ifJump = false;

    bool ifgrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        animator.SetBool("jump", false);
        animator.SetBool("run", false);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        TouchMove();
       // ChangeAnimations();
    }
   
    void TouchMove()
    {
        if (Input.GetMouseButton(0)&& !ifJump)
        {
            animator.SetBool("run", true);
            direction = transform.right * x + transform.forward * Time.deltaTime * forwardSpeed;
            transform.position= transform.position + direction * forwardSpeed * Time.deltaTime;
            ifRun = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ifRun = false;
            animator.SetBool("run", false);
        }
        if (Input.GetButtonDown("Jump")  && ifJump)
        {
            animator.SetBool("run", false);
            animator.SetBool("jump", true);
            ifgrounded = false;
        }

    }
    private void OnTriggerExit(Collider trig)
    {
        if (trig.CompareTag("block"))
        {
            scaleChange = new Vector3(0f, 0.25f, 0f);
            trans.transform.localScale -= scaleChange;
            Destroy(trig.gameObject);
            point -= 15;
        }
    }
 
    private void OnTriggerEnter(Collider trig)
    {
        if (trig.CompareTag("x"))
        {
            scaleChange=new Vector3(0.75f, 0f, 0f);
            trans.transform.localScale += scaleChange;
            Destroy(trig.gameObject);
            point += 25;
        }
        if (trig.CompareTag("xdown"))
        {
            scaleChange = new Vector3(0.75f, 0f, 0f);
            trans.transform.localScale -= scaleChange;
            Destroy(trig.gameObject);
            point -= 25;
        }

        if (trig.CompareTag("y"))
        {
            scaleChange = new Vector3(0f, 0.75f, 0f);
            trans.transform.localScale += scaleChange;
            Destroy(trig.gameObject);
            point += 25;
        }
        if (trig.CompareTag("ydown"))
        {
            scaleChange = new Vector3(0f, 0.75f, 0f);
            trans.transform.localScale -= scaleChange;
            Destroy(trig.gameObject);
            point -= 25;
        }
        if (trig.CompareTag("diamond"))
        {
            point += 10;
            Destroy(trig.gameObject);
        }
        if (trig.CompareTag("jump"))
        {
            ifgrounded = false;
            ifJump = true;
        }
        if (trig.CompareTag("finish"))
        {
            animator.SetTrigger("finish");
            ifJump = true;
        }


    }
    public void animationEnd(string message)
    {
        if (message.Equals("animationEnded"))
        {        
            animator.SetBool("jump", false);
            animator.SetTrigger("idle");
            ifgrounded = true;
            ifJump = false;
            point += 10;
        }
    }
    public void animationjump(string message)
    {
        if (message.Equals("jump"))
        {
            direction = new Vector3(0, Mathf.Sqrt(jumpSpeed * -10 * gravity), Mathf.Sqrt(jumpSpeed * -500 * gravity));
            transform.position = transform.position + direction * Time.deltaTime;
        }
    }
    public void showresult(string message)
    {
        if (message.Equals("result"))
        {
            PlayerPrefs.SetString("score", point.ToString());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        }
    }
