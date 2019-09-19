﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Enemy base class, inherits from abstract avatar base and uses IStats interface to regulate its entity data
/// </summary>
public class Enemy : AbstractAvatarClass, IStats<EnemyData>, IPoolable
{
    /// <summary>
    /// Input of this enemy instance
    /// </summary>
    public EnemyData enemyInput;
    /// <summary>
    /// Maximum level the enemy and his childs should be able to reach
    /// </summary>
    public const int MAXLEVEL = 2;

    /// <summary>
    /// Enemy his to low spot to check if the enemy is to low
    /// </summary>
    public const float TOLOW = 1f;

    private bool checking = true;

    /// <summary>
    /// Overriden start method
    /// </summary>
    public override void Start()
    {
        ///First we call the base
        base.Start();
        try
        {
            enemyInput.Equals(enemyInput);
        }
        catch
        {
            enemyInput = new EnemyData();
        }
        GetComponent<Renderer>().material.color = Random.ColorHSV();

        /// We add all children to the splitPoints list stored in the enemyInput data
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            enemyInput.SplitPoints.Add(transform.GetChild(childIndex));
        }

        /// We give a random force so every enemy acts different
        RBody.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5) * 10f), ForceMode.Impulse);

        ///We always need points
        if (enemyInput.Points == 0)
            enemyInput.Points = 10;
    }

    private void FixedUpdate()
    {        
        if(transform.position.y < TOLOW && checking)
        {
            StartCoroutine(ToLowChecking(TOLOW));
        }
    }

    private IEnumerator ToLowChecking(float _low)
    {
        checking = false;
        float time = 0;
        while(transform.position.y < _low)
        {
            time++;
            yield return new WaitForEndOfFrame();
        }

        if (time > _low)
        {
            RBody.AddForce(new Vector3(Random.Range(-1,1), Random.Range(-1,2), Random.Range(-1,1)) * (Speed), ForceMode.Impulse);
            time = 0;
            yield return new WaitForSeconds(_low);
            checking = true;
        }
        else
        {
            yield return new WaitForSeconds(_low);
            checking = true;
        }
    }

    /// <summary>
    /// Handles the activations and regulation of the children 
    /// </summary>
    public void SplitEnemy()
    {
        if (enemyInput == null) return;
        if (enemyInput.Level < MAXLEVEL)
        {
            foreach (Transform point in enemyInput.SplitPoints)
            {
                GameObject child = ObjectPoolerLearning.Instance.SpawnFromPool<Enemy>(point.position, Quaternion.identity).gameObject;

                child.transform.localScale = new Vector3(child.transform.localScale.x / 2, child.transform.localScale.y / 2, child.transform.localScale.z / 2);
                enemyInput.Level++;
                child.GetComponent<Enemy>().enemyInput.Level = enemyInput.Level;            
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Implemented SetStats from the interface, here we set the enemy input
    /// </summary>
    /// <param name="data"></param>
    public void SetStats(EnemyData data)
    {
        enemyInput = data;
    }

}