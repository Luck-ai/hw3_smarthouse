using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LuckyScript : MonoBehaviour
{
    [Header("Menus")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject livingRoomMenu;
    [SerializeField] private GameObject livingRoomWashroomMenu;
    [SerializeField] private GameObject masterBedroomMenu;

    [SerializeField] private Button livingRoomButton;
    [SerializeField] private Button livingRoomBackButton;
    [SerializeField] private Button livingRoomWashroomButton;
    [SerializeField] private Button livingRoomWashroomBackButton;
    [SerializeField] private Button masterBedroomMenuButton;
    [SerializeField] private Button masterBedroomBackButton;

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

    [Header("Living Room Washroom")]

    [SerializeField] private GameObject WashroomDoor;
    [SerializeField] private Button WashroomDoorButton;
    [SerializeField] private GameObject WashroomSeat;
    [SerializeField] private Button WashroomSeatButton;
    [SerializeField] private GameObject WashroomFlush;
    [SerializeField] private Button WashroomFlushButton;
    [SerializeField, Range(0.1f, 5f)] private float washroomRotationSpeed = 1.2f;
    [SerializeField] private float flushPressDistance = 0.04f;
    [SerializeField] private float flushMoveSpeed = 0.25f;

    private bool isWashroomDoorOpen;
    private bool isWashroomSeatOpen;
    private bool isWashroomFlushPressed;
    private float flushHoldTimer;
    private Quaternion initialWashroomDoorPosition;
    private Quaternion openWashroomDoorPosition;
    private Quaternion initialWashroomSeatPosition;
    private Quaternion openWashroomSeatPosition;
    private Vector3 initialWashroomFlushPosition;
    private Vector3 pressedWashroomFlushPosition;

    [Header("Living Room Washroom Middle Cabinet")]

    [SerializeField] private GameObject RightDoor;
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private Button MiddleCabinetButton;

    private bool isMiddleCabinetOpen;
    private Quaternion initialRightCabinetDoorPosition;
    private Quaternion openRightCabinetDoorPosition;
    private Quaternion initialLeftCabinetDoorPosition;
    private Quaternion openLeftCabinetDoorPosition;

    [Header("Living Room Washroom Drawers")]

    [SerializeField] private GameObject TopDrawerL;
    [SerializeField] private GameObject MiddleDrawerL;
    [SerializeField] private GameObject BottomDrawerL;
    [SerializeField] private TMP_Dropdown DrawerLDropdown;

    [SerializeField] private GameObject TopDrawerR;
    [SerializeField] private GameObject MiddleDrawerR;
    [SerializeField] private GameObject BottomDrawerR;
    [SerializeField] private TMP_Dropdown DrawerRDropdown;

    private const float ClosedDrawerY = -0.0059f;
    private const float OpenDrawerY = -0.00806f;
    private const float DrawerMoveSpeed = 0.004f;

    private int selectedLeftDrawer;
    private int selectedRightDrawer;
    private Vector3 topDrawerLClosedPosition;
    private Vector3 middleDrawerLClosedPosition;
    private Vector3 bottomDrawerLClosedPosition;
    private Vector3 topDrawerRClosedPosition;
    private Vector3 middleDrawerRClosedPosition;
    private Vector3 bottomDrawerRClosedPosition;

    [Header("Master Bedroom")]

    [SerializeField] private GameObject masterBedroomDoor;
    [SerializeField] private Button masterBedroomDoorButton;
    [SerializeField] private GameObject masterBedroomWindowLeft;
    [SerializeField] private GameObject masterBedroomWindowRight;
    [SerializeField] private Slider masterBedroomWindowSlider;
    [SerializeField] private TMP_Text masterBedroomWindowText;
    [SerializeField] private GameObject masterBedroomBlanket;
    [SerializeField] private Slider masterBedroomBlanketSlider;
    [SerializeField] private TMP_Text masterBedroomBlanketText;
    [SerializeField] private GameObject masterBedroomPillow;
    [SerializeField] private Slider masterBedroomPillowSlider;
    [SerializeField] private TMP_Text masterBedroomPillowText;
    [SerializeField] private Renderer masterBedroomGlassRenderer;
    [SerializeField] private Button masterBedroomGlassTintButton;
    [SerializeField, Range(30f, 240f)] private float masterBedroomRotationSpeed = 120f;
    [SerializeField] private float masterBedroomMoveSpeed = 1.2f;

    private bool isMasterBedroomDoorOpen;
    private bool isMasterBedroomGlassTinted;
    private float masterBedroomWindowAmount;
    private float masterBedroomBlanketAmount;
    private float masterBedroomPillowAngle;
    private Quaternion masterBedroomDoorClosedRotation;
    private Quaternion masterBedroomDoorOpenRotation;
    private Vector3 masterBedroomWindowLeftClosedPosition;
    private Vector3 masterBedroomWindowRightClosedPosition;
    private Vector3 masterBedroomBlanketClosedPosition;
    private Quaternion masterBedroomPillowClosedRotation;
    private Color masterBedroomGlassOriginalColor;
    private MaterialPropertyBlock masterBedroomGlassPropertyBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
private void Start()
    {
        ShowMainMenu();
        ApplyMenuTheme();

        livingRoomButton.onClick.AddListener(ShowLivingRoomMenu);
        livingRoomBackButton.onClick.AddListener(ShowMainMenu);
        livingRoomWashroomButton.onClick.AddListener(ShowLivingRoomWashroomMenu);
        livingRoomWashroomBackButton.onClick.AddListener(ShowLivingRoomMenu);
        masterBedroomMenuButton.onClick.AddListener(ShowMasterBedroomMenu);
        masterBedroomBackButton.onClick.AddListener(ShowMainMenu);


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

        initialWashroomDoorPosition = WashroomDoor.transform.localRotation;
        openWashroomDoorPosition = initialWashroomDoorPosition * Quaternion.Euler(0f, 0f, -90f);
        WashroomDoorButton.onClick.AddListener(ToggleWashroomDoor);
        UpdateButtonText(WashroomDoorButton, "Open Washroom", "Close Washroom", isWashroomDoorOpen);

        initialWashroomSeatPosition = WashroomSeat.transform.localRotation;
        float lidOpenAngle = 220f + WashroomSeat.transform.localEulerAngles.x;
        openWashroomSeatPosition = initialWashroomSeatPosition * Quaternion.Euler(lidOpenAngle, 0f, 0f);
        WashroomSeatButton.onClick.AddListener(ToggleWashroomSeat);
        UpdateButtonText(WashroomSeatButton, "Open Seat Lid", "Close Seat Lid", isWashroomSeatOpen);

        initialWashroomFlushPosition = WashroomFlush.transform.localPosition;
        pressedWashroomFlushPosition = initialWashroomFlushPosition + Vector3.down * flushPressDistance;
        WashroomFlushButton.onClick.AddListener(PressWashroomFlush);

        initialLeftCabinetDoorPosition = Quaternion.identity;
        initialRightCabinetDoorPosition = Quaternion.identity;
        LeftDoor.transform.localRotation = initialLeftCabinetDoorPosition;
        RightDoor.transform.localRotation = initialRightCabinetDoorPosition;
        openLeftCabinetDoorPosition = initialLeftCabinetDoorPosition * Quaternion.Euler(0f, 0f, -90f);
        openRightCabinetDoorPosition = initialRightCabinetDoorPosition * Quaternion.Euler(0f, 0f, -90f);
        MiddleCabinetButton.onClick.AddListener(ToggleMiddleCabinet);
        UpdateButtonText(MiddleCabinetButton, "Open Middle Cabinet", "Close Middle Cabinet", isMiddleCabinetOpen);

        topDrawerLClosedPosition = WithDrawerY(TopDrawerL.transform.localPosition, ClosedDrawerY);
        middleDrawerLClosedPosition = WithDrawerY(MiddleDrawerL.transform.localPosition, ClosedDrawerY);
        bottomDrawerLClosedPosition = WithDrawerY(BottomDrawerL.transform.localPosition, ClosedDrawerY);
        topDrawerRClosedPosition = WithDrawerY(TopDrawerR.transform.localPosition, ClosedDrawerY);
        middleDrawerRClosedPosition = WithDrawerY(MiddleDrawerR.transform.localPosition, ClosedDrawerY);
        bottomDrawerRClosedPosition = WithDrawerY(BottomDrawerR.transform.localPosition, ClosedDrawerY);

        TopDrawerL.transform.localPosition = topDrawerLClosedPosition;
        MiddleDrawerL.transform.localPosition = middleDrawerLClosedPosition;
        BottomDrawerL.transform.localPosition = bottomDrawerLClosedPosition;
        TopDrawerR.transform.localPosition = topDrawerRClosedPosition;
        MiddleDrawerR.transform.localPosition = middleDrawerRClosedPosition;
        BottomDrawerR.transform.localPosition = bottomDrawerRClosedPosition;

        selectedLeftDrawer = DrawerLDropdown.value;
        selectedRightDrawer = DrawerRDropdown.value;
        DrawerLDropdown.onValueChanged.AddListener(OnLeftDrawerChanged);
        DrawerRDropdown.onValueChanged.AddListener(OnRightDrawerChanged);
        masterBedroomDoorClosedRotation = masterBedroomDoor.transform.localRotation;
        masterBedroomDoorOpenRotation = masterBedroomDoorClosedRotation * Quaternion.Euler(0f, 0f, -100f);
        masterBedroomDoorButton.onClick.AddListener(ToggleMasterBedroomDoor);
        UpdateButtonText(masterBedroomDoorButton, "Open Door", "Close Door", isMasterBedroomDoorOpen);

        masterBedroomWindowLeftClosedPosition = masterBedroomWindowLeft.transform.localPosition;
        masterBedroomWindowRightClosedPosition = masterBedroomWindowRight.transform.localPosition;
        masterBedroomWindowSlider.minValue = -1f;
        masterBedroomWindowSlider.maxValue = 1f;
        masterBedroomWindowSlider.value = 0f;
        masterBedroomWindowSlider.onValueChanged.AddListener(OnMasterBedroomWindowChanged);
        OnMasterBedroomWindowChanged(0f);

        masterBedroomBlanketClosedPosition = masterBedroomBlanket.transform.localPosition;
        masterBedroomBlanketSlider.minValue = 0f;
        masterBedroomBlanketSlider.maxValue = 1f;
        masterBedroomBlanketSlider.value = 0f;
        masterBedroomBlanketSlider.onValueChanged.AddListener(OnMasterBedroomBlanketChanged);
        OnMasterBedroomBlanketChanged(0f);

        masterBedroomPillowClosedRotation = masterBedroomPillow.transform.localRotation;
        masterBedroomPillowSlider.minValue = -45f;
        masterBedroomPillowSlider.maxValue = 45f;
        masterBedroomPillowSlider.value = 0f;
        masterBedroomPillowSlider.onValueChanged.AddListener(OnMasterBedroomPillowChanged);
        OnMasterBedroomPillowChanged(0f);

        masterBedroomGlassPropertyBlock = new MaterialPropertyBlock();
        Material glassMaterial = masterBedroomGlassRenderer.sharedMaterial;
        masterBedroomGlassOriginalColor =
            glassMaterial != null && glassMaterial.HasProperty(BaseColorProperty)
                ? glassMaterial.GetColor(BaseColorProperty)
                : Color.white;
        masterBedroomGlassTintButton.onClick.AddListener(ToggleMasterBedroomGlassTint);
        ApplyMasterBedroomGlassTint();
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

    private void ToggleWashroomDoor()
    {
        isWashroomDoorOpen = !isWashroomDoorOpen;
        UpdateButtonText(WashroomDoorButton, "Open Washroom", "Close Washroom", isWashroomDoorOpen);
    }

    private void ToggleWashroomSeat()
    {
        isWashroomSeatOpen = !isWashroomSeatOpen;
        UpdateButtonText(WashroomSeatButton, "Open Seat Lid", "Close Seat Lid", isWashroomSeatOpen);
    }

    private void PressWashroomFlush()
    {
        isWashroomFlushPressed = true;
        flushHoldTimer = 0f;
    }

    private void ToggleMiddleCabinet()
    {
        isMiddleCabinetOpen = !isMiddleCabinetOpen;
        UpdateButtonText(MiddleCabinetButton, "Open Middle Cabinet", "Close Middle Cabinet", isMiddleCabinetOpen);
    }

    private void OnLeftDrawerChanged(int value)
    {
        selectedLeftDrawer = value;
    }

    private void OnRightDrawerChanged(int value)
    {
        selectedRightDrawer = value;
    }

    private static Vector3 WithDrawerY(Vector3 position, float y)
    {
        position.y = y;
        return position;
    }

    private static void UpdateButtonText(Button button, string closedText, string openText, bool isOpen)
    {
        TMP_Text label = button.GetComponentInChildren<TMP_Text>();
        if (label != null)
        {
            label.text = isOpen ? openText : closedText;
        }
    }

    private void ToggleMasterBedroomDoor()
    {
        isMasterBedroomDoorOpen = !isMasterBedroomDoorOpen;
        UpdateButtonText(masterBedroomDoorButton, "Open Door", "Close Door", isMasterBedroomDoorOpen);
    }

    private void OnMasterBedroomWindowChanged(float value)
    {
        masterBedroomWindowAmount = value;

        if (value < -0.05f)
        {
            masterBedroomWindowText.text = "Window: Left Open";
        }
        else if (value > 0.05f)
        {
            masterBedroomWindowText.text = "Window: Right Open";
        }
        else
        {
            masterBedroomWindowText.text = "Window: Closed";
        }
    }

    private void OnMasterBedroomBlanketChanged(float value)
    {
        masterBedroomBlanketAmount = value;
        masterBedroomBlanketText.text = $"Blanket Slide: {value * 100f:F0}%";
    }

    private void OnMasterBedroomPillowChanged(float value)
    {
        masterBedroomPillowAngle = value;
        masterBedroomPillowText.text = $"Pillow Rotation: {value:F0}°";
    }

    private void ToggleMasterBedroomGlassTint()
    {
        isMasterBedroomGlassTinted = !isMasterBedroomGlassTinted;
        ApplyMasterBedroomGlassTint();
    }

    private void ApplyMasterBedroomGlassTint()
    {
        Color tintColor = isMasterBedroomGlassTinted
            ? new Color(0f, 0f, 0f, masterBedroomGlassOriginalColor.a)
            : masterBedroomGlassOriginalColor;

        masterBedroomGlassRenderer.GetPropertyBlock(masterBedroomGlassPropertyBlock, 0);
        masterBedroomGlassPropertyBlock.SetColor(BaseColorProperty, tintColor);
        masterBedroomGlassRenderer.SetPropertyBlock(masterBedroomGlassPropertyBlock, 0);

        UpdateButtonText(
            masterBedroomGlassTintButton,
            "Tint Glass Black",
            "Tint Glass White",
            isMasterBedroomGlassTinted);
    }

    public void ShowMasterBedroomMenu()
    {
        ShowOnly(masterBedroomMenu);
    }

    public void ShowMainMenu()
    {
        ShowOnly(mainMenu);
    }

    public void ShowLivingRoomMenu()
    {
        ShowOnly(livingRoomMenu);
    }

    public void ShowLivingRoomWashroomMenu()
    {
        ShowOnly(livingRoomWashroomMenu);
    }

private void ShowOnly(GameObject selectedMenu)
    {
        mainMenu.SetActive(selectedMenu == mainMenu);
        livingRoomMenu.SetActive(selectedMenu == livingRoomMenu);
        livingRoomWashroomMenu.SetActive(selectedMenu == livingRoomWashroomMenu);
        masterBedroomMenu.SetActive(selectedMenu == masterBedroomMenu);
    }

public void ApplyMenuTheme()
    {
        ApplyMenuThemeTo(mainMenu);
        ApplyMenuThemeTo(livingRoomMenu);
        ApplyMenuThemeTo(livingRoomWashroomMenu);
        ApplyMenuThemeTo(masterBedroomMenu);
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

        leftGate.transform.rotation = Quaternion.Slerp(leftGate.transform.rotation, targetLeftRotation, gateRotationSpeed * Time.deltaTime);
        rightGate.transform.rotation = Quaternion.Slerp(rightGate.transform.rotation, targetRightRotation, gateRotationSpeed * Time.deltaTime);

        Quaternion targetLeftHouseRotation = isHouseGateOpen ? openLeftHouseGatePosition : initialLeftHouseGatePosition;
        Quaternion targetRightHouseRotation = isHouseGateOpen ? openRightHouseGatePosition : initialRightHouseGatePosition;

        leftHouseGate.transform.rotation = Quaternion.Slerp(leftHouseGate.transform.rotation, targetLeftHouseRotation, houseGateRotationSpeed * Time.deltaTime);
        rightHouseGate.transform.rotation = Quaternion.Slerp(rightHouseGate.transform.rotation, targetRightHouseRotation, houseGateRotationSpeed * Time.deltaTime);

        sofa1.transform.position = Vector3.MoveTowards(sofa1.transform.position, sofa1newPosition, Time.deltaTime * 0.5f);
        sofa2.transform.position = Vector3.MoveTowards(sofa2.transform.position, sofa2newPosition, Time.deltaTime * 0.5f);
        table.transform.position = Vector3.MoveTowards(table.transform.position, tablenewPosition, Time.deltaTime * 0.5f);

        Quaternion washroomDoorTarget = isWashroomDoorOpen ? openWashroomDoorPosition : initialWashroomDoorPosition;
        WashroomDoor.transform.localRotation = Quaternion.RotateTowards(WashroomDoor.transform.localRotation, washroomDoorTarget, washroomRotationSpeed * 90f * Time.deltaTime);

        Quaternion washroomSeatTarget = isWashroomSeatOpen ? openWashroomSeatPosition : initialWashroomSeatPosition;
        WashroomSeat.transform.localRotation = Quaternion.RotateTowards(WashroomSeat.transform.localRotation, washroomSeatTarget, washroomRotationSpeed * 90f * Time.deltaTime);

        Quaternion leftCabinetTarget = isMiddleCabinetOpen ? openLeftCabinetDoorPosition : initialLeftCabinetDoorPosition;
        Quaternion rightCabinetTarget = isMiddleCabinetOpen ? openRightCabinetDoorPosition : initialRightCabinetDoorPosition;
        LeftDoor.transform.localRotation = Quaternion.RotateTowards(LeftDoor.transform.localRotation, leftCabinetTarget, washroomRotationSpeed * 90f * Time.deltaTime);
        RightDoor.transform.localRotation = Quaternion.RotateTowards(RightDoor.transform.localRotation, rightCabinetTarget, washroomRotationSpeed * 90f * Time.deltaTime);

        TopDrawerL.transform.localPosition = Vector3.MoveTowards(TopDrawerL.transform.localPosition, WithDrawerY(topDrawerLClosedPosition, selectedLeftDrawer == 1 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);
        MiddleDrawerL.transform.localPosition = Vector3.MoveTowards(MiddleDrawerL.transform.localPosition, WithDrawerY(middleDrawerLClosedPosition, selectedLeftDrawer == 2 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);
        BottomDrawerL.transform.localPosition = Vector3.MoveTowards(BottomDrawerL.transform.localPosition, WithDrawerY(bottomDrawerLClosedPosition, selectedLeftDrawer == 3 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);

        TopDrawerR.transform.localPosition = Vector3.MoveTowards(TopDrawerR.transform.localPosition, WithDrawerY(topDrawerRClosedPosition, selectedRightDrawer == 1 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);
        MiddleDrawerR.transform.localPosition = Vector3.MoveTowards(MiddleDrawerR.transform.localPosition, WithDrawerY(middleDrawerRClosedPosition, selectedRightDrawer == 2 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);
        BottomDrawerR.transform.localPosition = Vector3.MoveTowards(BottomDrawerR.transform.localPosition, WithDrawerY(bottomDrawerRClosedPosition, selectedRightDrawer == 3 ? OpenDrawerY : ClosedDrawerY), DrawerMoveSpeed * Time.deltaTime);

        Vector3 flushTarget = isWashroomFlushPressed ? pressedWashroomFlushPosition : initialWashroomFlushPosition;
        WashroomFlush.transform.localPosition = Vector3.MoveTowards(WashroomFlush.transform.localPosition, flushTarget, flushMoveSpeed * Time.deltaTime);

        if (isWashroomFlushPressed &&
            Vector3.Distance(WashroomFlush.transform.localPosition, pressedWashroomFlushPosition) < 0.0001f)
        {
            flushHoldTimer += Time.deltaTime;
            if (flushHoldTimer >= 0.12f)
            {
                isWashroomFlushPressed = false;
            }
        }
        Quaternion masterBedroomDoorTarget = isMasterBedroomDoorOpen
            ? masterBedroomDoorOpenRotation
            : masterBedroomDoorClosedRotation;
        masterBedroomDoor.transform.localRotation = Quaternion.RotateTowards(
            masterBedroomDoor.transform.localRotation,
            masterBedroomDoorTarget,
            masterBedroomRotationSpeed * Time.deltaTime);

        Vector3 leftWindowTarget = masterBedroomWindowLeftClosedPosition;
        Vector3 rightWindowTarget = masterBedroomWindowRightClosedPosition;

        if (masterBedroomWindowAmount < 0f)
        {
            leftWindowTarget.x = Mathf.Lerp(
                masterBedroomWindowLeftClosedPosition.x,
                masterBedroomWindowRightClosedPosition.x,
                -masterBedroomWindowAmount);
        }
        else
        {
            rightWindowTarget.x = Mathf.Lerp(
                masterBedroomWindowRightClosedPosition.x,
                masterBedroomWindowLeftClosedPosition.x,
                masterBedroomWindowAmount);
        }

        float windowMoveSpeed = masterBedroomMoveSpeed * 0.01f;
        masterBedroomWindowLeft.transform.localPosition = Vector3.MoveTowards(
            masterBedroomWindowLeft.transform.localPosition,
            leftWindowTarget,
            windowMoveSpeed * Time.deltaTime);
        masterBedroomWindowRight.transform.localPosition = Vector3.MoveTowards(
            masterBedroomWindowRight.transform.localPosition,
            rightWindowTarget,
            windowMoveSpeed * Time.deltaTime);

        Vector3 blanketTarget =
            masterBedroomBlanketClosedPosition + Vector3.left * (masterBedroomBlanketAmount * 0.8f);
        masterBedroomBlanket.transform.localPosition = Vector3.MoveTowards(
            masterBedroomBlanket.transform.localPosition,
            blanketTarget,
            masterBedroomMoveSpeed * Time.deltaTime);

        Quaternion pillowTarget =
            masterBedroomPillowClosedRotation * Quaternion.Euler(0f, masterBedroomPillowAngle, 0f);
        masterBedroomPillow.transform.localRotation = Quaternion.RotateTowards(
            masterBedroomPillow.transform.localRotation,
            pillowTarget,
            masterBedroomRotationSpeed * Time.deltaTime);
    }
}
