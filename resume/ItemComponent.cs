using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BGSulz;

public class ItemComponent : IComponent
{
    public ResumeItem Item { get; }

    public ItemComponent(ResumeItem item)
    {
        Item = item;
    }

    public void Compose(IContainer container)
    {
        if (Item.Bullets == null) return;

        container.Row(row =>
        {
            row.Spacing(12);
            
            var left = row.RelativeItem();
            var right = row.ConstantItem(100);

            if (Item.Timespan != null)
            {
                right
                    .AlignRight()
                    .MinimalBox()
                    //.ExtendHorizontal()
                    .Layers(layers =>
                    {
                        layers.Layer().RoundedRect();

                        layers
                            .PrimaryLayer()
                            .PaddingHorizontal(Styles.RectMargin)
                            .PaddingVertical(2)
                            .Text(Item.Timespan.ToString())
                            .Medium();
                    });
            }

            left.Column(col =>
            {
                if (!string.IsNullOrWhiteSpace(Item.ItemTitle) || !string.IsNullOrWhiteSpace(Item.ItemDesc))
                {
                    col.Item().Text(text =>
                    {
                        text.Span(Item.ItemTitle).Bold();

                        if (string.IsNullOrWhiteSpace(Item.ItemDesc)) return;

                        text.Span(" - ");
                        text.Span(Item.ItemDesc).Italic();
                    });
                }

                foreach (var item in Item.Bullets) col.Item().Text($"• {item}");
            });
        });
    }
}