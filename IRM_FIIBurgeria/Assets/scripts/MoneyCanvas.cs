using UnityEngine;
using TMPro;
using System.Diagnostics;

public class MoneyCanvas : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI valueText;

    [Header("Audio")]
    public AudioSource moneySound;  // 🔹 Adăugăm referința la AudioSource

    [Header("Character Movement")]
    public Animator animator;
    public Transform character;
    public Transform startPoint;
    public Transform midPoint;
    public Transform finishPoint;
    public float speed = 2f;

    private int score = 0;
    private bool isMoving = false;
    private bool moneyAddedForTeleport = false;
    private bool reachedMidPoint = false;

    void Start()
    {
        UpdateText();
        animator.SetBool("isWalking", false);
        StartWalking();
    }

    void Update()
    {
        if (TeleportObject.teleported && !moneyAddedForTeleport)
        {
            AddMoney(30);
            moneyAddedForTeleport = true;
            StartWalking();
            TeleportObject.teleported = false;
        }

        if (isMoving)
        {
            MoveCharacter();
        }

        if (!TeleportObject.teleported)
        {
            moneyAddedForTeleport = false;
        }
    }

    void AddMoney(int amount)
    {
        score += amount;
        UnityEngine.Debug.Log($"Bani actualizați: {score} $");
        UpdateText();

        UnityEngine.Debug.Log(score);

        // 🔹 Redăm sunetul de colectare a banilor
        if (moneySound != null && score!=0)
        {
            moneySound.Play();
        }
        else
        {
            UnityEngine.Debug.LogWarning("Nu ai asignat un sunet pentru bani!");
        }
    }

    void UpdateText()
    {
        valueText.text = "Money: " + score.ToString() + " $";
    }

    void StartWalking()
    {
        if (!isMoving)
        {
            character.position = startPoint.position;
            reachedMidPoint = false;
            character.rotation = Quaternion.Euler(0, 0, 0);
            animator.Rebind();
            animator.Update(0);
            animator.SetBool("isWalking", true);
            isMoving = true;
        }
    }

    void MoveCharacter()
    {
        if (!reachedMidPoint)
        {
            character.position = Vector3.MoveTowards(character.position, midPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(character.position, midPoint.position) < 0.1f)
            {
                reachedMidPoint = true;
                character.rotation = Quaternion.Euler(0, 270, 0);
            }
        }
        else
        {
            character.position = Vector3.MoveTowards(character.position, finishPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(character.position, finishPoint.position) < 0.1f)
            {
                animator.SetBool("isWalking", false);
                isMoving = false;
                character.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
