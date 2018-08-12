using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float score = 0;
    public float speed = .1f;

    public static PlayerController reff;
    Blob pBlob;
    Rigidbody rb;
    CameraOrbit cam;
   
    // Use this for initialization

    private void Awake()
    {
        reff = this;
    }
    void Start () {
        cam = GetComponentInChildren<CameraOrbit>();
        rb=  GetComponent<Rigidbody>();
        pBlob = GetComponent<Blob>();
        pBlob.isPlayer = true;
        pBlob.Resize(.6f);
        LevelManager.manager.blobs.Add(pBlob);
        this.name = "You!";
    }

    void Move()
    {
        speed = pBlob.speed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        this.transform.forward = cam.transform.forward;

        transform.Translate(new Vector3(h, 0, 1)*speed);
    }

    private void Update()
    {
        Move();
        UIManager.ui.PowerUp(pBlob.hasBoost);
        if (Input.GetMouseButtonDown(0)&& pBlob.hasBoost)
        {
            
            pBlob.StartCoroutine("Boost");
        }
    }

    private void OnDestroy()
    {
        UIManager.ui.DeathScreen();
        LevelManager.manager.popSound.Play();
    }


}
