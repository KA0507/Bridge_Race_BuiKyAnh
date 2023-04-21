using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Character> dictCharacter = new Dictionary<Collider, Character>();
    public static Character GetCharacter(Collider collider)
    {
        if (!dictCharacter.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            dictCharacter.Add(collider, character);
        }
        return dictCharacter[collider];
    }

    private static Dictionary<Collider, Door> dictDoor = new Dictionary<Collider, Door>();
    public static Door GetDoor(Collider collider)
    {
        if (!dictDoor.ContainsKey(collider))
        {
            Door door = collider.GetComponent<Door>();
            dictDoor.Add(collider, door);
        }
        return dictDoor[collider];
    }

    private static Dictionary<Collider, Enemy> dictEnemy = new Dictionary<Collider, Enemy>();
    public static Enemy GetEnemy(Collider collider)
    {
        if (!dictEnemy.ContainsKey(collider))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            dictEnemy.Add(collider, enemy);
        }
        return dictEnemy[collider];
    }

    private static Dictionary<Collider, BrickGround> dictBrickGround = new Dictionary<Collider, BrickGround>();
    public static BrickGround GetBrickGround(Collider collider)
    {
        if (!dictBrickGround.ContainsKey(collider))
        {
            BrickGround brickGround = collider.GetComponent<BrickGround>();
            dictBrickGround.Add(collider, brickGround);
        }
        return dictBrickGround[collider];
    }
    private static Dictionary<Collider, BrickOnBridge> dictBrickOnBridge = new Dictionary<Collider, BrickOnBridge>();
    public static BrickOnBridge GetBrickOnBridge(Collider collider)
    {
        if (!dictBrickOnBridge.ContainsKey(collider))
        {
            BrickOnBridge brickOnBridge = collider.GetComponent<BrickOnBridge>();
            dictBrickOnBridge.Add(collider, brickOnBridge);
        }
        return dictBrickOnBridge[collider];
    }
    private static Dictionary<Collider, BoxCollider> dictBoxCollider = new Dictionary<Collider, BoxCollider>();
    public static BoxCollider GetBoxCollider(Collider collider)
    {
        if (!dictBoxCollider.ContainsKey(collider))
        {
            BoxCollider boxCollider = collider.GetComponent<BoxCollider>();
            dictBoxCollider.Add(collider, boxCollider);
        }
        return dictBoxCollider[collider];
    }
    private static Dictionary<GameObject, Transform> dictTransform = new Dictionary<GameObject, Transform>();
    public static Transform GetTransform(GameObject gameObject)
    {
        if (!dictTransform.ContainsKey(gameObject))
        {
            Transform transform = gameObject.GetComponent<Transform>();
            dictTransform.Add(gameObject, transform);
        }
        return dictTransform[gameObject];
    }
}
