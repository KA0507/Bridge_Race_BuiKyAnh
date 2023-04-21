using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Player : Character
{
    [SerializeField] private Transform people;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Joystick joystick;

    float maxrx;
    float timer = 2;
    bool isFall = false;
    bool isPause = false;
    Vector3 transformJoystick;

    void Start()
    {
        ChangeColor(CharacterColor);
        ChangeStage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPause)
        {
            return;
        }
        if (timer < 2f)
        {
            timer += Time.fixedDeltaTime;
            return;
        } else
        {
            StackBrick.gameObject.SetActive(true);
        }
        if (joystick != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 viewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 joystickPosition = new Vector3(viewportPosition.x * Screen.width, viewportPosition.y * Screen.height, joystick.transform.position.z);
                if (joystickPosition.x > 300 || joystickPosition.y < 1300)
                {
                    joystick.transform.position = joystickPosition;
                }
            }
            Move();
            if (Input.GetMouseButtonUp(0))
            {
                joystick.transform.position = transformJoystick;
            }
        }
    }
    // Di chuyển
    public void Move()
    {
        float horizontalMove = joystick.Horizontal;
        float verticalMove = joystick.Vertical;
        if (new Vector2(horizontalMove, verticalMove).magnitude > 0.1f)
        {
            ChangeAnim(Constant.ANIM_RUN);
        }else
        {
            ChangeAnim(Constant.ANIM_IDLE);
        }

        if (verticalMove > 0 && StackBrick.IsBridgeCube && CharacterColor != StackBrick.BrickOnBridgeColor && StackBrick.CountBrick == 0)
        {
            rb.velocity = new Vector3(horizontalMove, 0, 0);
            return;
        }

        // Di chuyển đối tượng theo giá trị của joystick
        if (verticalMove != 0)
        {
            float angle = Mathf.Atan2(horizontalMove, verticalMove) * Mathf.Rad2Deg;
            float rx = maxrx * Mathf.Cos(angle * Mathf.Deg2Rad);
            rb.velocity = new Vector3(horizontalMove, verticalMove * Mathf.Sin(Mathf.Abs(rx) * Mathf.Deg2Rad), verticalMove * Mathf.Cos(rx * Mathf.Deg2Rad)) * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
        else
        {
            rb.velocity = new Vector3(horizontalMove, 0, 0) * speed * Time.fixedDeltaTime;
        }
    }
    // Player đến đích
    public void Victory()
    {
        ChangeAnim(Constant.ANIM_VICTORY);
        isPause = true;
        rb.velocity = Vector3.zero;
        LevelManager.Ins.PauseGame();
        UIManager.Ins.CloseUI<GamePlay>();
        UIManager.Ins.OpenUI<Victory>();
    }
    // Ngã
    public void Fall()
    {
        timer = 0;
        ChangeAnim(Constant.ANIM_FALL);
    }

    // Thay đổi Stage
    public override void ChangeStage()
    {
        base.ChangeStage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_BRIDGE))
        {
            // góc nghiêng của bridge
            maxrx = collision.transform.rotation.eulerAngles.x - 360;
        }
        if (collision.gameObject.CompareTag(Constant.TAG_GROUND))
        {
            maxrx = 0;
        }
        // Va chạm với Enemy có nhiều gạch hơn
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER) && !StackBrick.IsBridgeCube)
        {
            Enemy enemy = Cache.GetEnemy(collision.collider);
            if (enemy != null)
            {
                if (enemy.StackBrick.CountBrick > StackBrick.CountBrick)
                {
                    Fall();
                    StackBrick.FallBrick();
                    StackBrick.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_BRIDGE))
        {
            maxrx = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_STAGEPOINT))
        {
            if (CurrentStage.Targets.Length != 1)
            {
                ChangeStage();
                Destroy(other.gameObject);
            }
            else
            {
                Victory();
                StackBrick.ClearBrick();
            }           
        }
    }
    public bool IsPause { get { return isPause; } set { isPause = value; } }
    public Joystick Joystick { get { return joystick; } set { joystick = value; } }
    public Vector3 TransformJoystick { get { return transformJoystick; } set { transformJoystick = value; } }
}
