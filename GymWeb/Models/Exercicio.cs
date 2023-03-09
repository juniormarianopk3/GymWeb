using System.Reflection.Metadata.Ecma335;

namespace GymWeb.Models {
    public class Exercicio {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ?AreaCorporal { get; set; }
        public int QntSerie { get; set; }
        public int Repeticao { get; set; }
        public string Image { get; set; }
        public string Preparacao { get; set; }
        public string Execucao { get; set; }
        public string Dicas { get; set; }
        public string MusculoPrimario { get; set; }
        public string MusculoSecundario { get; set; }
        public string ImageMusculoAlvo { get; set; }
        public ICollection<Treino>  Treinos { get; set; }
   

    }
    }

