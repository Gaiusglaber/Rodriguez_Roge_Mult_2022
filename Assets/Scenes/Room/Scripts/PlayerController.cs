using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool turn = false;
    private bool lerping = false;
    private bool ataccking = false;
    private Vector3 destPos = Vector3.zero;
    private Vector3 initialPos = Vector3.zero;
    private Animator animator = null;
    private Photon.Pun.PhotonView photonView = null;
    public Action<PlayerController, PlayerController> OnWin = null;
    void Start()
    {
        photonView = GetComponent<Photon.Pun.PhotonView>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (!lerping)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lerping = true;
                    ataccking = true;
                    StartCoroutine(StartAttack());
                }
                InputMovement();
            }
        }
    }
    private void InputMovement()
    {
        initialPos = transform.position;

        Vector3 rightPosUp = new Vector2(transform.position.x + 0.425f, transform.position.y + 0.75f);
        Vector3 leftPosUp = new Vector2(transform.position.x - 0.425f, transform.position.y + 0.75f);

        Vector3 rightPosDown = new Vector2(transform.position.x + 0.425f, transform.position.y - 0.75f);
        Vector3 leftPosDown = new Vector2(transform.position.x - 0.425f, transform.position.y - 0.75f);
        if (Input.GetKeyDown(KeyCode.W))
        {
            turn = !turn;
            destPos = turn ? rightPosUp : leftPosUp;
            lerping = true;
            StartCoroutine(StartMovement());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            destPos = turn ? rightPosDown : leftPosDown;
            lerping = true;
            turn = !turn;
            StartCoroutine(StartMovement());
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            destPos = new Vector2(transform.position.x + 0.85f, transform.position.y);
            turn = false;
            lerping = true;
            StartCoroutine(StartMovement());
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            turn = true;
            destPos = new Vector2(transform.position.x - 0.85f, transform.position.y);
            lerping = true;
            StartCoroutine(StartMovement());
        }
        transform.eulerAngles = turn ? Vector3.zero : new Vector3(0, 180, 0);
        if (destPos.x < -0.42f || destPos.x > 14.4f || destPos.y > 5.7f || destPos.y < 0)
        {
            destPos = initialPos;
        }
    }
    private IEnumerator StartMovement()
    {
        animator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.5f);
        while (transform.position != destPos)
        {
            transform.position = Vector3.Lerp(transform.position, destPos, 0.2f);
            yield return new WaitForEndOfFrame();
        }
        lerping = !lerping;
        animator.SetBool("Walk", false);
    }
    private IEnumerator StartAttack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);
        Vector3 initialPos = transform.position;
        destPos = turn ? new Vector2(transform.position.x - 0.85f, transform.position.y): new Vector2(transform.position.x + 0.85f, transform.position.y);
        while (transform.position != destPos)
        {
            transform.position = Vector3.Lerp(transform.position, destPos, 0.4f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.2f);
        while (transform.position != initialPos)
        {
            transform.position = Vector3.Lerp(transform.position, initialPos, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        ataccking = false;
        lerping = !lerping;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (ataccking)
            {
                OnWin?.Invoke(this, collision.GetComponent<PlayerController>());
                collision.gameObject.SetActive(false);
            }
        }
    }
}
