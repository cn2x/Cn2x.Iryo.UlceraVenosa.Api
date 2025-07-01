using Beyond.IPO.Domain.Core.Specification;
using Cn2x.Iryo.UlceraVenosa.Domain.Entities;
using System.Linq.Expressions;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Specifications {
    public class PacienteFullTextSpecification : Specification<Paciente> {
        private readonly string? _term;

        public PacienteFullTextSpecification(string? term) {
            _term = term;
        }

        public override Expression<Func<Paciente, bool>> SatisfiedBy() {
            if (string.IsNullOrWhiteSpace(_term))
                return p => false;

            var termLower = _term!.ToUpper();
            return p => p.Cpf.Equals(_term) || p.Nome.Contains(termLower);
        }
    }
}
