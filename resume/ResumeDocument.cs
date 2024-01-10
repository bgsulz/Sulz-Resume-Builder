using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BGSulz;

public class ResumeDocument : IDocument
{
    public ResumeModel Model { get; }

    public ResumeDocument(ResumeModel model)
    {
        Model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer doc)
    {
        doc.Page(page =>
        {
            page.DefaultTextStyle(TextStyle.Default.FontSize(10).FontFamily("Schibsted Grotesk"));
            page.MarginVertical(24);
            page.MarginHorizontal(24 - Styles.RectMargin);

            page.Header().Layers(layers =>
            {
                layers.Layer().RoundedRect();
                layers.PrimaryLayer()
                    .PaddingHorizontal(Styles.RectMargin)
                    .PaddingVertical(2)
                    .Element(ComposeHeader);
            });
            page.Content()
                .PaddingLeft(5)
                .Element(ComposeCategories);
        });
    }

    private void ComposeHeader(IContainer doc)
    {
        doc.Row(row =>
        {
            var left = row.ConstantItem(150);
            var right = row.RelativeItem();

            left
                .Text("Ben Sulzinsky")
                .Thin()
                .LetterSpacing(0.025f)
                .FontSize(16);

            right.AlignRight()
                .AlignMiddle()
                .Text(text =>
                {
                    text.Span("(203) 885-6563 • ");
                    text.Hyperlink("bsulzinsky@gmail.com", "mailto:bsulzinsky@gmail.com");
                    text.Span(" • ");
                    text.Hyperlink("bgsulz.com", "https://bgsulz.com");
                    text.Span(" • ");
                    text.Hyperlink("LinkedIn", "https://www.linkedin.com/in/ben-sulzinsky-b14b06166/");
                });
        });
    }

    private void ComposeCategories(IContainer doc)
    {
        if (Model.Categories == null) return;
        
        doc.Column(col =>
        {
            foreach (var item in Model.Categories)
            {
                col.Item().Component(new CategoryComponent(item));
            }
        });
    }
}