using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes {

    public enum LateralidadeEnum : int {
        Direta = 1,
        Esquerda = 2,
    }

    public class Lateralidade : Enumeration<LateralidadeEnum> {
        public static Lateralidade Direto = new Lateralidade(LateralidadeEnum.Direta, "Direta");
        public static Lateralidade Esquerdo = new Lateralidade(LateralidadeEnum.Esquerda, "Esquerda");

        private Lateralidade(LateralidadeEnum value, string displayName)
          : base(value, displayName) {
        }
    }
}
