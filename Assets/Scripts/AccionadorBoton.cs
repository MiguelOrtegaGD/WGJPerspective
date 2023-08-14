using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionadorBoton : MonoBehaviour
{
    public Animator _boton;
    public Animator _plataforma;

    private void Start()
    {
        _boton.SetBool("Boton", false);
        _plataforma.SetBool("Hola", false);
    }

    private void OnTriggerEnter(Collider other)
    {   
        _boton.SetBool("Boton", true);
        _plataforma.SetBool("Hola", true);
    }
}
