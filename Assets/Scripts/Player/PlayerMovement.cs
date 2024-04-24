using System;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Player
{
    public class PlayerMovement
    {
        /// <summary>
        /// La función Jump se encarga de hacer saltar al jugador comprobando si el jugador aplastó la tecla de salto y 
        /// si el jugador está en el suelo.
        /// </summary>
        /// <param name="rigidBody"> Envía una referencia del Rigidbody del personaje</param>
        /// <param name="jumpForce">Envía la fuerza con la que saltará el personaje. Equivalente a la fuerza contraria a la gravedad.</param>
        /// <param name="isGrounded">Envía un booleano que es true si el personaje se encuentra en el suelo. Caso contrario es false</param>
        /// <param name="keyCode">Envía un booleano que indica que la tecla de salto fue tecleada.</param>
        public void Jump(Rigidbody2D rigidBody, float jumpForce, bool isGrounded, bool keyCode) 
        {
            if (keyCode && isGrounded)
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// IsNotJumping es una función que devuelve un booleano que indica si el jugador está en el suelo o no.
        /// </summary>
        /// <param name="transform">Envía una referencia Transform que se accede mediante la clase Player</param>
        /// <returns>True si el jugador está en el suelo, caso contrario False.</returns>
        public bool IsNotJumping(UnityEngine.Transform transform)
        {
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// HorizontalMovement es una función que devuelve un float que indica la dirección en la que se mueve el jugador.
        /// La dirección es obtenida en el eje horizontal.
        /// </summary>
        /// <param name="isKeyUp">Booleano que representa si la tecla no está siendo aplastada.</param>
        /// <param name="horizontalAxisName">String que representa el nombre del eje horizontal que usará el jugador</param>
        /// <returns>Devuelve un valor flotante.</returns>
        public float HorizontalMovement(bool isKeyUp, String horizontalAxisName)
        {
            if(isKeyUp)
            {
                return 0f;
            } else
            {
                return Input.GetAxisRaw(horizontalAxisName);
            }
        }

        /// <summary>
        /// isHorizontalKeyDown es una función que devuelve un booleano que indica si el jugador aplastó la tecla de movimiento horizontal.
        /// </summary>
        /// <param name="horizontalAxisName">String que representa el nombre del eje horizontal que usará el jugador</param>
        /// <returns>True si la tecla es tecleada, caso contrario devuelve false.</returns>
        public bool isHorizontalKeyDown(String horizontalAxisName)
        {
            if(horizontalAxisName == "Horizontal")
            {
                return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
            } else
            {
                return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D);
            }
        }


    }
}