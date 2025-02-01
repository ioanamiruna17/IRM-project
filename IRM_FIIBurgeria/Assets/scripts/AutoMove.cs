using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Animator animator;
    public Transform startPoint;
    public Transform finishPoint;
    public float speed = 2f;
    private bool isMoving = false;

    private static AutoMove instance; // Instanță statică pentru acces global

    void Awake()
    {
        instance = this; // Salvăm instanța curentă
    }

    void Start()
    {
        animator.SetBool("isWalking", false);
    }

    public static void StartWalking()
    {
        if (instance != null && !instance.isMoving)
        {
            // 🔹 Setăm poziția inițială și activăm animația
            instance.transform.position = instance.startPoint.position;
            instance.animator.SetBool("isWalking", true);
            instance.isMoving = true;

            UnityEngine.Debug.Log("AutoMove: Personajul începe să meargă.");
        }
        else
        {
            UnityEngine.Debug.LogWarning("AutoMove: Instanța este null sau deja se mișcă.");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPoint.position, speed * Time.deltaTime);

            // 🔹 Oprim animația și mișcarea când ajunge la destinație
            if (Vector3.Distance(transform.position, finishPoint.position) < 0.1f)
            {
                animator.SetBool("isWalking", false);
                isMoving = false;
                UnityEngine.Debug.Log("AutoMove: Personajul a ajuns la destinație.");
            }
        }
    }
}
