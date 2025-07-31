namespace Shopper.Services.Components.Dtos
{
    public class ItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not ItemDto other) return false;
            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
                && string.Equals(Genre, other.Genre, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Name?.ToLowerInvariant(),
                Genre?.ToLowerInvariant()
            );
        }
    }
}
