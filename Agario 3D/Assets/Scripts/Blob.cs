using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    float size = 1;
    public bool isPlayer;
    public bool hasBoost;
    public float speed;
    public int score;
    Color color;
    Blob closestBlob;
    public bool isFood;

    public void Resize(float s)
    {
        transform.localScale = new Vector3(s, s, s);
        size = s;
        speed = .1f;
        if(size<3)
        speed = .1f / (size);
        else
        {
            speed = .025f;
        }
    }
	// Use this for initialization
	void Start () {
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Resize(size);
        GetComponentInChildren<Renderer>().material.SetColor("Color_81F6A261" ,color);
        
	}

    public void Split()
    {
        size = size / 2;
        Resize(size);
    }
    private void Awake()
    {
        if(!isPlayer)
            FindClosestBlob();
    }

    void FindClosestBlob()
    {
        if (!isPlayer)
        {
            float closestPos = Mathf.Infinity;
            foreach (Blob b in LevelManager.manager.blobs)
            {
                if (b != this)
                {
                    if (Vector3.Distance(b.transform.position, this.transform.position) < closestPos)
                    {
                        closestBlob = b;
                        closestPos = Vector3.Distance(b.transform.position, this.transform.position);

                    }
                }

            }
        }       
    }

    private void Update()
    {
        if(isPlayer || isFood)
        {
            return;
        }
        if (closestBlob != null)
        {
            FindClosestBlob();
            Follow(closestBlob);
        }
  
            
    }
    public void Follow(Blob other)
    {
        
        if(other.size< this.size)
        {
            transform.position = Vector3.MoveTowards(this.transform.position,other.transform.position, speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(this.transform.position, other.transform.position, -speed);
        }
    }
	
	void Eat(Blob other)
    {
        if(other== null)
        {
            return;
        }
        if (1.1f*other.size < this.size )
        {
            LevelManager.manager.RemoveBlob(other);
            size += other.size/1.5f;
           
            Resize(size);
            score += (int) (other.size * 100);
            if(UIManager.ui != null)
            UIManager.ui.GetHighscores(this);

            if (isPlayer)
            {
                UIManager.ui.SetPlayerScore(score);
                LevelManager.manager.eatSound.Play();
            }

        }
        FindClosestBlob();
    }

    public IEnumerator Boost()
    {
        hasBoost = false;
        speed *= 2f;
        yield return new WaitForSeconds(5f);
        speed /= 2f;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blob"))
        {
            Eat(other.GetComponent<Blob>());
        }
        else if (other.CompareTag("PowerUp"))
        {
            hasBoost = true;
            Destroy(other.gameObject);
        }
        else 
        {
           
            LevelManager.manager.RemoveBlob(this);
        }
    }
}
