using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuckyScript : MonoBehaviour
{
    [Header("Main Gate")]

    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField, Range(0.1f, 5f)] private float gateRotationSpeed = 1.2f;

    [SerializeField] private Button mainGateButton;

    [Header("House Gate")]

    [SerializeField] private GameObject leftHouseGate;
    [SerializeField] private GameObject rightHouseGate;
    [SerializeField, Range(0.1f, 5f)] private float houseGateRotationSpeed = 1.2f;

    [SerializeField] private Button houseGateButton;


    private bool isGateOpen = false;
    [Header("TV")]

    [SerializeField] private Renderer tvRenderer;
    [SerializeField] private Button tvButton;

    private const int TvScreenMaterialIndex = 0;

    private static readonly int BaseColorProperty =
        Shader.PropertyToID("_BaseColor");

    private static readonly int EmissionColorProperty =
        Shader.PropertyToID("_EmissionColor");

    private bool isTvOn = true;
    private TMP_Text tvButtonText;
    private MaterialPropertyBlock tvPropertyBlock;

    private TMP_Text mainGateButtonText;

    private Quaternion initialLeftGatePosition;
    private Quaternion initialRightGatePosition;
    private Quaternion openLeftGatePosition;
    private Quaternion openRightGatePosition;

    private bool isHouseGateOpen = false;
    private TMP_Text houseGateButtonText;

    private Quaternion initialLeftHouseGatePosition;
    private Quaternion initialRightHouseGatePosition;
    private Quaternion openLeftHouseGatePosition;
    private Quaternion openRightHouseGatePosition;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        initialLeftGatePosition = leftGate.transform.rotation;
        initialRightGatePosition = rightGate.transform.rotation;
        openLeftGatePosition = initialLeftGatePosition * Quaternion.Euler(0f, 108f, 0f);
        openRightGatePosition = initialRightGatePosition * Quaternion.Euler(0f, -100f, 0f);
        mainGateButtonText = mainGateButton.GetComponentInChildren<TMP_Text>();
        mainGateButton.onClick.AddListener(OpenGate);

        initialLeftHouseGatePosition = leftHouseGate.transform.rotation;
        initialRightHouseGatePosition = rightHouseGate.transform.rotation;
        openLeftHouseGatePosition = initialLeftHouseGatePosition * Quaternion.Euler(0f, 0f, -100f);
        openRightHouseGatePosition = initialRightHouseGatePosition * Quaternion.Euler(0f, 0f, 100f);
        houseGateButtonText = houseGateButton.GetComponentInChildren<TMP_Text>();
        houseGateButton.onClick.AddListener(OpenHouseGate);
        houseGateButtonText.text = isHouseGateOpen ? "Close House Gate" : "Open House Gate";

        tvPropertyBlock = new MaterialPropertyBlock();
        ApplyTvState();

        if (tvButton != null)
        {
            tvButtonText = tvButton.GetComponentInChildren<TMP_Text>();
            tvButton.onClick.AddListener(ToggleTv);
            UpdateTvButtonText();
        }
    }

    private void OpenGate()
    {
        isGateOpen = !isGateOpen;

        mainGateButtonText.text = isGateOpen ? "Close Gate" : "Open Gate";

    }

    private void OpenHouseGate()
    {
        isHouseGateOpen = !isHouseGateOpen;

        houseGateButtonText.text = isHouseGateOpen ? "Close House Gate" : "Open House Gate";
    }

    public void ToggleTv()
    {
        isTvOn = !isTvOn;
        ApplyTvState();
        UpdateTvButtonText();
    }

    private void ApplyTvState()
    {
        if (tvRenderer == null)
        {
            return;
        }

        tvRenderer.GetPropertyBlock(tvPropertyBlock, TvScreenMaterialIndex);

        Color screenColor = isTvOn ? Color.white : Color.black;
        tvPropertyBlock.SetColor(BaseColorProperty, screenColor);
        tvPropertyBlock.SetColor(EmissionColorProperty, screenColor);

        tvRenderer.SetPropertyBlock(tvPropertyBlock, TvScreenMaterialIndex);
    }

    private void UpdateTvButtonText()
    {
        if (tvButtonText != null)
        {
            tvButtonText.text = isTvOn ? "Turn TV Off" : "Turn TV On";
        }
    }


    // Update is called once per frame
    private void Update()
    {
        Quaternion targetLeftRotation = isGateOpen ? openLeftGatePosition : initialLeftGatePosition;
        Quaternion targetRightRotation = isGateOpen ? openRightGatePosition : initialRightGatePosition;

        leftGate.transform.rotation = Quaternion.Slerp(
            leftGate.transform.rotation,
            targetLeftRotation,
            gateRotationSpeed * Time.deltaTime);

        rightGate.transform.rotation = Quaternion.Slerp(
            rightGate.transform.rotation,
            targetRightRotation,
            gateRotationSpeed * Time.deltaTime);

        Quaternion targetLeftHouseRotation = isHouseGateOpen ? openLeftHouseGatePosition : initialLeftHouseGatePosition;
        Quaternion targetRightHouseRotation = isHouseGateOpen ? openRightHouseGatePosition : initialRightHouseGatePosition;

        leftHouseGate.transform.rotation = Quaternion.Slerp(
            leftHouseGate.transform.rotation,
            targetLeftHouseRotation,
            houseGateRotationSpeed * Time.deltaTime);

        rightHouseGate.transform.rotation = Quaternion.Slerp(
            rightHouseGate.transform.rotation,
            targetRightHouseRotation,
            houseGateRotationSpeed * Time.deltaTime);
    }
}
