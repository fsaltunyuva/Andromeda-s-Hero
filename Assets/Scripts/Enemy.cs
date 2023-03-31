using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [FormerlySerializedAs("deathVFX")] [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private int score = 15;
    [SerializeField] private int hitPoints = 2;

    private ScoreBoard scoreBoard;
    private GameObject parentGameObject;

    private void Start()
    {
        AddRigidbody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntimeB");
        scoreBoard = FindObjectOfType<ScoreBoard>();
        // scoreBoard = GameObject.Find("Scoreboard").GetComponent<ScoreBoard>();
        // GameObject.Find("name"); // returns a single object, or null
        // GameObject.FindGameObjectWithTag("tagName");//returns (randomly, i think) one of the objects that have the required tag
        // GameObject.FindGameObjectsWithTag("tagName");//returns a list of gameobjects that have the required tag
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }

    }

    private void KillEnemy()
    {
        GameObject
            fx = Instantiate(deathFX, transform.position,
                Quaternion.identity); //Quaternion.identity indicates that there is no need to change rotation
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
        scoreBoard.increaseScore(score);
        
    }

    private void ProcessHit()
    {
        GameObject
            vfx = Instantiate(hitVFX, transform.position,
                Quaternion.identity); //Quaternion.identity indicates that there is no need to change rotation
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }
}
