using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StackBrick : MonoBehaviour
{
    [SerializeField] private GameObject brickOnCharacter;
    [SerializeField] private Character character;
    [SerializeField] private ColorData cd;
    [SerializeField] private GameObject brickFall;
    
    private Stack<GameObject> stack = new Stack<GameObject>();
    private bool isBridgeCube = false;
    private int characterColor;
    private int countBrick = 0;
    private int brickOnBridgeColor = -1;
    private float spawnRadius = 5f;
    private void Start()
    {
        characterColor = character.CharacterColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Va chạm với gạch
        if (other.gameObject.CompareTag(Constant.TAG_BRICK))
        {
            BrickGround brick = Cache.GetBrickGround(other);

            if (brick != null)
            {
                if (characterColor == brick.BrickColor)
                {
                    AddBrick(other);
                    other.gameObject.SetActive(false);
                }
            }
        }

        if (other.gameObject.CompareTag(Constant.TAG_BRICKFALL))
        {
            BrickFall brickFall = other.GetComponent<BrickFall>();

            if (brickFall != null)
            {
                AddBrick(other);
                Destroy(other.gameObject);
            }
        }
        // Va chạm với BridgeCube
        if (other.gameObject.CompareTag(Constant.TAG_BRIDGECUBE))
        {
            BrickOnBridge brickOnBridge = Cache.GetBrickOnBridge(other);
            isBridgeCube = true;
            if (brickOnBridge != null)
            {
                brickOnBridgeColor = brickOnBridge.BrickColor;
            }
            if (brickOnBridge != null && brickOnBridge.BrickColor != characterColor && countBrick > 0)
            {
                brickOnBridge.SetBrickColor(characterColor);
                BoxCollider boxCollider = Cache.GetBoxCollider(other);
                boxCollider.isTrigger = true;
                Destroy(stack.Pop());
                countBrick--;
            }
        }
        if (other.gameObject.CompareTag(Constant.TAG_OPENDOOR))
        {
            Door door = Cache.GetDoor(other);
            door.OpenDoor();
        }
        if (other.gameObject.CompareTag(Constant.TAG_CLOSEDOOR))
        {
            Door door = Cache.GetDoor(other);
            door.CloseDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_BRIDGECUBE))
        {
            isBridgeCube = false;
        }
    }

    // Thêm gạch
    public void AddBrick(Collider other)
    {
        GameObject newBrick = Instantiate(brickOnCharacter);
        Transform trans = Cache.GetTransform(newBrick);
        trans.SetParent(transform.parent);
        trans.localPosition = new Vector3(0, 0.3f * (countBrick + 3), -0.7f);
        trans.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        newBrick.GetComponent<BrickOnCharacter>().SetBrickColor(characterColor);
        countBrick++;
        stack.Push(newBrick); // Thêm gạch vào stack
    }

    // Xóa hết gạch
    public void ClearBrick()
    {
        while (stack.Count != 0)
        {
            Destroy(stack.Pop());
            countBrick--;
        }
    }
    // Rơi hết gạch
    public void FallBrick()
    {
        while (stack.Count != 0)
        {
            Destroy(Stack.Pop());
            countBrick--;
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
            Vector3 brickPos = new Vector3(transform.position.x + randomPos.x, transform.position.y + 3, transform.position.z + randomPos.z);
            GameObject brick = Instantiate(brickFall, brickPos, Quaternion.identity);
        }
    }
    public int CountBrick { get { return countBrick; } set { countBrick = value; } }
    public bool IsBridgeCube { get { return isBridgeCube; } }
    public int CharacterColor { get { return CharacterColor; } }
    public int BrickOnBridgeColor { get { return brickOnBridgeColor; } set { brickOnBridgeColor = value; } }
    public Stack<GameObject> Stack { get { return stack; } }
}
