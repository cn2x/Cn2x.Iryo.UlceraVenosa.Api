using Beyond.IPO.Domain.Core.Specification;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using System.Linq.Expressions;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Specifications {
    public class PacienteOrSpecification : Specification<Paciente> {
        private readonly string? _cpf;
        private readonly string? _nome;
        private readonly Guid? _id;

        public PacienteOrSpecification(Guid? id, string? cpf, string? nome) {
            _id = id;
            _cpf = cpf;
            _nome = nome;
        }

        public override Expression<Func<Paciente, bool>> SatisfiedBy() {
            bool hasId = _id.HasValue && _id.Value != Guid.Empty;
            bool hasCpf = !string.IsNullOrWhiteSpace(_cpf);
            bool hasNome = !string.IsNullOrWhiteSpace(_nome);

            int count = (hasId ? 1 : 0) + (hasCpf ? 1 : 0) + (hasNome ? 1 : 0);
            if (count != 1)
                throw new ArgumentException("Exatamente um dos parâmetros (id, cpf, nome) deve ser informado.");

            if (hasId)
                return p => p.Id == _id.Value;
            if (hasCpf)
            {
                var cpfLower = _cpf!.ToLower();
                return p => p.Cpf.ToLower().Contains(cpfLower);
            }
            // só nome
            var nomeLowerOnly = _nome!.ToLower();
            return p => p.Nome.ToLower().Contains(nomeLowerOnly);
        }
    }
}
