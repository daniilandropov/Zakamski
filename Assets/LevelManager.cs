using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GUI ThisGUI;
    public Floor ThisFloorDerevnya;
    public Floor ThisFloorLes;
    public Hero ThisHero;
    public List<Home> Homes;
    public List<Tree> Trees;
    public GameObject StopWall;

    private GUI _instGUI;

    public int KvasCount = 0;

    public int FloorCountDerevnya = 51;
    public int FloorCountLes = 51;

    private int _homeCount = 0;
    private int _treeCount = 0;


    void Start()
    {
        _instGUI = Instantiate(ThisGUI);

        #region Земля деревня
        var floorDerevnyaWidth = ThisFloorDerevnya.GetWitdth();

        var currentFloorDerevnyaPosition = new Vector3(0, (-ThisFloorDerevnya.GetHeight() * 0.5f) + 0.5f, 0);

        for (var i = 0; i < FloorCountDerevnya; i++)
        {
            Instantiate(ThisFloorDerevnya, currentFloorDerevnyaPosition, Quaternion.identity);
            currentFloorDerevnyaPosition = new Vector3(currentFloorDerevnyaPosition.x + floorDerevnyaWidth, currentFloorDerevnyaPosition.y, currentFloorDerevnyaPosition.z);
        }
        #endregion

        #region Дома

        _homeCount = FloorCountDerevnya / 2;

        var currentHomePosition = new Vector3(floorDerevnyaWidth, 4.5f, 0);

        var lastIndex = 0;

        float lastHomeWidth = 1f;

        var startHeroPos = new Vector3();

        for (var i = 0; i < _homeCount; i++)
        {
            int index = 0;
            while (lastIndex == index)
                index = new System.Random(DateTime.Now.ToString().GetHashCode()).Next(0, Homes.Count);
                

            lastIndex = index;

            currentHomePosition = new Vector3(currentHomePosition.x + lastHomeWidth / 2, currentHomePosition.y, currentHomePosition.z);

            if (i == 1)
                Instantiate(StopWall, currentHomePosition, Quaternion.identity);

            if (i == 2)
                startHeroPos = new Vector3(currentHomePosition.x, currentHomePosition.y + 15f, currentHomePosition.z);


            var home = Instantiate(Homes[index], currentHomePosition, Quaternion.identity);

            lastHomeWidth = home.GetWitdth();

            currentHomePosition = new Vector3(currentHomePosition.x + lastHomeWidth / 2, currentHomePosition.y, currentHomePosition.z);
        }

        #endregion


        #region Лес земля
        var floorLesWidth = ThisFloorLes.GetWitdth();

        var currentFloorLesPosition = currentFloorDerevnyaPosition;

        for (var i = 0; i < FloorCountLes; i++)
        {
            Instantiate(ThisFloorLes, currentFloorLesPosition, Quaternion.identity);
            currentFloorLesPosition = new Vector3(currentFloorLesPosition.x + floorLesWidth, currentFloorLesPosition.y, currentFloorLesPosition.z);
        }
        #endregion


        #region Деревья

        _treeCount = FloorCountLes * 2;

        var currentTreePosition = new Vector3(currentHomePosition.x, 6f, 0); // Деревья спаунятся на месте последнего дома

        lastIndex = 0;

        for (var i = 0; i < _treeCount; i++)
        {
            int index = 0;
            while (lastIndex == index)
                index = UnityEngine.Random.Range(0, Trees.Count*1000);

            lastIndex = index;

            var tree = Instantiate(Trees[index/1000], currentTreePosition, Quaternion.identity);

            currentTreePosition = new Vector3(currentTreePosition.x + 5f, currentTreePosition.y, currentTreePosition.z);
        }

        #endregion




        var hero = Instantiate(ThisHero, startHeroPos, Quaternion.identity); ;
        hero.SetLM(this);
    }

    public void AddKvas()
    {
        KvasCount++;
        _instGUI.SetKvas(KvasCount);
    }

    public bool MinusKvas()
    {
        if(KvasCount > 0)
        {
            KvasCount--;
            _instGUI.SetKvas(KvasCount);
            return true;
        }

        return false;
       
    }

    public int Damage()
    {
        var ret = KvasCount;
        KvasCount = 0;
        _instGUI.SetKvas(KvasCount);
        return ret;
    }
}
