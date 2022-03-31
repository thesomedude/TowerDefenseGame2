using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    private GameObject monster;

    private bool CanPlaceMonster()
    {
        return monster == null;
    }

    private bool CanUpgradeMonster()
    {
        if(monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterData.MonsterLevel nextLevel = monsterData.GetNextLevel();
            if(nextLevel != null)
            {
                return true;
            }
        }
        return false;
    }

    //Unity automatically calls OnMouseUp when a player taps a GameObject's physics collider
    private void OnMouseUp()
    {
        //When called, this method places a new monster if CanPlaceMonster() return true
        if (CanPlaceMonster())
        {
            //Creates monster with Instantiate.
            //Copy monsterPrefab, give it the current GameObject's position and no rotation, cast the result to a GameObject and store it in monster
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            //Call PlayOneShot to play the sound effect attached to the object's AudioSource component
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
        //TODO:  Deduct Gold
    }
}
