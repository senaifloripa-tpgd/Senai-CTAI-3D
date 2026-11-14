
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 5f;
    public float velocidadeCorrida = 9f;

    [Header("Pulo")]
    public float alturaPulo = 2f;
    public float gravidade = -9.81f;

    [Header("Camera")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocidadeVertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool noChao = controller.isGrounded;

        if (noChao && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f;
        }

        // Entrada do jogador
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Direção baseada na câmera
        Vector3 frente = cameraTransform.forward;
        Vector3 direita = cameraTransform.right;

        // Ignora a inclinação vertical da câmera
        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        Vector3 movimento = frente * z + direita * x;
        movimento = Vector3.ClampMagnitude(movimento, 1f);

        // Corrida
        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift)
            ? velocidadeCorrida
            : velocidade;

        controller.Move(movimento * velocidadeAtual * Time.deltaTime);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            velocidadeVertical.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        // Gravidade
        velocidadeVertical.y += gravidade * Time.deltaTime;

        controller.Move(velocidadeVertical * Time.deltaTime);
    }
}
