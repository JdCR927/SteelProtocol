using UnityEngine;
using Unity.Properties;
using UnityEngine.UIElements;

[UxmlElement]
public partial class AmmoBar : VisualElement
{

    static CustomStyleProperty<Color> s_FillColor = new("--fill-color");
    static CustomStyleProperty<Color> s_BackgroundColor = new("--background-color");
    static CustomStyleProperty<Color> s_ReloadColor = new("--reload-color");

    Color fillColor;
    Color backgroundColor;
    Color reloadColor;

    private Label ammoLabel;
    private float currentAmmo;
    private float maxAmmo;
    private bool isReloading = false;
    private float reloadTime = 1f;
    private float reloadTimer = 0f;

    [SerializeField, DontCreateProperty]
    float ammo;

    public float GetAmmo() => ammo;
    
    public void SetAmmo(float current, float max)
    {
        currentAmmo = Mathf.Clamp(current, 0, max);
        maxAmmo = max;

        ammo = Mathf.Clamp(current / max * 100f, 0.01f, 100f);
        ammoLabel.text = $"{currentAmmo}";
        MarkDirtyRepaint();
    }

    public AmmoBar()
    {
        // Register a callback after custom style resolution
        RegisterCallback<CustomStyleResolvedEvent>(CustomStyleResolved);

        // Register a callback to generate the visual content of the control
        generateVisualContent += GenerateVisualContent;

        ammoLabel = new Label();
        ammoLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        ammoLabel.style.position = Position.Absolute;
        ammoLabel.style.top = 0;
        ammoLabel.style.left = 0;
        ammoLabel.style.right = 0;
        ammoLabel.style.height = Length.Percent(100);
        ammoLabel.style.color = Color.white;
        ammoLabel.style.fontSize = 14;
        ammoLabel.style.unityFontStyleAndWeight = FontStyle.Bold;

        Add(ammoLabel);

        // Start update loop (for animation)
        schedule.Execute(AnimateReload).Every(16); // ~60fps
    }


    private void CustomStyleResolved(CustomStyleResolvedEvent evt)
    {
        if (evt.currentTarget == this)
        {
            AmmoBar element = (AmmoBar)evt.currentTarget;
            element.UpdateCustomStyles();
        }
    }

    private void UpdateCustomStyles()
    {
        bool repaint = false;
        if (customStyle.TryGetValue(s_FillColor, out fillColor))
            repaint = true;
        if (customStyle.TryGetValue(s_BackgroundColor, out backgroundColor))
            repaint = true;
        if (customStyle.TryGetValue(s_ReloadColor, out reloadColor))
            repaint = true;
        if (repaint)
            MarkDirtyRepaint();
    }

    private void GenerateVisualContent(MeshGenerationContext context)
    {
        // Background
        float width = contentRect.width;
        float height = contentRect.height;

        var painter = context.painter2D;

        painter.BeginPath();
        painter.lineWidth = 4f;
        painter.MoveTo(new Vector2(0, 0));
        painter.LineTo(new Vector2(width, 0));
        painter.LineTo(new Vector2(width, height));
        painter.LineTo(new Vector2(0, height));
        painter.ClosePath();
        painter.fillColor = backgroundColor;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();

        // Fill
        float fillWidth = width * (ammo / 100f);

        painter.BeginPath();
        painter.lineWidth = 2f;
        painter.MoveTo(new Vector2(0, 0));
        painter.LineTo(new Vector2(fillWidth, 0));
        painter.LineTo(new Vector2(fillWidth, height));
        painter.LineTo(new Vector2(0, height));
        painter.ClosePath();
        painter.fillColor = isReloading ? reloadColor : fillColor;
        painter.Fill();
        painter.Stroke();
    }

    public void StartReload(float duration)
    {
        reloadTime = duration;
        reloadTimer = 0f;
        isReloading = true;
        ammo = 0.01f;
        MarkDirtyRepaint();
    }

    private void AnimateReload()
    {
        if (!isReloading)
            return;

        reloadTimer += Time.deltaTime;
        float t = Mathf.Clamp01(reloadTimer / reloadTime);

        int interpolatedAmmo = Mathf.RoundToInt(t * maxAmmo);
        SetAmmo(interpolatedAmmo, maxAmmo);

        if (t >= 1f)
            isReloading = false;
    }
}
