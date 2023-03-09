namespace GymWeb.Models.ViewModels {
    public class ExercicioViewModel {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string? AreaCorporal { get; set; }
        public int QntSerie { get; set; }
        public int Repeticao { get; set; }
        public IFormFile Image { get; set; }
        public string Preparacao { get; set; }
        public string Execucao { get; set; }
        public string Dicas { get; set; }
        public string MusculoPrimario { get; set; }
        public string MusculoSecundario { get; set; }
        public IFormFile ImageMusculoAlvo { get; set; }


        public static List<Exercicio> GetAreaCorporal() {
            var lista = new List<Exercicio>()
            {
                new Exercicio( ){ AreaCorporal = "Peito"},
                new Exercicio( ){ AreaCorporal = "Costas"},
                new Exercicio( ){ AreaCorporal = "Pernas"},
                new Exercicio( ){ AreaCorporal = "Braços"},
                new Exercicio( ){ AreaCorporal = "Glúteos"},
                new Exercicio( ){ AreaCorporal = "Cárdio"}

            };
            return lista;
        }

       
    }
}
