using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuckyScript : MonoBehaviour
{
    [Header("Main Gate")]

    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField] private float gateRotationSpeed = 1.2f;

    [SerializeField] private Button mainGateButton;

    private bool isGateOpen = false;
    private TMP_Text mainGateButtonText;

    private Quaternion initialLeftGatePosition;
    private Quaternion initialRightGatePosition;
    private Quaternion openLeftGatePosition;
    private Quaternion openRightGatePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialLeftGatePosition = leftGate.transform.rotation;
        initialRightGatePosition = rightGate.transform.rotation;
        openLeftGatePosition = initialLeftGatePosition * Quaternion.Euler(0f, 108f, 0f);
        openRightGatePosition = initialRightGatePosition * Quaternion.Euler(0f, -100f, 0f);
        mainGateButtonText = mainGateButton.GetComponentInChildren<TMP_Text>();
        mainGateButton.onClick.AddListener(OpenGate);
    }

    private void OpenGate()
    {
        isGateOpen = !isGateOpen;

        mainGateButtonText.text = isGateOpen ? "Close Gate" : "Open Gate";

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetLeftRotation = isGateOpen ? openLeftGatePosition : initialLeftGatePosition;
        Quaternion targetRightRotation = isGateOpen ? openRightGatePosition : initialRightGatePosition;

        leftGate.transform.rotation = Quaternion.RotateTowards(
            leftGate.transform.rotation,
            targetLeftRotation,
            gateRotationSpeed * Time.deltaTime);

        rightGate.transform.rotation = Quaternion.RotateTowards(
            rightGate.transform.rotation,
            targetRightRotation,
            gateRotationSpeed * Time.deltaTime);
    }
}
