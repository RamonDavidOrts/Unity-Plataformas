﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float xInicial, yInicial;
    float velocidad = 5f;
    float velocidadSalto = 60f;
    float alturaPersonaje;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento lateral
        float horizontal = Input.GetAxis("Horizontal");
        if ((horizontal > 0.1f) || (horizontal < -0.1f))
            anim.Play("personajeCorriendo");
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
        
        // Salto
        float salto = Input.GetAxis("Jump");
        if (salto > 0)
        {
            RaycastHit2D impacto = Physics2D.Raycast(
                transform.position, 
                new Vector2(0, -1));
            if (impacto.collider != null)
            {
                float distanciaAlSuelo = impacto.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
                if (tocandoElSuelo)
                {
                    Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);
                    GetComponent<Rigidbody2D>().AddForce(fuerzaSalto);
                }
            }
            
        }
    }

    public void Recolocar()
    {
        transform.position = new Vector2(xInicial, yInicial);
    }
}
