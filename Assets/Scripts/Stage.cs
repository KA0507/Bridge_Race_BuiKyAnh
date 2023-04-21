using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private BrickGround[] brickGrounds;
    [SerializeField] private Transform[] targets;
    [SerializeField] private float inactiveTime = 30f;
    [SerializeField] private int numberColor;

    private float timeSinceInactive = 0f;
    private bool[] colorOnStage = new bool[6];

    private void Awake()
    {
        colorOnStage[0] = false;
        colorOnStage[1] = false;
        colorOnStage[2] = false;
        colorOnStage[3] = false;
        colorOnStage[4] = false;
        colorOnStage[5] = false;
        foreach (BrickGround brick in brickGrounds)
        {
            brick.BrickColor = Random.Range(0, numberColor);
            brick.SetBrickColor(brick.BrickColor);
        }
    }
    private void Update()
    {
        timeSinceInactive += Time.deltaTime;
        if (timeSinceInactive > inactiveTime)
        {
            CreateBrick();
            timeSinceInactive = 0;
        }
    }

    // Tạo gạch
    public void CreateBrick()
    {
        foreach (BrickGround brick in brickGrounds)
        {
            if (brick.gameObject.activeSelf == false)
            {
                if (timeSinceInactive >= inactiveTime)
                {
                    brick.BrickColor = Random.Range(0, numberColor);
                    brick.SetBrickColor(brick.BrickColor);
                    if (colorOnStage[brick.BrickColor])
                    {
                        brick.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    // Tạo gạch theo màu
    public void CreateBrick(int color)
    {
        foreach (BrickGround brick in brickGrounds)
        {
            if (brick.gameObject.activeSelf == false)
            {
                if (brick.BrickColor == color && colorOnStage[color])
                {
                    brick.gameObject.SetActive(true);
                }
            }
        }
    }

    // Tìm viên gạch gần nhất theo màu
    public Vector3 FindClosestBrick(int color, Vector3 pos)
    {
        Vector3 closestBrick = Vector3.zero;
        float closestDistance = Mathf.Infinity;
        foreach (BrickGround brick in brickGrounds)
        {
            if (brick.BrickColor == color && brick.gameObject.activeSelf)
            {
                Transform trans = Cache.GetTransform(brick.gameObject);
                float distance = Vector3.Distance(pos, trans.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBrick = trans.position;
                }
            }
        }
        return closestBrick;
    }
    public BrickGround[] GetBricks()
    {
        return brickGrounds;
    }
    public Transform[] Targets { get { return targets; } set { targets = value; } }
    public bool[] ColorOnStage { get { return colorOnStage; } set { colorOnStage = value; } }
    public float TimeSinceInactive { get { return timeSinceInactive; } set { timeSinceInactive = value; } }
    
}
