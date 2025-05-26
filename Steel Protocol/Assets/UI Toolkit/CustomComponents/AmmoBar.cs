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
    private bool isReloading = false;
    private float reloadTime;
    private float reloadTimer;
    private float reloadProgress = 0f; // purely for animation

    [SerializeField, DontCreateProperty]
    float ammo;
    public float GetAmmo() => ammo;    
    public void SetAmmo(float current, float max)
    {
        currentAmmo = Mathf.Clamp(current, 0, max);

        ammo = Mathf.Clamp(current / max * 100f, 0.01f, 100f);
        ammoLabel.text = $"{currentAmmo}";
        MarkDirtyRepaint();
    }

    public AmmoBar()
    {
        InitVisuals();
        InitStyleCallbacks();
        
    }

    private void InitVisuals()
    {
        ammoLabel = new Label
        {
            style =
            {
                unityTextAlign = TextAnchor.MiddleCenter,
                position = Position.Absolute,
                top = 0,
                left = 0,
                right = 0,
                height = Length.Percent(-100),
                color = Color.white,
                fontSize = 14,
                unityFontStyleAndWeight = FontStyle.Bold
            }
        };
        Add(ammoLabel);// Default background color
    }

    private void InitStyleCallbacks()
    {
        RegisterCallback<CustomStyleResolvedEvent>(CustomStyleResolved);
        generateVisualContent += GenerateVisualContent;
    }


    private void CustomStyleResolved(CustomStyleResolvedEvent evt)
    {
        UpdateCustomStyles();
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
        var painter = context.painter2D;

        DrawBackground(painter);
        DrawFill(painter);
    }

    private void DrawBackground(Painter2D painter)
        {
        float width = contentRect.width;
        float height = contentRect.height;

        painter.BeginPath();
        painter.lineWidth = 4f;
        painter.MoveTo(Vector2.zero);
        painter.LineTo(new Vector2(width, 0));
        painter.LineTo(new Vector2(width, height));
        painter.LineTo(new Vector2(0, height));
        painter.ClosePath();
        painter.fillColor = backgroundColor;
        painter.Fill(FillRule.NonZero);
        painter.Stroke();
    }

    private void DrawFill(Painter2D painter)
    {
        float width = contentRect.width;
        float height = contentRect.height;

        float fillPercent = isReloading ? reloadProgress : ammo;
        float fillWidth = width * (fillPercent / 100f);

        painter.BeginPath();
        painter.lineWidth = 2f;
        painter.MoveTo(Vector2.zero);
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
        // Prevent restarting if already reloading
        if (isReloading)
            return;

        isReloading = true;
        reloadTime = duration;
        reloadTimer = 0f;
        reloadProgress = 0f;

        schedule.Execute(() =>
        {
            reloadTimer += Time.deltaTime;
            reloadProgress = reloadTimer / reloadTime * 100f;
            MarkDirtyRepaint(); // Force redraw with updated reloadProgress
        })
        .Every(0)
        .Until(() =>
        {
            if (reloadTimer >= reloadTime)
            {
                isReloading = false;
                reloadProgress = 0f;
                return true; // stop scheduling
            }
            return false; // keep going
        });
    }
}
