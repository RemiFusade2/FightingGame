using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartBehaviour : MonoBehaviour
{
    private CharacterBehaviour character;

    void Start()
    {
        character = this.transform.parent.GetComponent<CharacterBehaviour>();
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag.Equals("HitBox") && !col.transform.gameObject.Equals(character.gameObject))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(WaitAndReturnToColor(1.0f, Color.white));
            character.GetsHit(1.0f, true);
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HitBox") && !other.transform.gameObject.Equals(character.gameObject))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(WaitAndReturnToColor(1.0f, Color.white));
            character.GetsHit(1.0f, other.transform.parent.transform.position);
        }
    }

    IEnumerator WaitAndReturnToColor(float delay, Color c)
    {
        yield return new WaitForSeconds(delay);
        this.GetComponent<Renderer>().material.color = c;
    }
}
