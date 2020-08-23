using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] private Sprite[] _debrisSprites = null; 
    [SerializeField] private Vector2[] _targetLocations = null;
    
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private BoxCollider2D _collider = null;
    [SerializeField] private Interactable _interactable = null;
    [SerializeField] Effect _debrisLandEffect = null;
    


    Vector2 _targetLocation;

    public static List<Destructable> _shelves; //todo set programatically

    
    void Start()
    {        
        //programatically set the shelves if not already set TODO:
        //if (_shelves == null)
        //    find object by tag - shelves and add to the list
        // 

        //get random sprite
        int debrisSpriteIndex = Random.Range(0, _debrisSprites.Length);
        _spriteRenderer.sprite = _debrisSprites[debrisSpriteIndex];

        SetTargetLocation();
    }

    private void SetTargetLocation(){
        //TODO: Add chance to smash a shelf

        //get random location (todo: from new list)
        int targetLocIndex = Random.Range(0, _targetLocations.Length);
        _targetLocation = _targetLocations[targetLocIndex];
                
        //trigger spawn
        StartCoroutine(SpawnDebris());
    }   

    private IEnumerator SpawnDebris()
    {
        //shoot debris up in the air from origin point
        Vector2 shootUpTarget;
        shootUpTarget.y = transform.position.y + 10;
        shootUpTarget.x = _targetLocation.x;

        while (Vector2.Distance(transform.position, shootUpTarget) > 0.1f){
            transform.position = Vector2.MoveTowards(transform.position, shootUpTarget, 20f * Time.deltaTime);
               yield return null;
        }

        //bring debris back down to target location
        while (Vector2.Distance(transform.position, _targetLocation) > 0.1f){
            transform.position = Vector2.MoveTowards(transform.position, _targetLocation, 20f * Time.deltaTime);
               yield return null;
        }

        //play effect when debris hits ground
        Instantiate(_debrisLandEffect, _targetLocation, Quaternion.identity);

        //turn on collider2D & make it match size of the sprite 
        _collider.enabled = true;
        _collider.offset = new Vector2(0, 0);
        _collider.size = new Vector2(_spriteRenderer.bounds.size.x / transform.lossyScale.x,
                                     _spriteRenderer.bounds.size.y / transform.lossyScale.y);

        //turn on interactable (TODO: make new success outcome - destroy self)
        _interactable.enabled = true;

        //(todo - do I need to make a trigger check for no trigger for the interactable? - maybe trigger should be button press in range)
        
    }


    public void DestoryThis(){
        Destroy(gameObject);
    }

}

/* rand locs
-0.13,-1.4
-7.28, -3.63
-5.97, -1.89
1.93, 0.01
-1.71, -0.78
4.65, -3.89
*/