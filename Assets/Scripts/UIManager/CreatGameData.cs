using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatGameData : MonoBehaviour
{
    private void Awake() {

        //_________________Game State__________________________
        //PlayerPrefs.DeleteKey("EnemyLevel");
        //PlayerPrefs.DeleteKey("PreviousEnemyLevel");
        if (!PlayerPrefs.HasKey("EnemyLevel")) {
            PlayerPrefs.SetInt("EnemyLevel", 1);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("PreviousEnemyLevel")) {
            PlayerPrefs.SetInt("PreviousEnemyLevel", 1);
            PlayerPrefs.Save();
        }

        //_____________________________________________________


        //_________________Currency____________________________
        //if (!PlayerPrefs.HasKey("CurrentGold")) {
        //    PlayerPrefs.SetFloat("CurrentGold", 9000000);
        //    PlayerPrefs.Save();
        //}

        //if (!PlayerPrefs.HasKey("CurrentBean")) {
        //    PlayerPrefs.SetFloat("CurrentBean", 20);
        //    PlayerPrefs.Save();
        //}
        //___________________________________________________


        //_______________Unlock Player Level_________________
        if (!PlayerPrefs.HasKey("GokuLevel")) {
            PlayerPrefs.SetFloat("GokuLevel", 0);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("VegetaLevel")) {
            PlayerPrefs.SetFloat("VegetaLevel", 0);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("TrunkLevel")) {
            PlayerPrefs.SetFloat("TrunkLevel", 0);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("GohanLevel")) {
            PlayerPrefs.SetFloat("GohanLevel", 0);
            PlayerPrefs.Save();
        }
        //__________________________________________________
    }
}
