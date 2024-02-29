using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScript : MonoBehaviour
{
    public GameObject questionParticle;
    public void click()
    {
        Instantiate(questionParticle, transform.position, transform.rotation);
    }
}
