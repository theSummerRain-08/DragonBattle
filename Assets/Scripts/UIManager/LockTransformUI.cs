using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransformUI : MonoBehaviour {
    public float thisLevel;
    public CharacterToSelect thisCharacterToSelect;
    private void OnEnable() {
        switch (thisCharacterToSelect) {
            case CharacterToSelect.Goku:
                this.gameObject.SetActive(thisLevel > GameManager.Instance.GokuTransformLevel);
                break;
            case CharacterToSelect.Vegeta:
                this.gameObject.SetActive(thisLevel > GameManager.Instance.VegetaTransformLevel);
                break;
            case CharacterToSelect.Gohan:
                this.gameObject.SetActive(thisLevel > GameManager.Instance.GohanTransformLevel);
                break;
            case CharacterToSelect.Trunk:
                this.gameObject.SetActive(thisLevel > GameManager.Instance.TrunkTransformLevel);
                break;

        }

    }
    private void Update() {
        switch (thisCharacterToSelect) {
            case CharacterToSelect.Goku:
                this.gameObject.SetActive(!(thisLevel <= GameManager.Instance.GokuTransformLevel));
                break;
            case CharacterToSelect.Vegeta:
                this.gameObject.SetActive(!(thisLevel <= GameManager.Instance.VegetaTransformLevel));
                break;
            case CharacterToSelect.Gohan:
                this.gameObject.SetActive(!(thisLevel <= GameManager.Instance.GohanTransformLevel));
                break;
            case CharacterToSelect.Trunk:
                this.gameObject.SetActive(!(thisLevel <= GameManager.Instance.TrunkTransformLevel));
                break;
        }
    }
}
