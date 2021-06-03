using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Floor ThisFloor;
    public Hero ThisHero;
    public List<Home> Homes;

    public int FloorCount = 51;

    private int _homeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        var floorWidth = ThisFloor.GetWitdth();

        var currentFloorPosition = new Vector3(0, 0, 0);

        for(var i = 0; i < FloorCount; i++)
        {
            Instantiate(ThisFloor, currentFloorPosition, Quaternion.identity);
            currentFloorPosition = new Vector3(currentFloorPosition.x + floorWidth, currentFloorPosition.y, currentFloorPosition.z);
        }

        Instantiate(ThisHero, new Vector3(0, 0, 0), Quaternion.identity);

        _homeCount = FloorCount / 2;

        var currentHomePosition = new Vector3(floorWidth, 4.5f, 0);

        var random = new System.Random(DateTime.Now.ToString().GetHashCode());

        var lastIndex = 0;

        float lastHomeWidth = 1f;

        for (var i = 0; i < _homeCount; i++)
        {
            int index = 0;
            while(index == lastIndex)
                index = random.Next(0, Homes.Count);

            lastIndex = index;

            currentHomePosition = new Vector3(currentHomePosition.x + lastHomeWidth/2, currentHomePosition.y, currentHomePosition.z);

            var home = Instantiate(Homes[index], currentHomePosition, Quaternion.identity);

            lastHomeWidth = home.GetWitdth();

            currentHomePosition = new Vector3(currentHomePosition.x + lastHomeWidth/ 2, currentHomePosition.y, currentHomePosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
