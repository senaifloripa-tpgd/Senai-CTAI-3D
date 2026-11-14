
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float sensibilidade = 3f;
    public float distancia = 5f;
    public float altura = 2f;

    private float rotacaoX;
    private float rotacaoY = 15f;

    void Start()
    {
        rotacaoX = player.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Girar câmera segurando M1
        if (Input.GetMouseButton(0))
        {
            rotacaoX += Input.GetAxis("Mouse X") * sensibilidade;
            rotacaoY -= Input.GetAxis("Mouse Y") * sensibilidade;

            rotacaoY = Mathf.Clamp(rotacaoY, -20f, 70f);
        }

        // Posicionamento da câmera
        Quaternion rotacao = Quaternion.Euler(rotacaoY, rotacaoX, 0);

        Vector3 alvo = player.position + Vector3.up * altura;

        Vector3 posicaoDesejada =
            alvo - (rotacao * Vector3.forward * distancia);

        transform.position = posicaoDesejada;
        transform.LookAt(alvo);
    }
}
