
using Cn2x.Iryo.UlceraVenosa.Domain.Core;
using Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes;

[ExtendObjectType("Query")]
public class ClassificacaoQueries
{
    [GraphQLName("clinicas")]
    public async Task<List<KeyValueDto>> GetClassificacaoClinicasAsync()
    {
        var results = Enumeration<ClinicaEnum>.GetAll<Clinica>()
            .Select(p => new KeyValueDto
            {
                Key = p.Id.ToString(),
                Value = $"{p.Name}-{p.Descricao}",

            })
            .ToList();

        return await Task.FromResult(results); // simula async
    }

    [GraphQLName("anatomicas")]
    public async Task<List<KeyValueDto>> GetClassificacaoAnatomicaAsync()
    {
        var results = Enumeration<AnatomicaEnum>.GetAll<Anatomica>()
            .Select(p => new KeyValueDto
            {
                Key = p.Id.ToString(),
                Value = $"{p.Name}-{p.Descricao}",

            })
            .ToList();

        return await Task.FromResult(results); // simula async

    }

    [GraphQLName("etiologicas")]
    public async Task<List<KeyValueDto>> GGetClassificacaoAnatomicaAsync()
    {
        var results = Enumeration<EtiologicaEnum>.GetAll<Etiologica>()
           .Select(p => new KeyValueDto
           {
               Key = p.Id.ToString(),
               Value = $"{p.Name}-{p.Descricao}",

           })
           .ToList();

        return await Task.FromResult(results); // simula async
    }

    [GraphQLName("patofisiologicas")]
    public async Task<List<KeyValueDto>> GetClassificacaoPatofisiologicasAsync()
    {
        var results = Enumeration<PatofisiologicaEnum>.GetAll<Patofisiologica>()
            .Select(p => new KeyValueDto
            {
                Key = p.Id.ToString(),
                Value = $"{p.Name}-{p.Descricao}",

            })
            .ToList();

        return await Task.FromResult(results); // simula async
    }
}
