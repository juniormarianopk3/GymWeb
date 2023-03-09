using System.Collections;

namespace GymWeb.Models {
    public class Treino {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Dias { get; set; }
        public List<Exercicio>? Exercicios { get; set; }
    }
}
