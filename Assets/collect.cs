//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Collect : MonoBehaviour
//{
//    private BoxCollider2D boxCollider;
//    private SpriteRenderer collectRenderer;
//    public GameObject collectPrefab;
//    void Start()
//    {
//        boxCollider = GetComponent<BoxCollider2D>();
//        collectRenderer = GetComponent<SpriteRenderer>();
//    }

//    void Update()
//    {

//    }

//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            ExpandColliderToFullScreen();
//        }
//    }

//    void ExpandColliderToFullScreen()
//    {
//        Camera mainCamera = Camera.main;
//        if (mainCamera != null)
//        {
//            float screenHeight = 2f * mainCamera.orthographicSize;
//            float screenWidth = screenHeight * mainCamera.aspect;

//            boxCollider.size = new Vector2(screenWidth, screenHeight);
//            boxCollider.offset = Vector2.zero;
//            boxCollider.isTrigger = true;

//            StartCoroutine(ShowEnemiesTemporarily());

//            // Disable the collect object
//            collectRenderer.enabled = false;

//            // Destroy the collect object after 5 seconds
//            StartCoroutine(DestroyAfterDelay(5f));
//        }
//    }

//    IEnumerator ShowEnemiesTemporarily()
//    {
//        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f);

//        List<SpriteRenderer> enemyRenderers = new List<SpriteRenderer>();

//        foreach (Collider2D collider in colliders)
//        {
//            if (collider.CompareTag("Enemy"))
//            {
//                SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
//                if (spriteRenderer != null)
//                {
//                    spriteRenderer.enabled = true;
//                    enemyRenderers.Add(spriteRenderer);
//                }
//            }
//        }

//        yield return new WaitForSeconds(2f);

//        foreach (SpriteRenderer renderer in enemyRenderers)
//        {
//            renderer.enabled = false;
//        }

//        // Show the collect object again
//        gameObject.SetActive(true);
//    }

//    IEnumerator DestroyAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        // Respawn the collect object
//        RespawnCollect();
//    }

//    void RespawnCollect()
//{
//    Debug.Log("Destroying current Collect object and respawning.");
//    //Destroy(gameObject);
//    Instantiate(collectPrefab, transform.position, Quaternion.identity);
//    Debug.Log("Collect object respawned.");
//}
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    //private BoxCollider2D boxCollider;
    private SpriteRenderer collectRenderer;
    //public GameObject collectPrefab;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject eye;
    [SerializeField] float eyeWidth;
    [SerializeField] float eyeheight;
    bool status = true;
    //public BoxCollider2D boxCollider;
    void Start()
    {
       
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& status)
        {
            ExpandColliderToFullScreen();
        }
    }

    void ExpandColliderToFullScreen()
    {
        //Camera mainCamera = Camera.main;
        //if (mainCamera != null)
        //{
        //    float screenHeight = 2f * mainCamera.orthographicSize;
        //    float screenWidth = screenHeight * mainCamera.aspect;

             eye.GetComponent<BoxCollider2D>().size = new Vector2(eyeWidth, eyeheight);
        //    boxCollider.offset = Vector2.zero;
        //    boxCollider.isTrigger = true;

        //StartCoroutine(ShowEnemiesTemporarily());

        // Disable the collect object
        collectRenderer.GetComponent<SpriteRenderer>().enabled = false;
            status = false;
            // Destroy the collect object after 5 seconds
            StartCoroutine(DestroyAfterDelay(5f));
    //}
}

    IEnumerator ShowEnemiesTemporarily()
    {
        //Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f);

    

        
           
                SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            
        

        yield return new WaitForSeconds(2f);


       
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }


        // Show the collect object again
        
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Respawn the collect object
        RespawnCollect();
        Debug.Log("aaaaaa");
    }

    void RespawnCollect()
    {
        collectRenderer.GetComponent<SpriteRenderer>().enabled = true;
        status = true;
    }
}
