using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// RequireComponent() método para garantir que outros componentes necessários o script também está anexado
/// método para o início de seus scripts, esse script será adicionado ao menu de componentes no editor do Unity.
/// </summary>
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class CollisionScript : MonoBehaviour
{
    //ajusta a velocidade e gravidade
    public float speed = 6.0f;
    public float gravity = -9.8f;
    //variavel para referenciar o controle do personagem
    private CharacterController _charController;
    
    void Start()
    {
        //acessa outros componentes associados aos mesmo objeto
        _charController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
       //movimento segundo o mapeamento do teclado para os eixos horizontal e vertical (Edit-> Project Settings -> Input Manager)
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
       // Limite o movimento diagonal com a mesma velocidade que movimento ao longo de um eixo.
        movement = Vector3.ClampMagnitude(movement, speed);
        //utiliza a gravidade
        movement.y = gravity;
        /*Ajustar o código de movimento para torná - lo independente da taxa de quadros ( frame update).
         * Significa que a velocidade do movimento não depende da taxa de quadros do jogo.
         * O caminho para conseguir isso, não é necessáriamente aplicar o mesmo valor de velocidade em todas as taxas de quadros. 
         * Em vez disso, dimensionar o valor da velocidade maior ou menor dependendo da rapidez com que o computador é executado.
         * Isso é alcançado multiplicando o valor da velocidade por outro valor denominado deltaTime.
        */
        movement *= Time.deltaTime;
        //transforma o vetor de movimento de local para global ( eixos da cena e não do objeto)
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
