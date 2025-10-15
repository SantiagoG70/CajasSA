namespace Boxes.Shared.Entites;

public class Alerta
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;
}