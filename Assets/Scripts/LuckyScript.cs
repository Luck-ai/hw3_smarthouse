using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuckyScript : MonoBehaviour
{
    [Header("Menus")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject livingRoomMenu;

    [SerializeField] private Button livingRoomButton;
    [SerializeField] private Button livingRoomBackButton;
    [Header("Menu Theme")]

    [SerializeField] private Color panelColor = new Color32(13, 17, 24, 190);
    [SerializeField] private Color sectionColor = new Color32(32, 39, 56, 184);
    [SerializeField] private Color buttonNormalColor = new Color32(47, 46, 137, 235);
    [SerializeField] private Color buttonHighlightedColor = new Color32(81, 75, 196, 255);
    [SerializeField] private Color buttonPressedColor = new Color32(29, 27, 94, 255);
    [SerializeField] private Color buttonSelectedColor = new Color32(62, 58, 167, 255);
    [SerializeField] private Color buttonDisabledColor = new Color32(54, 56, 71, 128);
    [SerializeField] private Color buttonTextColor = new Color32(244, 246, 255, 255);
    [SerializeField] private Color labelTextColor = new Color32(230, 232, 242, 255);
    [SerializeField] private Color sliderBackgroundColor = new Color32(37, 50, 74, 255);
    [SerializeField] private Color sliderFillColor = new Color32(110, 99, 215, 255);
    [SerializeField] private Color sliderHandleColor = new Color32(244, 246, 255, 255);
    [Header("Main Gate")]



    [SerializeField] private GameObject leftGate;
    [SerializeField] private GameObject rightGate;
    [SerializeField, Range(0.1f, 5f)] private float gateRotationSpeed = 1.2f;

    private TMP_Text mainGateButtonText;
    private Quaternion initialLeftGatePosition;
    private Quaternion initialRightGatePosition;
    private Quaternion openLeftGatePosition;
    private Quaternion openRightGatePosition;

    [SerializeField] private Button mainGateButton;

    [Header("House Gate")]

    [SerializeField] private GameObject leftHouseGate;
    [SerializeField] private GameObject rightHouseGate;
    [SerializeField, Range(0.1f, 5f)] private float houseGateRotationSpeed = 1.2f;

    [SerializeField] private Button houseGateButton;

    private bool isHouseGateOpen = false;
    private TMP_Text houseGateButtonText;

    private Quaternion initialLeftHouseGatePosition;
    private Quaternion initialRightHouseGatePosition;
    private Quaternion openLeftHouseGatePosition;
    private Quaternion openRightHouseGatePosition;
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

    [Header("Sofas")]

    [SerializeField] private GameObject sofa1;
    [SerializeField] private GameObject sofa2;

    [SerializeField] private Slider sofa1Sliderx;
    [SerializeField] private TMP_Text sofa1SliderxText;
    [SerializeField] private Slider sofa2Sliderx;
    [SerializeField] private TMP_Text sofa2SliderxText;

    [SerializeField] private Slider sofa2Slidery;
    [SerializeField] private TMP_Text sofa2SlideryText;
    [SerializeField] private Slider sofa1Slidery;
    [SerializeField] private TMP_Text sofa1SlideryText;

    private float Sofa1Changedx;
    private float Sofa1Changedz;
    private float Sofa2Changedx;
    private float Sofa2Changedz;

    private Vector3 sofa1InitialPosition;
    private Vector3 sofa2InitialPosition;
    private Vector3 tableInitialPosition;

    private Vector3 sofa1newPosition;
    private Vector3 sofa2newPosition;
    private Vector3 tablenewPosition;

    [Header("Table")]
    [SerializeField] private GameObject table;
    [SerializeField] private Slider tableSliderx;
    [SerializeField] private TMP_Text tableSliderxText;
    [SerializeField] private Slider tableSlidery;
    [SerializeField] private TMP_Text tableSlideryText;
    private float TableChangedx;
    private float TableChangedz;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ShowMainMenu();
        ApplyMenuTheme();

        livingRoomButton.onClick.AddListener(ShowLivingRoomMenu);
        livingRoomBackButton.onClick.AddListener(ShowMainMenu);

        sofa1InitialPosition = sofa1.transform.position;
        sofa2InitialPosition = sofa2.transform.position;
        tableInitialPosition = table.transform.position;

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
        tvButtonText = tvButton.GetComponentInChildren<TMP_Text>();
        tvButton.onClick.AddListener(ToggleTv);
        UpdateTvButtonText();

        sofa1Sliderx.minValue = -10f;
        sofa1Sliderx.maxValue = 10f;
        sofa2Sliderx.minValue = -10f;
        sofa2Sliderx.maxValue = 10f;
        tableSliderx.minValue = -10f;
        tableSliderx.maxValue = 10f;
        sofa1Slidery.minValue = -10f;
        sofa1Slidery.maxValue = 10f;
        sofa2Slidery.minValue = -10f;
        sofa2Slidery.maxValue = 10f;
        tableSlidery.minValue = -10f;
        tableSlidery.maxValue = 10f;

        sofa1Sliderx.onValueChanged.AddListener(OnSofa1SliderXChanged);
        sofa2Sliderx.onValueChanged.AddListener(OnSofa2SliderXChanged);
        sofa1Slidery.onValueChanged.AddListener(OnSofa1SliderYChanged);
        sofa2Slidery.onValueChanged.AddListener(OnSofa2SliderYChanged);
        tableSliderx.onValueChanged.AddListener(OnTableSliderXChanged);
        tableSlidery.onValueChanged.AddListener(OnTableSliderYChanged);

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

    public void ShowMainMenu()
    {
        ShowOnly(mainMenu);
    }

    public void ShowLivingRoomMenu()
    {
        ShowOnly(livingRoomMenu);
    }


    private void ShowOnly(GameObject selectedMenu)
    {
        mainMenu.SetActive(selectedMenu == mainMenu);
        livingRoomMenu.SetActive(selectedMenu == livingRoomMenu);
    }

    public void ApplyMenuTheme()
    {
        ApplyMenuThemeTo(mainMenu);
        ApplyMenuThemeTo(livingRoomMenu);
    }

    private void ApplyMenuThemeTo(GameObject menu)
    {
        if (menu == null)
        {
            return;
        }

        Image rootImage = menu.GetComponent<Image>();
        if (rootImage != null)
        {
            rootImage.color = panelColor;
        }

        foreach (Button button in menu.GetComponentsInChildren<Button>(true))
        {
            Image buttonImage = button.targetGraphic as Image;
            if (buttonImage != null)
            {
                buttonImage.color = Color.white;
            }

            ColorBlock colors = button.colors;
            colors.normalColor = buttonNormalColor;
            colors.highlightedColor = buttonHighlightedColor;
            colors.pressedColor = buttonPressedColor;
            colors.selectedColor = buttonSelectedColor;
            colors.disabledColor = buttonDisabledColor;
            colors.colorMultiplier = 1f;
            colors.fadeDuration = 0.08f;
            button.colors = colors;

            foreach (TMP_Text buttonLabel in button.GetComponentsInChildren<TMP_Text>(true))
            {
                buttonLabel.color = buttonTextColor;
            }
        }

        foreach (Slider slider in menu.GetComponentsInChildren<Slider>(true))
        {
            Transform background = slider.transform.Find("Background");
            if (background != null && background.TryGetComponent(out Image backgroundImage))
            {
                backgroundImage.color = sliderBackgroundColor;
            }

            if (slider.fillRect != null && slider.fillRect.TryGetComponent(out Image fillImage))
            {
                fillImage.color = sliderFillColor;
            }

            if (slider.handleRect != null && slider.handleRect.TryGetComponent(out Image handleImage))
            {
                handleImage.color = sliderHandleColor;
            }
        }

        foreach (Image image in menu.GetComponentsInChildren<Image>(true))
        {
            if (image == rootImage ||
                image.GetComponentInParent<Button>() != null ||
                image.GetComponentInParent<Slider>() != null)
            {
                continue;
            }

            image.color = sectionColor;
        }

        foreach (TMP_Text label in menu.GetComponentsInChildren<TMP_Text>(true))
        {
            if (label.GetComponentInParent<Button>() == null)
            {
                label.color = labelTextColor;
            }
        }
    }


    private void OnSofa1SliderXChanged(float value)
    {
        Sofa1Changedx = value;
        sofa1SliderxText.text = $"Sofa 1 X: {value:F2}";
    }

    private void OnSofa1SliderYChanged(float value)
    {
        Sofa1Changedz = value;
        sofa1SlideryText.text = $"Sofa 1 Z: {value:F2}";
    }

    private void OnSofa2SliderXChanged(float value)
    {
        Sofa2Changedx = value;
        sofa2SliderxText.text = $"Sofa 2 X: {value:F2}";
    }

    private void OnSofa2SliderYChanged(float value)
    {
        Sofa2Changedz = value;
        sofa2SlideryText.text = $"Sofa 2 Z: {value:F2}";
    }

    private void OnTableSliderXChanged(float value)
    {
        TableChangedx = value;
        tableSliderxText.text = $"Table X: {value:F2}";
    }

    private void OnTableSliderYChanged(float value)
    {
        TableChangedz = value;
        tableSlideryText.text = $"Table Z: {value:F2}";
    }



    // Update is called once per frame
    private void Update()
    {
        sofa1newPosition = sofa1InitialPosition + new Vector3(Sofa1Changedx, 0f, Sofa1Changedz);
        sofa2newPosition = sofa2InitialPosition + new Vector3(Sofa2Changedx, 0f, Sofa2Changedz);
        tablenewPosition = tableInitialPosition + new Vector3(TableChangedx, 0f, TableChangedz);

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

        sofa1.transform.position = Vector3.MoveTowards(sofa1.transform.position, sofa1newPosition, Time.deltaTime * 0.5f);
        sofa2.transform.position = Vector3.MoveTowards(sofa2.transform.position, sofa2newPosition, Time.deltaTime * 0.5f);
        table.transform.position = Vector3.MoveTowards(table.transform.position, tablenewPosition, Time.deltaTime * 0.5f);
    }
}
