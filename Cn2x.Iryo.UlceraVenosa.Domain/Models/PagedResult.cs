namespace Cn2x.Iryo.UlceraVenosa.Domain.Models;

/// <summary>
/// Modelo para resultados paginados
/// </summary>
/// <typeparam name="T">Tipo dos itens</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Lista de itens da página atual
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();
    
    /// <summary>
    /// Total de itens em todas as páginas
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Página atual
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Tamanho da página
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total de páginas
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    
    /// <summary>
    /// Indica se há página anterior
    /// </summary>
    public bool HasPreviousPage => Page > 1;
    
    /// <summary>
    /// Indica se há próxima página
    /// </summary>
    public bool HasNextPage => Page < TotalPages;
} 