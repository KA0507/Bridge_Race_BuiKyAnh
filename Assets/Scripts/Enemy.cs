using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private Rigidbody rb;

    public NavMeshAgent navMeshAgent;
    bool isDead = false;
    float maxrx;
    float timer = 2;
    int intTarget;
    private Vector3 targetBridge;
    private bool isPause = false;
    private int randomBridge = -1;
    private IState currentState;
    

    // Start is called before the first frame update
    void Start()
    {
        ChangeColor(CharacterColor);
        ChangeStage();
        ChangeState(new MoveBrickState());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
        {
            navMeshAgent.velocity = Vector3.zero;
            return;
        }
        if (timer < 1f)
        {
            timer += Time.fixedDeltaTime;
            navMeshAgent.velocity = Vector3.zero;
            isDead = true;
            return;
        } else
        {
            StackBrick.gameObject.SetActive(true);
            isDead = false;
        }
        // Nếu currentState khác null và không chết thực hiện state
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        } 
    }
    // Thay đổi state của Enemy
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    // Thay đổi Stage
    public override void ChangeStage()
    {
        base.ChangeStage();
        randomBridge = Random.Range(0, CurrentStage.Targets.Length);
        targetBridge = CurrentStage.Targets[randomBridge].position - Vector3.forward;
    }
    
    // Ngã
    public void Fall()
    {
        timer = 0;
        ChangeAnim(Constant.ANIM_FALL);
    }

    // Enemy đến đích
    public void Victory()
    {
        ChangeAnim(Constant.ANIM_VICTORY);
        UIManager.Ins.CloseUI<GamePlay>();
        UIManager.Ins.OpenUI<Defeat>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_BRIDGE))
        {
            maxrx = collision.transform.rotation.eulerAngles.x - 360;
        }
        if (collision.gameObject.CompareTag(Constant.TAG_GROUND))
        {
            maxrx = 0;
        }
        // Va chạm với character có nhiều gạch hơn
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER) && !StackBrick.IsBridgeCube)
        {
            Character character = Cache.GetCharacter(collision.collider);
            if (character != null)
            {
                if (character.StackBrick.CountBrick > StackBrick.CountBrick)
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
        
    }
    public Rigidbody RB { get { return rb; } set { rb = value; } }
    public Vector3 TargetBridge { get { return targetBridge; } set { targetBridge = value; } }
    public int IntTarget { get { return intTarget; } set { intTarget = value; } }
    public int RandomBridge { get { return randomBridge; } set { randomBridge = value; } }
    public bool IsPause { get { return isPause; } set { isPause = value; } }

}