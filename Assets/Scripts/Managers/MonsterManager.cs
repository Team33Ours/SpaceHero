using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터의 생성, 사망처리
/// 2025.02.24.ImSeonggyun
/// 
/// 몬스터의 경우 오브젝트 풀링을 적용
/// 2025.02.26.한만진
/// </summary>
public class MonsterManager : MonoBehaviour
{
    public int poolSize;

    GameManager gameManager;

    public GameObject flyingMonster;
    public GameObject greenMonster;
    public GameObject bossMonster;

    public List<GameObject> flyingMonsterPool;
    private List<GameObject> greenMonsterPool;
    private List<GameObject> bossMonsterPool;

    [SerializeField]
    public Transform flyingMonsterParent;
    [SerializeField]
    public Transform greenMonsterParent;
    [SerializeField]
    public Transform bossMonsterParent;

    public NormalMonsterController normalMonsterController;

    private void Awake()
    {
        flyingMonsterPool = new List<GameObject>();
        greenMonsterPool = new List<GameObject>();
        bossMonsterPool = new List<GameObject>();


        //for(int i = 0; i < poolSize; i++)
        //{
        //    GameObject flying = Instantiate(flyingMonster, flyingMonsterParent);
        //    flying.SetActive(false);


        //    flyingMonsterPool.Add(flying);
        //    GameObject green = Instantiate(greenMonster, greenMonsterParent);
        //    green.SetActive(false);


        //    greenMonsterPool.Add(green);
        //    GameObject boss = Instantiate(bossMonster, bossMonsterParent);
        //    boss.SetActive(false);


        //    bossMonsterPool.Add(boss);
        //}
    }

    private void Start()
    {
        gameManager = GameManager.Instance;


        for (int i = 0; i < poolSize; i++)
        {
            GameObject flying = Instantiate(flyingMonster, flyingMonsterParent);
            //normalMonsterController = flying.GetComponent<NormalMonsterController>();

            flyingMonsterPool.Add(flying);
            //if (normalMonsterController != null)
            //{
            //    Debug.Log("1");
            //    //normalController1.Initializea(this, gameManager.playerController.transform, 100f);
            //    flyingMonsterPool[i].GetComponent<NormalMonsterController>().Initializea(this, gameManager.playerController.transform, 100f);
            //}
            flying.SetActive(false);



            GameObject green = Instantiate(greenMonster, greenMonsterParent);
            //NormalMonsterController normalController2 = green.GetComponent<NormalMonsterController>();
            //if (normalController2 != null)
            //    normalController2.Initializea(this, gameManager.playerController.transform, 100f);
            green.SetActive(false);
            greenMonsterPool.Add(green);

            GameObject boss = Instantiate(bossMonster, bossMonsterParent);
            //BossMonsterController bossController = boss.GetComponent<BossMonsterController>();
            //if (bossController != null)
            //    bossController.Initialize(this, gameManager.playerController.transform, 200f);
            boss.SetActive(false);
            bossMonsterPool.Add(boss);
        }

    }

    public GameObject FlyMonsterFromPool()
    {
        for (int i = 0; i < flyingMonsterPool.Count; i++)
        {
            if (!flyingMonsterPool[i].activeInHierarchy)
            {
                flyingMonsterPool[i].SetActive(true);
                return flyingMonsterPool[i];
            }
        }

        GameObject flying = Instantiate(flyingMonster, flyingMonsterParent);
        //NormalMonsterController normalController2 = flying.GetComponent<NormalMonsterController>();
        //if (normalController2 != null)
        //    normalController2.Initializea(this, gameManager.playerController.transform, 100f);
        flying.SetActive(true);
        flyingMonsterPool.Add(flying);
        return flying;
    }

    public GameObject GreenMonsterFromPool()
    {
        for (int i = 0; i < greenMonsterPool.Count; i++)
        {
            if (!greenMonsterPool[i].activeInHierarchy)
            {
                greenMonsterPool[i].SetActive(true);
                return greenMonsterPool[i];
            }
        }
        GameObject green = Instantiate(greenMonster, greenMonsterParent);
        green.SetActive(true);
        greenMonsterPool.Add(green);
        return green;
    }

    public GameObject BossMonsterFromPool()
    {
        for (int i = 0; i < bossMonsterPool.Count; i++)
        {
            if (!bossMonsterPool[i].activeInHierarchy)
            {
                bossMonsterPool[i].SetActive(true);
                return bossMonsterPool[i];
            }
        }
        GameObject boss = Instantiate(bossMonster, bossMonsterParent);
        boss.SetActive(true);
        bossMonsterPool.Add(boss);
        return boss;
    }
    public void RemoveMonsterOnDeath(GameObject monster)
    {
        monster.SetActive(false);
    }
}
