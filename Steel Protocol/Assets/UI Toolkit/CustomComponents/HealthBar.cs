using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class HealthBar : VisualElement
{
    static CustomStyleProperty<Color> s_FillColor = new("--fill-color");
    static CustomStyleProperty<Color> s_BackgroundColor = new("--background-color");

    Color fillColor;
    Color backgroundColor;

    // This is the number that the Label displays as a percentage
    [SerializeField, DontCreateProperty]
    float progress;

    // A value between 0 and 100
    [UxmlAttribute, CreateProperty]
    public float Progress
    {
        get => progress;
        set
        {
            // Whenever the progress property changes, MarkDirtyRepaint() is named. This causes a call to the
            // generateVisualContents callback
            progress = Mathf.Clamp(value, 0.01f, 100f);
            MarkDirtyRepaint();
        }
    }

    public HealthBar()
    {
        // Register a callback after custom style resolution
        RegisterCallback<CustomStyleResolvedEvent>(CustomStyleResolved);

        // Register a callback to generate the visual content of the control
        generateVisualContent += GenerateVisualContent;
    }

    private void CustomStyleResolved(CustomStyleResolvedEvent evt)
    {
        if (evt.currentTarget == this)
        {
            HealthBar element = (HealthBar)evt.currentTarget;
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
        float fillWidth = width * (progress / 100f);

        painter.BeginPath();
        painter.lineWidth = 2f;
        painter.MoveTo(new Vector2(0, 0));
        painter.LineTo(new Vector2(fillWidth, 0));
        painter.LineTo(new Vector2(fillWidth, height));
        painter.LineTo(new Vector2(0, height));
        painter.ClosePath();
        painter.fillColor = fillColor;
        painter.Fill();
        painter.Stroke();
    }

}
