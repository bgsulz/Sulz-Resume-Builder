using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BGSulz;

public class CategoryComponent : IComponent
{
    public ResumeCategory Category { get; }

    public CategoryComponent(ResumeCategory category)
    {
        Category = category;
    }

    public void Compose(IContainer doc)
    {
        if (Category.Items == null) return;
        
        doc.Column(col =>
        {
            col.Item().Height(10);
            col.Item().Text(Category.CategoryTitle?.ToUpperInvariant()).Bold().LetterSpacing(0.1f);
            
            foreach (var item in Category.Items)
            {
                col.Item().Component(new ItemComponent(item));
            }
        });
    }
}