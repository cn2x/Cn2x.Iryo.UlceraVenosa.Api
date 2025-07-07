using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Enumeracoes {

    public enum LateralidadeEnum : int {
        Direita = 1,
        Esquerda = 2,
    }

    public class Lateralidade : Enumeration<LateralidadeEnum> {
        public static Lateralidade Direto = new Lateralidade(LateralidadeEnum.Direita, "Direta");
        public static Lateralidade Esquerdo = new Lateralidade(LateralidadeEnum.Esquerda, "Esquerda");

        private Lateralidade(LateralidadeEnum value, string displayName)
          : base(value, displayName) {
        }
    }
}
