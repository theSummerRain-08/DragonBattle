using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [Header("Transform Character Unlock")]
    [SerializeField] private float gokuTransformLevel = 0;
    [SerializeField] private float vegetaTransformLevel = 0;
    [SerializeField] private float trunkTransformLevel = 0;
    [SerializeField] private float gohanTransformLevel = 0;

    public float GokuTransformLevel => gokuTransformLevel;
    public float VegetaTransformLevel => vegetaTransformLevel;
    public float TrunkTransformLevel => trunkTransformLevel;
    public float GohanTransformLevel => gohanTransformLevel; 
    private void OnEnable() {
        gokuTransformLevel = PlayerPrefs.GetFloat("GokuLevel", 0);
        vegetaTransformLevel = PlayerPrefs.GetFloat("VegetaLevel", 0);
        trunkTransformLevel = PlayerPrefs.GetFloat("TrunkLevel", 0);
        gohanTransformLevel = PlayerPrefs.GetFloat("GohanLevel", 0);
    }
    public void UnlockNextTransformLevel(CharacterToSelect character) {
        switch (character) {
            case CharacterToSelect.Goku: 
                gokuTransformLevel += 1;
                PlayerPrefs.SetFloat("GokuLevel", gokuTransformLevel);
                PlayerPrefs.Save();
                break;
            case CharacterToSelect.Vegeta:
                vegetaTransformLevel += 1;
                PlayerPrefs.SetFloat("VegetaLevel", vegetaTransformLevel);
                PlayerPrefs.Save();
                break;
            case CharacterToSelect.Trunk:
                trunkTransformLevel += 1;
                PlayerPrefs.SetFloat("TrunkLevel", trunkTransformLevel);
                PlayerPrefs.Save();
                break;
            case CharacterToSelect.Gohan: 
                gohanTransformLevel += 1;
                PlayerPrefs.SetFloat("GohanLevel", gohanTransformLevel);
                PlayerPrefs.Save();
                break;
        }
    }
}
