using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRing : MonoBehaviour {

	public void NextStage()
    {
        gameObject.transform.localScale = gameObject.transform.localScale * .8f;
    }
}
